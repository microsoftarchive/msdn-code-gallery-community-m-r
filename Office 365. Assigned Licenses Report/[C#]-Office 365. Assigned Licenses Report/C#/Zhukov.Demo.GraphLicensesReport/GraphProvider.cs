using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Zhukov.Demo.GraphLicensesReport
{
    public class GraphProvider : IAuthenticationProvider
    {
        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {

            var token = await GetToken();
            request.Headers.Add("Authorization", "Bearer " + token);
        }

        public static async Task<string> GetToken(string resource = @"https://graph.microsoft.com/")
        {
            var clientId = ConfigurationManager.AppSettings["ida:ClientId"];
            var clientKey = ConfigurationManager.AppSettings["ida:ClientSecret"];
            var azureDomain = ConfigurationManager.AppSettings["ida:Domain"];
            var authory = $@"https://login.microsoftonline.com/{azureDomain}";
            var creds = new ClientCredential(clientId, clientKey);
            var authContext = new AuthenticationContext(authory);
            var authResult = await authContext.AcquireTokenAsync(resource, creds);
            return authResult.AccessToken;
        }
    }
}