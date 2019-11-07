using System;
using System.Globalization;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using MyEvents.Data;
using System.Linq;
using System.Text;
using System.Web.Configuration;

namespace MyEvents.Api.Authentication
{
    /// <summary>
    /// Authorize attribute.
    /// </summary>
    public class AuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        /// <summary>
        /// OnAuthorization
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            // Validate that the Authorization header has a valid token
            var token = MyEventsToken.GetTokenFromHeader();
            if (token != null && !token.IsExpired())
                return;

            base.OnAuthorization(actionContext);
        }
    }
}