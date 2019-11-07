
namespace MyCompany.Expenses.WebApiRole
{
    using Microsoft.WindowsAzure.ServiceRuntime;
    using MyCompany.Common.CrossCutting;
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
    using System.Xml;

    /// <summary>
    /// Security Helper
    /// </summary>
    public class SecurityHelper : ISecurityHelper
    {
        /// <summary>
        /// <see cref="MyCompany.Visitors.Web.ISecurityHelper"/>
        /// </summary>
        /// <returns><see cref="MyCompany.Visitors.Web.ISecurityHelper"/></returns>
        public string GetUser()
        {
            TraceManager.TraceInfo("GetUser");

            var principal = (ClaimsPrincipal)Thread.CurrentPrincipal;
            if (!principal.Identity.IsAuthenticated)
                return string.Empty;

            var nameClaim = principal.Claims
                        .FirstOrDefault(c => c.Type == ClaimTypes.Name || c.Type == ClaimTypes.Upn);

            if (nameClaim != null)
                return nameClaim.Value;

            return RoleEnvironment.GetConfigurationSettingValue("testModeIdentity");
        }
        

        /// <summary>
        /// This function retrieves ACS token (in format of OAuth 2.0 Bearer Token type) from 
        /// the Authorization header in the incoming HTTP request from the ShipperClient.
        /// </summary>
        /// <param name="authzHeader"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool TryRetrieveToken(string authzHeader, out string token)
        {
            token = authzHeader;

            if(string.IsNullOrEmpty(authzHeader))
            {
                return false;
            }

            // Remove the bearer token scheme prefix and return the rest as  token 
            token = token.StartsWith("Bearer ") ? token.Substring(7) : token;
            token = token.StartsWith("Authorization Bearer ") ? token.Substring(21) : token;
            return true;
        }


    }
}