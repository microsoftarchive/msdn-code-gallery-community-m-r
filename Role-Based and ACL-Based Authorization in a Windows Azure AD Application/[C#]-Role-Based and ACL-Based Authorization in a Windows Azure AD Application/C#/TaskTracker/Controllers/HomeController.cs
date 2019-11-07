using Microsoft.WindowsAzure.ActiveDirectory;
using Microsoft.WindowsAzure.ActiveDirectory.GraphHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Services.Client;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace TaskTracker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Error(string errorMessage)
        {
            ViewData["errorMessage"] = errorMessage;
            return View();
        }

        public ActionResult Users()
        {
            //get the user's objectID
            Boolean onACL = false;
            String userObjectId = ((ClaimsIdentity)User.Identity).FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;

            List<ACLElem> ACLElems = XmlHelper.GetACLElemsFromXml();
            foreach (ACLElem elem in ACLElems)
            {
                //is user's objectId in the ACL?
                if (elem.ObjectId.Equals(userObjectId))
                {
                    onACL = true;
                }
                else
                {
                    foreach (Claim groupClaim in ((ClaimsIdentity)User.Identity).FindAll("Group"))
                    {
                        //is a group the user belongs to in the ACL?
                        if (elem.ObjectId.Equals(groupClaim.Value))
                        {
                            onACL = true;
                            break;
                        }
                    }
                }
                // exit as soon as you find the user or a group in the ACL
                if (onACL)
                {
                    break;
                }
            }

            //if user is not in ACL - do not grant permission
            if (!onACL)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = "Access Denied. To view this resource, have an admin add you or your group to the ACL." });
            }


            //get the tenantName
            string tenantName = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;

            // get the clientId and password values from the Web.config file
            string clientId = ConfigurationManager.AppSettings["ClientId"];
            string password = ConfigurationManager.AppSettings["Password"];


            // use the Graph help to get a token
            AADJWTToken token = DirectoryDataServiceAuthorizationHelper.GetAuthorizationToken(tenantName, clientId, password);

            // use the token to initialize a graphService instance
            DirectoryDataService graphService = new DirectoryDataService(tenantName, token);

            //  get Users
            //
            var users = graphService.users;
            QueryOperationResponse<User> response;
            response = users.Execute() as QueryOperationResponse<User>;
            List<User> userList = response.ToList();
            ViewBag.userList = userList;


            //  Use the token for subsequent Graph calls.
            //  Is the existing token expire or about to expire in 2 mins?
            //  if true, get a new token and refresh the graph service
            //
            int tokenMins = 2;
            if (token.IsExpired || token.WillExpireIn(tokenMins))
            {
                AADJWTToken newToken = DirectoryDataServiceAuthorizationHelper.GetAuthorizationToken(tenantName, clientId, password);
                token = newToken;
                graphService = new DirectoryDataService(tenantName, token);
            }

            //  get tenant information
            //
            var tenant = graphService.tenantDetails;
            QueryOperationResponse<TenantDetail> responseTenantQuery;
            responseTenantQuery = tenant.Execute() as QueryOperationResponse<TenantDetail>;
            List<TenantDetail> tenantInfo = responseTenantQuery.ToList();
            ViewBag.OtherMessage = "User List from tenant: " + tenantInfo[0].displayName;


            return View(userList);
        }
    }
}
