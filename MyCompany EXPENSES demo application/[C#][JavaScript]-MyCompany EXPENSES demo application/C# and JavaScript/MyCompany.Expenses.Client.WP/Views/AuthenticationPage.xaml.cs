namespace MyCompany.Expenses.Client.WP.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;
    using MyCompany.Expenses.Client.WP.Settings;
    using System.Threading.Tasks;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using MyCompany.Expenses.Client.WP.Resources;
    using System.Text;
    using System.IO;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Authentication page
    /// </summary>
    public partial class AuthenticationPage : PhoneApplicationPage
    {
        WebBrowser SignInBrowser;
        string code = string.Empty;

        ApplicationBarMenuItem settingsMenuItem;

        /// <summary>
        /// Constructor
        /// </summary>
        public AuthenticationPage()
        {
            InitializeComponent();
            GenerateAppBar();
        }

        /// <summary>
        /// This method is called when navigated to the page.
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (SignInBrowser != null)
            {
                await SignInBrowser.ClearCookiesAsync();
                await SignInBrowser.ClearInternetCacheAsync();
                SignInBrowser.Navigating -= SignIn_Navigating;
                placeHolder.Child = null;
            }

            SignInBrowser = new WebBrowser();
            SignInBrowser.IsScriptEnabled = true;
            SignInBrowser.Navigating += SignIn_Navigating;
            SignInBrowser.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            SignInBrowser.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            placeHolder.Child = SignInBrowser;
            if (!AppSettings.TestMode && string.IsNullOrWhiteSpace(AppSettings.SecurityToken))
            {
                SignInBrowser.Navigate(AppSettings.AuthenticationUri);
            }
            else
            {
                NavigateToNextPage();
            }
        }

        /// <summary>
        /// Executed when navigated away the page.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            SignInBrowser.Navigating -= SignIn_Navigating;
            if ((e.Content as MainPage) != null)
            {
                while (App.RootFrame.BackStack.Any())
                    App.RootFrame.RemoveBackEntry();
            }
        }

        /// <summary>
        /// When the web browser control initiate navigation...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignIn_Navigating(object sender, NavigatingEventArgs e)
        {
            string returnURL = e.Uri.ToString();
            if (returnURL.StartsWith(AppSettings.ReplyUri))
            {
                code = e.Uri.Query.Remove(0, 6);
                e.Cancel = true;
                GetToken();
            }
        }

        private void GetToken()
        {
            SignInBrowser.Visibility = Visibility.Collapsed;

            HttpWebRequest hwr =
                WebRequest.Create(
                    string.Format("https://login.windows.net/{0}/oauth2/token", AppSettings.Tenant)) as HttpWebRequest;
            hwr.Method = "POST";
            hwr.ContentType = "application/x-www-form-urlencoded";
            hwr.BeginGetRequestStream(new AsyncCallback(SendTokenEndpointRequest), hwr);
        }

        private void SendTokenEndpointRequest(IAsyncResult rez)
        {
            HttpWebRequest hwr = rez.AsyncState as HttpWebRequest;
            byte[] bodyBits = Encoding.UTF8.GetBytes(
                string.Format(
                    "grant_type=authorization_code&code={0}&client_id={1}&redirect_uri={2}",
                    code,
                    AppSettings.ClientId,
                    HttpUtility.UrlEncode(AppSettings.ReplyUri)));
            Stream st = hwr.EndGetRequestStream(rez);
            st.Write(bodyBits, 0, bodyBits.Length);
            st.Close();
            hwr.BeginGetResponse(new AsyncCallback(RetrieveTokenEndpointResponse), hwr);
        }

        private void RetrieveTokenEndpointResponse(IAsyncResult rez)
        {
            HttpWebRequest hwr = rez.AsyncState as HttpWebRequest;
            HttpWebResponse resp = hwr.EndGetResponse(rez) as HttpWebResponse;

            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string responseString = sr.ReadToEnd();
            JObject jo = JsonConvert.DeserializeObject(responseString) as JObject;
            string token = (string)jo["access_token"];
            if (!string.IsNullOrEmpty(token))
            {
                AppSettings.SecurityToken = string.Format("Bearer {0}", token);
                Dispatcher.BeginInvoke(() =>
                {
                    SignInBrowser.Visibility = Visibility.Visible;
                    NavigateToNextPage();
                }); 
            }
        }

        /// <summary>
        /// Regenerates appbar icons and menu items.
        /// </summary>
        private void GenerateAppBar()
        {
            this.ApplicationBar.MenuItems.Clear();

            this.settingsMenuItem = new ApplicationBarMenuItem();
            this.settingsMenuItem.Text = AppResources.SettingsMenuItemText;
            this.settingsMenuItem.Click += NavTo_Settings;

            this.ApplicationBar.MenuItems.Add(this.settingsMenuItem);
        }

        /// <summary>
        /// settings navigation event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavTo_Settings(object sender, EventArgs e)
        {
            App.RootFrame.Navigate(new Uri("/Views/SettingsPage.xaml", UriKind.Relative));
        }

        private void NavigateToNextPage()
        {
            string expenseId;
            if (NavigationContext.QueryString.TryGetValue("expenseId", out expenseId))
            {
                App.RootFrame.Navigate(new Uri(string.Format("/Views/ExpenseDetailPage.xaml?expenseId={0}", expenseId), UriKind.Relative));
            }
            else
            {
                App.RootFrame.Navigate(new Uri("/Views/MainPage.xaml", UriKind.Relative));
                GenerateAppBar();
            }
        }
    }
}