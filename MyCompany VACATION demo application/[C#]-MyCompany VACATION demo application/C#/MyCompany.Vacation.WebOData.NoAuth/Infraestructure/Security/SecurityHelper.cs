namespace MyCompany.Vacation.Web
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.IdentityModel.Metadata;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Security.Claims;
    using System.Security.Cryptography.X509Certificates;
    using System.Security.Principal;
    using System.ServiceModel.Security;
    using System.Threading;
    using System.Web;
    using System.Web.Configuration;
    using System.Xml;

    /// <summary>
    /// Security Helper
    /// </summary>
    public class SecurityHelper : ISecurityHelper
    {
        /// <summary>
        /// <see cref="MyCompany.Vacation.Web.ISecurityHelper"/>
        /// </summary>
        /// <returns><see cref="MyCompany.Vacation.Web.ISecurityHelper"/></returns>
        public string GetUser()
        {
            // Test Mode is only for demos without internet, not for real apps!
            return System.Configuration.ConfigurationManager.AppSettings["testModeIdentity"];
        }

    }
}