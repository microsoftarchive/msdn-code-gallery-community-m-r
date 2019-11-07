using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MyEvents.Api.Authentication
{
    /// <summary>
    /// Ensure that your Web API is only accessed via HTTPS
    /// </summary>
    public class OnlyHttpsAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// OnActionExecuting
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!String.Equals(actionContext.Request.RequestUri.Scheme, "https", StringComparison.OrdinalIgnoreCase))
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("HTTPS Required")
                };
                return;
            }
        }
    }
}