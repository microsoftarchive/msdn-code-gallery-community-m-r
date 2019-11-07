using Microsoft.WindowsAzure.ActiveDirectory;
using Microsoft.WindowsAzure.ActiveDirectory.GraphHelper;
using System.Configuration;
using System.Web;

namespace TaskTracker
{
    public static class GraphServiceHelper
    {
        public static DirectoryDataService CreateDirectoryDataService(HttpSessionStateBase session)
        {
            AADJWTToken token = null;
           
            if (session != null && session["token"] != null)
            {
                token = session["token"] as AADJWTToken;
            }

            //Fetch a token if it has not been fetched earlier or if it is 2 minutes from expiration. 
            if (token == null || token.WillExpireIn(2))
            {
                token = DirectoryDataServiceAuthorizationHelper.GetAuthorizationToken(ConfigurationManager.AppSettings["TenantDomainName"],
                    ConfigurationManager.AppSettings["ClientId"], ConfigurationManager.AppSettings["Password"]);
                if (session != null)
                {
                    session["token"] = token;
                }
            }
            return new DirectoryDataService(ConfigurationManager.AppSettings["TenantDomainName"], token); 
        }

    }
}