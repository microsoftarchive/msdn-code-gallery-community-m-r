namespace MyCompany.Expenses.Client.WP.Settings
{
    using GalaSoft.MvvmLight.Messaging;
    using Microsoft.Phone.Controls;
    using Microsoft.Practices.ServiceLocation;
    using MyCompany.Expenses.Client.WP.Messages;
    using MyCompany.Expenses.Client.WP.ViewModel.Base;
    using System;
    using System.Collections.Generic;
    using System.IO.IsolatedStorage;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class to store application settings across user session.
    /// </summary>
    public static class AppSettings
    {

        /// <summary>
        /// WAAD tenant
        /// </summary>
        public static string Tenant
        {
            get
            {
                return "[tenantname].onmicrosoft.com";
            }
        }

        /// <summary>
        /// Uri for authentication
        /// </summary>
        public static Uri AuthenticationUri
        {
            get
            {
                string authUri;
                if (!IsolatedStorageSettings.ApplicationSettings.Contains("authUri"))
                {
                    authUri = string.Format(
                            "https://login.windows.net/{0}/oauth2/authorize?response_type=code&resource={1}&client_id={2}&redirect_uri={3}",
                            Tenant,
                            ApiUri,
                            ClientId,
                            ReplyUri);

                    IsolatedStorageSettings.ApplicationSettings["authUri"] = authUri;
                }
                else
                    authUri = IsolatedStorageSettings.ApplicationSettings["authUri"].ToString();

                return new Uri(authUri);
            }
        }

        /// <summary>
        /// Api uri
        /// </summary>
        public static Uri ApiUri
        {
            get
            {
                string apiUri;
                if (!IsolatedStorageSettings.ApplicationSettings.Contains("apiUri"))
                {
                    apiUri = "http://[server]/MyCompany.Expenses.Web/";
                    IsolatedStorageSettings.ApplicationSettings["apiUri"] = apiUri;
                }
                else
                {
                    apiUri = IsolatedStorageSettings.ApplicationSettings["apiUri"].ToString();
                }

                if (!apiUri.EndsWith("/"))
                    apiUri = string.Format("{0}/", apiUri);

                return new Uri(apiUri);
            }
        }

        /// <summary>
        /// Test mode
        /// </summary>
        public static bool TestMode
        {
            get
            {
                if (IsolatedStorageSettings.ApplicationSettings.Contains("testMode"))
                {
                    bool isTest;
                    bool.TryParse(IsolatedStorageSettings.ApplicationSettings["testMode"].ToString(), out isTest);
                    return isTest;
                }

                return false;
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["testMode"] = value.ToString();
            }
        }

        /// <summary>
        /// Reply Uri for authentication process
        /// </summary>
        public static string ReplyUri
        {
            get
            {
                return "http://localhost:31329/";
            }
        }

        /// <summary>
        /// Client Id for authentication process
        /// </summary>
        public static string ClientId
        {
            get
            {
                return "[clientId]";
            }
        }

        /// <summary>
        /// Authenticated security token
        /// </summary>
        public static string SecurityToken 
        {
            get
            {
                if (IsolatedStorageSettings.ApplicationSettings.Contains("token"))
                    return IsolatedStorageSettings.ApplicationSettings["token"].ToString();

                return string.Empty;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    IsolatedStorageSettings.ApplicationSettings["token"] = value;
            }
        }

        /// <summary>
        /// Authenticated employee information
        /// </summary>
        public static Employee EmployeeInformation { get; set; }

        /// <summary>
        /// Per kilometer amount to calculate travel amount.
        /// </summary>
        public static double AmountPerKilometer
        {
            get { return 0.25; }
        }

        /// <summary>
        /// Save settings and reset token
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="testMode"></param>
        public static void SaveSettingsInfo(string apiUrl, bool testMode)
        {
            if (!string.IsNullOrWhiteSpace(apiUrl) && IsolatedStorageSettings.ApplicationSettings["apiUri"].ToString() != apiUrl.Trim())
                IsolatedStorageSettings.ApplicationSettings["apiUri"] = apiUrl.ToLower().Trim();

            IsolatedStorageSettings.ApplicationSettings["testMode"] = testMode.ToString();
            IsolatedStorageSettings.ApplicationSettings["token"] = string.Empty;

            IsolatedStorageSettings.ApplicationSettings.Save();

            while (App.RootFrame.BackStack.Any())
                App.RootFrame.RemoveBackEntry();

            App.RootFrame.Navigate(new Uri("/Views/AuthenticationPage.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Sign out from the application.
        /// </summary>
        public static async void SignOut()
        {
            var client = ServiceLocator.Current.GetInstance<IMyCompanyClient>();
            string logoutUrl = await client.SecurityService.GetLogoutUrl();
            logoutUrl = WebUtility.UrlDecode(logoutUrl);
            IsolatedStorageSettings.ApplicationSettings.Remove("token");
            EmployeeInformation = null;

            while (App.RootFrame.BackStack.Any())
                App.RootFrame.RemoveBackEntry();

            if (!string.IsNullOrEmpty(logoutUrl))
            {
                WebClient c = new WebClient();
                c.DownloadStringAsync(new Uri(logoutUrl));
                c.DownloadStringCompleted += (s, e) =>
                {
                    App.RootFrame.Navigate(new Uri("/Views/AuthenticationPage.xaml", UriKind.Relative));
                };
            }
        }

        /// <summary>
        /// Get logged user information
        /// </summary>
        /// <param name="myCompanyClient">Service client</param>
        public static async Task GetLoggedUserInformation(IMyCompanyClient myCompanyClient)
        {
            var info = await myCompanyClient.EmployeeService.GetLoggedEmployeeInfo(PictureType.Small);
            if (info != null)
            {
                AppSettings.EmployeeInformation = info;

                if (info.IsManager)
                {
                    Messenger.Default.Send<TeamManagerMessage>(new TeamManagerMessage());
                }
            }
        }
    }
}
