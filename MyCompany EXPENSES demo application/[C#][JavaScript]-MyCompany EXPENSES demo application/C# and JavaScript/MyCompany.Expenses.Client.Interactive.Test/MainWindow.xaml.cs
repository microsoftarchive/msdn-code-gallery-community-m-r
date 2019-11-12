using System;
using System.Collections.Generic;
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
using Microsoft.WindowsAzure.ActiveDirectory.Authentication;

namespace MyCompany.Expenses.Client.Interactive.Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Test();

        }

        async Task Test()
        {

            //// Create an AAL AuthenticationContext object and link it to the tenant backing the ShipperService
            //AuthenticationContext context = new AuthenticationContext("https://login.windows.net/mycompanysuite.onmicrosoft.com");

            //AssertionCredential credential = context.AcquireToken("http://localhost:31329/", "http://localhost:31329/");

            //// Create an OAuth2 Bearer token from the AssertionCredential                        
            ////string accessToken = credential.CreateAuthorizationHeader();

            ////var service = new MyCompanyClient("http://localhost:31329/", accessToken);
            ////var result = await service.EmployeeService.GetLoggedEmployeeInfo(PictureType.Small);

        }
    }
}
