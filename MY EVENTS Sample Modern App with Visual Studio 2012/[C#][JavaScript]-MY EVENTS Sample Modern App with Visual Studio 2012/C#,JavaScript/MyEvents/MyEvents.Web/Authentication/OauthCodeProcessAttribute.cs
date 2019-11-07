using System.Security.Principal;
using System.Web.Mvc;
using MyEvents.Data;
using MyEvents.Web.Services;
using Microsoft.Practices.Unity;

namespace MyEvents.Web.Authentication
{
    /// <summary>
    /// Class to process facebook return requests.
    /// </summary>
    public class OauthCodeProcessAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Injected authentication service.
        /// </summary>
        [Dependency]
        public AuthenticationService  AuthenticationService { get; set; }

        /// <summary>
        /// On action executing.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string oauthCode = AuthenticationService.GetOauthCode(filterContext.HttpContext);
            bool hasOauthCode = !string.IsNullOrEmpty(oauthCode);
            if (hasOauthCode)
            {
                AuthenticationService.CreateAuthenticationTicket(oauthCode, filterContext.HttpContext);
            }
        }
    }
}