using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Facebook;
using GalaSoft.MvvmLight.Ioc;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Desktop.Services.Facebook;
using MyEvents.Client.Organizer.Desktop.ViewModel;

namespace MyEvents.Client.Organizer.Desktop.Views.UserControls
{
    /// <summary>
    /// Interaction logic for FacebookLogin.xaml
    /// </summary>
    public partial class FacebookLogin : UserControl
    {
        FacebookApi fbapi = new FacebookApi(ConfigurationManager.AppSettings.Get("FacebookAppId"), ConfigurationManager.AppSettings.Get("FacebookAppPermissions"));
        IMyEventsClient _myEventsClient = SimpleIoc.Default.GetInstance<IMyEventsClient>();
        /// <summary>
        /// Constructor
        /// </summary>
        public FacebookLogin()
        {
            InitializeComponent();

            this.IsVisibleChanged += FacebookLogin_IsVisibleChanged;

        }

        void FacebookLogin_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ViewModelLocator.Register();
            bool offline = false;
            bool.TryParse(ConfigurationManager.AppSettings.Get("OfflineMode"), out offline);

            if (!offline)
            {
                if ((bool)e.NewValue)
                {
                    progressBarLogin.Visibility = System.Windows.Visibility.Visible;
                    fbapi = new FacebookApi(ConfigurationManager.AppSettings.Get("FacebookAppId"), ConfigurationManager.AppSettings.Get("FacebookAppPermissions"));
                    var loginUrl = fbapi.GetFacebookLoginUrl();
                    fbLoginBrowser.Navigate(loginUrl);
                }
            }
            else
            {
                _myEventsClient.AuthenticationService.GetFakeAuthorizationAsync((result) =>
                {
                    Credentials.UserCredentials.Current.UserId = result.RegisteredUserId;
                    Credentials.UserCredentials.Current.FacebookToken = "";
                    Credentials.UserCredentials.Current.FacebookId = result.FacebookUserId;
                    Credentials.UserCredentials.Current.MyEventsToken = result.Token;
                    Credentials.UserCredentials.Current.FullName = result.UserName;

                    _myEventsClient.SetAccessToken(Credentials.UserCredentials.Current.MyEventsToken);
                    Dispatcher.Invoke(() =>
                    {
                        progressBarLogin.Visibility = System.Windows.Visibility.Collapsed;
                        (this.DataContext as MainPageViewModel).ShowFacebookLogin = false;
                    });
                });
            }
        }

        private async void fbLoginBrowser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            string accessToken = ConfigurationManager.AppSettings.Get("FacebookAccessToken");

            if (string.IsNullOrEmpty(accessToken))
                accessToken = fbapi.GetFacebookLoginResponse(e.Uri);

            if (!string.IsNullOrEmpty(accessToken))
            {
                ConfigurationManager.AppSettings.Set("FacebookAccessToken", accessToken);

                var result = await fbapi.GetLogedUserInfoAsync(accessToken);
                Credentials.UserCredentials.Current.FullName = result["name"].ToString();

                _myEventsClient.AuthenticationService.LogOnAsync(accessToken, (UserResult) =>
                {
                    if (UserResult != null)
                    {
                        Credentials.UserCredentials.Current.UserId = UserResult.RegisteredUserId;
                        Credentials.UserCredentials.Current.FacebookToken = accessToken;
                        Credentials.UserCredentials.Current.MyEventsToken = UserResult.Token;
                        Credentials.UserCredentials.Current.FacebookId = UserResult.FacebookUserId;

                        _myEventsClient.SetAccessToken(Credentials.UserCredentials.Current.MyEventsToken);
                        Dispatcher.Invoke(() =>
                        {
                            progressBarLogin.Visibility = System.Windows.Visibility.Collapsed;
                            (this.DataContext as MainPageViewModel).ShowFacebookLogin = false;
                        });
                    }
                    else
                    {
                        Dispatcher.Invoke(() =>
                        {
                            fbapi.LogOut();
                            ConfigurationManager.AppSettings.Set("FacebookAccessToken", string.Empty);
                            FacebookLogin_IsVisibleChanged(this, new DependencyPropertyChangedEventArgs(VisibilityProperty, true, true));
                        });
                    }
                });
            }
        }
    }
}
