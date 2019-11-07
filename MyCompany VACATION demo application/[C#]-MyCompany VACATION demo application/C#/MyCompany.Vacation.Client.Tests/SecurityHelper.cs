namespace MyCompany.Vacation.Client.Tests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Configuration;
    using System;
    using Microsoft.IdentityModel.Clients.ActiveDirectory;

    [TestClass]
    public class SecurityHelper
    {
        public static string UrlBase = ConfigurationManager.AppSettings["UrlBase"];
        public static string ODataUrlBase = string.Format("{0}odata", UrlBase);
        public static string AccessToken = string.Empty;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            var dataContext = new Data.MyCompanyContext();
            var employee = dataContext.Employees.First();
            employee.Email = ConfigurationManager.AppSettings["Email"];
            dataContext.SaveChanges();

            var tenant = ConfigurationManager.AppSettings["ida:Tenant"];
            string clientId = ConfigurationManager.AppSettings["ClientId"];
            bool testMode = Boolean.Parse(ConfigurationManager.AppSettings["testMode"]);

            if (testMode)
            {
                UrlBase = UrlBase + "noauth/";
                SecurityHelper.AccessToken = "testmode";
            }
            else
            {
                var authContext = new AuthenticationContext(String.Format("https://login.windows.net/{0}", tenant));
                var credential = authContext.AcquireToken(UrlBase, clientId, new Uri(UrlBase));
                SecurityHelper.AccessToken = credential.CreateAuthorizationHeader();
            }
        }
    }
}
