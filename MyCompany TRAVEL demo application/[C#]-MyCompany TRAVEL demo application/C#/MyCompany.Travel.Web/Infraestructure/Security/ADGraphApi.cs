namespace MyCompany.Travel.Web.Security
{
    using Microsoft.WindowsAzure.ActiveDirectory;
    using Microsoft.WindowsAzure.ActiveDirectory.GraphHelper;
    using System;
    using System.Collections.Generic;
    using System.Data.Services.Client;
    using System.Linq;
    using System.Web;
    using System.Web.Configuration;

    /// <summary>
    /// <see cref="MyCompany.Travel.Web.Security.IADGraphApi"/>
    /// </summary>
    public class ADGraphApi : IADGraphApi
    {
        /// <summary>
        /// <see cref="MyCompany.Travel.Web.Security.IADGraphApi"/>
        /// http://code.msdn.microsoft.com/Windows-Azure-AD-Graph-API-a8c72e18
        /// </summary>
        /// <param name="userPrincipalName"><see cref="MyCompany.Travel.Web.Security.IADGraphApi"/></param>
        /// <param name="groupName"><see cref="MyCompany.Travel.Web.Security.IADGraphApi"/></param>
        /// <returns><see cref="MyCompany.Travel.Web.Security.IADGraphApi"/></returns>
        public bool IsInGroup(string userPrincipalName, string groupName)
        {
            string clientId = WebConfigurationManager.AppSettings["ida:ClientId"];
            string key = WebConfigurationManager.AppSettings["ida:Key"];
            string tenant = WebConfigurationManager.AppSettings["ida:Tenant"];

            // get a token using the helper
            AADJWTToken token = DirectoryDataServiceAuthorizationHelper.GetAuthorizationToken(tenant, clientId, key);

            // initialize a graphService instance using the token acquired from previous step
            var graphService = new DirectoryDataService(tenant, token);

            var users = graphService.users;
            var response = users.Execute() as QueryOperationResponse<User>;

            List<User> userList = response.Where(u => u.userPrincipalName == userPrincipalName).ToList();
            if (userList.Any())
            {
                var user = userList.First();
                List<string> _groupMemberships;
                _groupMemberships = new List<string>();
                string queryString = string.Format("https://graph.windows.net/{0}/{1}/{2}/{3}", tenant, "users", user.objectId.ToString(), "memberOf");
                var query = graphService.Execute<Group>(new Uri(queryString));
                var groups = query.Where(a => a.objectType == "Group" && a.displayName == groupName);
                var groupList = groups as IList<Group>;
                if (groupList != null)
                {
                    var directoryObjects = groups as IList<Group> ?? groups.ToList();
                    return directoryObjects.Any();
                }
            }

            return false;
        }
    }
}