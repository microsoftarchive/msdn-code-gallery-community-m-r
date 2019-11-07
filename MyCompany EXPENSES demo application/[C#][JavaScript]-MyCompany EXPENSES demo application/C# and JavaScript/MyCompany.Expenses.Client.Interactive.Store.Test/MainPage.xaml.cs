using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Preview.WindowsAzure.ActiveDirectory.Authentication;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyCompany.Expenses.Client.Interactive.Store.Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Test();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        async Task Test()
        {
            AuthenticationContext authenticationContext = new AuthenticationContext("https://login.windows.net/mycompanysuite.onmicrosoft.com");

            AuthenticationResult result = await authenticationContext.AcquireTokenAsync("http://localhost:31329/", "http://localhost:31329/");

            // Create an OAuth2 Bearer token from the AssertionCredential                        
            //string accessToken = credential.CreateAuthorizationHeader();

            var service = new MyCompanyClient("http://localhost:31329/", result.AccessToken);
            var employee = await service.EmployeeService.GetLoggedEmployeeInfo(PictureType.Small);

        }
    }
}
