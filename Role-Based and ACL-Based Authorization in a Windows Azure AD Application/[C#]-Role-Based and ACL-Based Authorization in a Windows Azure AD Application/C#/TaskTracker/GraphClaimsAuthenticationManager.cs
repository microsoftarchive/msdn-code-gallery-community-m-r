using Microsoft.WindowsAzure.ActiveDirectory;
using Microsoft.WindowsAzure.ActiveDirectory.GraphHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web.Hosting;

namespace TaskTracker
{
    public class GraphClaimsAuthenticationManager : ClaimsAuthenticationManager
    {
        public static string RoleMapXMLFilePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", "Roles.xml");

        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            if (incomingPrincipal != null && incomingPrincipal.Identity.IsAuthenticated == true){                
                //get the tenantId
                string tenantId = incomingPrincipal.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;

                // Use the DirectoryDataServiceAuthorizationHelper graph helper API
                // to get a token to access the Windows Azure AD Graph
                string clientId = ConfigurationManager.AppSettings["ClientId"];
                string password = ConfigurationManager.AppSettings["Password"];
                AADJWTToken token = DirectoryDataServiceAuthorizationHelper.GetAuthorizationToken(tenantId, clientId, password);

                // initialize a graphService instance. Use the JWT token acquired in the previous step.
                DirectoryDataService graphService = new DirectoryDataService(tenantId, token);
                
                // get the user's ObjectId
                String currentUserObjectId = incomingPrincipal.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
                
                // Get the User object by querying Windows Azure AD Graph
                User currentUser = new WebRetryHelper<User>(() => graphService.directoryObjects.OfType<User>().Where(it => (it.objectId == currentUserObjectId)).SingleOrDefault()).Value;

                /* 
                 * TaskTracker defines four roles that are specific to this app: 
                 * "Admin", "Observer", "Writer", "Approver". These app roles are 
                 * different from the roles that are built into Windows Azure AD, 
                 * e.g. "Company Administrator", "User Account Administrator".
                 * 
                 * This code uses the memberOf property of the User object to get
                 * the user's built-in roles. If the user has the "Company Administrator" 
                 * built-in role, the app assigns the user to the "Admin" app role.
                 */

                // get the user's built-in roles
                new WebRetryHelper<object>(() => graphService.LoadProperty(currentUser, "memberOf"));
                List<Role> currentRoles = currentUser.memberOf.OfType<Role>().ToList();

                //if the user is a Company Administrator (Global Administrator), 
                // assign them the "Admin" role in the app. 
                foreach(Role role in currentRoles)
                {
                    if (role.displayName.Equals("Company Administrator"))
                    {
                        ((ClaimsIdentity)incomingPrincipal.Identity).AddClaim(new Claim(ClaimTypes.Role, "Admin", ClaimValueTypes.String, "TaskTrackerSampleApplication"));
                    }
                }

                /* 
                * To determine the user's group membership, TaskTracker uses 
                * the getCompleteGroupMembership function, which calls the getMemberGroups
                * function, which returns the transitive group membership of the user. 
                */

                // Now, query transitive group membership of the user
                List<string> completeGroupMembership = new WebRetryHelper<List<String>>(() => graphService.GetCompleteGroupMembership(tenantId, currentUserObjectId, token)).Value;

                //Store the user's groups as claims of type "Group"
                foreach (string groupId in completeGroupMembership)
                {
                    Debug.WriteLine("adding " + groupId);
                    ((ClaimsIdentity)incomingPrincipal.Identity).AddClaim(new Claim("Group", groupId, ClaimValueTypes.String, "WindowsAzureADGraph"));
                }

                //Get role assignments
                foreach(string role in getRoles(currentUserObjectId, completeGroupMembership))
                {
                    //Store the user's application roles as claims of type Role
                    ((ClaimsIdentity)incomingPrincipal.Identity).AddClaim(new Claim(ClaimTypes.Role, role, ClaimValueTypes.String, "TaskTrackerSampleApplication"));
                }
            
            }
            return incomingPrincipal;
        }

        private static List<String> getRoles(string objectId, List<String> groupMemberships)
        {
            List<string> roles = new List<string>();

            if (!File.Exists(RoleMapXMLFilePath) || objectId == null)
            {
                return roles;
            }

            foreach (List<RoleMapElem> list in XmlHelper.GetRoleMappingsFromXml())
            {
                foreach (RoleMapElem elem in list)
                {
                    if(elem.ObjectId.Equals(objectId) || groupMemberships != null && groupMemberships.Contains(elem.ObjectId))
                    {
                        roles.Add(elem.Role);
                    }
                }
            }
            return roles;
        }
    }
}
