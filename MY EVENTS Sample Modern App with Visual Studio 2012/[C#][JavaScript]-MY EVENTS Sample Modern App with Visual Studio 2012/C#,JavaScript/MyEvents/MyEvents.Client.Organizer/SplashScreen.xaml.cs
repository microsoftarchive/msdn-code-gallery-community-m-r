using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyEvents.Client.Organizer.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyEvents.Client.Organizer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SplashScreen : Page
    {
        public SplashScreen()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModelLocator.Register();
            SplashScreenViewModel vm = (this.DataContext as SplashScreenViewModel);

            if (GlobalConfig.IsOfflineMode)
            {
                vm.LoadOfflineUserInformation();
            }
            else
            {
                string accessToken;

                if (ApplicationData.Current.LocalSettings.Values.ContainsKey("Facebook_AccessToken"))
                {
                    accessToken = ApplicationData.Current.LocalSettings.Values["Facebook_AccessToken"].ToString();

                    if (!string.IsNullOrEmpty(accessToken))
                        vm.CheckIfValidUser(accessToken);
                    else
                        vm.FacebookLogin();
                }
                else
                    vm.FacebookLogin();
            }
        }
    }
}
