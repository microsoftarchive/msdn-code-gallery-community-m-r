using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Net.Http;

using OwinSignalR.Data.Services;
using OwinSignalR.Common;

namespace OwinSignalR.Pulse.Attributes
{
    public class ApiAuthorizeAttribute
        : AuthorizeAttribute
    {
        public IApplicationService ApplicationService { get; set; }

        public ApiAuthorizeAttribute() 
        {
            StructureMap.ObjectFactory.BuildUp(this);
        }

        protected override bool IsAuthorized(
            HttpActionContext actionContext)
        {
            var queryString = actionContext.Request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value);

            string token;
            string applicationSecret;

            if (!queryString.TryGetValue(Constants.TOKEN_QUERYSTRING, out token))
            {
                return false;
            }

            if (!queryString.TryGetValue(Constants.APPLICATION_SECRET, out applicationSecret))
            {
                return false;
            }

            if (String.IsNullOrEmpty(applicationSecret))
            {
                if (!IsValidReferer(token, actionContext))
                {
                    return false;
                }
            }
            else if (!IsApplicationSecretValidForToken(token, applicationSecret))
            {
                return false;
            }

            return true;
        }

        private bool IsApplicationSecretValidForToken(
              string token
            , string applicationSecret)
        {
            var application = ApplicationService.FetchApplication(token);

            if (application == null)
            {
                return false;
            }

            return application.ApplicationSecret == applicationSecret;
        }

        private bool IsValidReferer(
            string token
            , HttpActionContext actionContext)
        {
            var application = ApplicationService.FetchApplication(token);

            if (application == null)
            {
                return false;
            }

            IEnumerable<string> headerValue;

            if (!actionContext.Request.Headers.TryGetValues("Referer", out headerValue))
            {
                return false;
            }
            else
            {
                var urlReferral = Helpers.HttpHelper.GetUrlReferer(headerValue.ElementAt(0));
                return application.ApplicationReferralUrls.Any(x => x.Url == urlReferral);
            }
        }
    }
}
