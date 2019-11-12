using System;
using Microsoft.WindowsAzure.ActiveDirectory;
using Microsoft.WindowsAzure.ActiveDirectory.GraphHelper;
using System.Configuration;
using System.Web;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace MvcDirectoryGraphSample.Helpers
{
    public static class MVCGraphServiceHelper
    {
        public static DirectoryDataService CreateDirectoryDataService(HttpSessionStateBase session)
        {
            // AADJWTToken token = null;
            AuthenticationResult token = null;
            if (session != null && session["token"] != null)
            {
                token = session["token"] as AuthenticationResult;
            }

            AuthenticationResult authenticationResult = null;

            var futureTime = DateTimeOffset.UtcNow.AddMinutes(2);

            // Fetch a token if it has not been fetched earlier or if the token is about to expire in 2 mins
            if (token == null || (futureTime.UtcDateTime > token.ExpiresOn.UtcDateTime))
            {
                string authString = System.String.Format(StringConstants.AzureADSTSURL,
                    ConfigurationManager.AppSettings["TenantDomainName"]);
                AuthenticationContext authenticationContext = new AuthenticationContext(authString);
                ClientCredential clientCred = new ClientCredential(ConfigurationManager.AppSettings["AppPrincipalId"],
                    ConfigurationManager.AppSettings["Password"]);
                try
                {
                    authenticationResult = authenticationContext.AcquireToken(StringConstants.GraphPrincipalId,
                        clientCred);
                }
                catch (ActiveDirectoryAuthenticationException ex)
                {

                    if (ex.InnerException != null)
                    {
                        // You should implement retry and back-off logic per the guidance given here:http://msdn.microsoft.com/en-us/library/dn168916.aspx
                        // InnerException Message will contain the HTTP error status codes mentioned in the link above
                        // Console.WriteLine("Error detail: {0}", ex.InnerException.Message);
                    }
                }

                token = authenticationResult;

                if (session != null)
                {
                    session["token"] = authenticationResult;
                }
            }

            if (token == null)
            {
                return null;
            }

            // Configure a AADJWTToken using the ADAL token
            var aadToken = new AADJWTToken();
            aadToken.AdalToken = token;
            aadToken.AccessToken = token.AccessToken;
            aadToken.TokenType = token.AccessTokenType;
            return new DirectoryDataService(ConfigurationManager.AppSettings["TenantDomainName"], aadToken);
        }        
    }   
}