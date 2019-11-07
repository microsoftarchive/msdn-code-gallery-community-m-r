namespace MyCompany.Travel.Web.Controllers
{
    using MyCompany.Travel.Web.Infraestructure.Security;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IdentityModel.Services;
    using System.IdentityModel.Services.Configuration;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Account Controller
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Signout session.
        /// </summary>
        /// <returns></returns>
        public ActionResult SignOut()
        {
            // Load Identity Configuration
            FederationConfiguration config = FederatedAuthentication.FederationConfiguration;

            // Get wtrealm from WsFederationConfiguation Section
            string wtrealm = config.WsFederationConfiguration.Realm;
            string wsFederationEndpoint = ConfigurationManager.AppSettings["ida:Issuer"];

            SignOutRequestMessage signoutRequestMessage = new SignOutRequestMessage(new Uri(wsFederationEndpoint), wtrealm);
            signoutRequestMessage.Parameters.Add("wtrealm", wtrealm);
            FederatedAuthentication.SessionAuthenticationModule.SignOut();

            string signoutUrl = signoutRequestMessage.WriteQueryString();

            return this.Redirect(signoutUrl);
        }
    }
}
