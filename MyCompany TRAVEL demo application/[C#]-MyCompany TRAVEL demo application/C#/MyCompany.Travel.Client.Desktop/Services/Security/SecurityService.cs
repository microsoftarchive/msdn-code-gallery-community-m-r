namespace MyCompany.Travel.Client.Desktop.Services.Security
{
    using Microsoft.IdentityModel.Clients.ActiveDirectory;
    using System;
    using System.Configuration;
    using System.Net;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// Security Service
    /// </summary>
    public static class SecurityService
    {
        /// <summary>
        /// Url base of the data service
        /// </summary>
        public static string UrlBase = ConfigurationManager.AppSettings["ServiceUrlBase"];
        /// <summary>
        /// Original Url readed from config
        /// </summary>
        public static readonly string OriginalUrl = ConfigurationManager.AppSettings["ServiceUrlBase"];
        private static string clientId = ConfigurationManager.AppSettings["ServiceClientId"];
        private static string testMode = ConfigurationManager.AppSettings["TestMode"]; 

        /// <summary>
        /// Access token retrieved from authentication service
        /// </summary>
        public static string AccessToken = string.Empty;

        /// <summary>
        /// Login
        /// </summary>
        public static void Login()
        {
            // Test mode doens't require authentication
            if (IsTestMode())
            {
                UrlBase = string.Format("{0}noauth/", UrlBase);
                SecurityService.AccessToken = "TestToken";
            }
            else
            {
                var authContext = new AuthenticationContext(ConfigurationManager.AppSettings["AuthUrl"]);
                AuthenticationResult credential = authContext.AcquireToken(UrlBase, clientId, new Uri(UrlBase));
                SecurityService.AccessToken = credential.CreateAuthorizationHeader();
            }
        }

        /// <summary>
        /// Logout
        /// </summary>
        public static async Task Logout()
        {
            if (!string.IsNullOrWhiteSpace(SecurityService.AccessToken))
            {
                try
                {
                    MyCompanyClient client = new MyCompanyClient(SecurityService.UrlBase, SecurityService.AccessToken);
                    var signoutUrl = await client.AccountService.GetLogoutUrl();
                    var webClient = new WebClient();
                    webClient.DownloadString(signoutUrl);
                    SecurityService.AccessToken = string.Empty;
                }
                catch (Exception)
                {
                    // Excessive filter here. We always close the application
                }
            }

            Application.Current.Shutdown();
        }

        /// <summary>
        /// Is Test Mode enabled
        /// </summary>
        /// <returns></returns>
        public static bool IsTestMode()
        {
            return Convert.ToBoolean(testMode);
        }
    }
}
