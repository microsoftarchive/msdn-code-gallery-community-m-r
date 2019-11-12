using System;
using System.Globalization;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using MyEvents.Data;
using MyEvents.Web.Models;
using MyEvents.Web.Services;
using Microsoft.Practices.Unity;

namespace MyEvents.Web.Authentication
{
    /// <summary>
    /// Authorize attribute.
    /// </summary>
    public class AuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// Authentication service dependency.
        /// </summary>
        [Dependency]
        public IAuthenticationService AuthenticationService { get; set; }

        /// <summary>
        /// Called when authorization is required.
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            bool isAuthenticated = AuthenticationService.IsAuthenticated(filterContext.HttpContext);
            string oauthCode = AuthenticationService.GetOauthCode(filterContext.HttpContext);                
            bool hasOauthCode = !string.IsNullOrEmpty(oauthCode);

            if (!isAuthenticated && !hasOauthCode)
            {
                string loginUrl = AuthenticationService.GetLoginUrl(filterContext.RequestContext.HttpContext);
                filterContext.Result = new RedirectResult(loginUrl);
                return;
            }

            if (hasOauthCode)
            {
                AuthenticationService.CreateAuthenticationTicket(oauthCode, filterContext.HttpContext);
            }
        }       
    }
}