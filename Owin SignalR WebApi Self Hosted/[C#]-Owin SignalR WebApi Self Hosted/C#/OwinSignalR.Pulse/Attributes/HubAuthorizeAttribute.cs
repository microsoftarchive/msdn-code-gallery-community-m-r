using System;
using System.Linq;

using Microsoft.AspNet.SignalR;

using OwinSignalR.Common;
using OwinSignalR.Data.Services;

namespace OwinSignalR.Pulse.Attributes
{
    public class HubAuthorizeAttribute
        : AuthorizeAttribute
    {
        public IApplicationService ApplicationService { get; set; }

        public HubAuthorizeAttribute() 
        {
            StructureMap.ObjectFactory.BuildUp(this);
        }

        public override bool AuthorizeHubConnection(
              Microsoft.AspNet.SignalR.Hubs.HubDescriptor hubDescriptor
            , IRequest request)
        {
            var authorizeHubConnection = base.AuthorizeHubConnection(hubDescriptor, request);

            var tokenValues    = request.QueryString.GetValues(Constants.TOKEN_QUERYSTRING);
            var clientIdValues = request.QueryString.GetValues(Constants.UNIQUE_CLIENT_ID);

            if ((tokenValues.Count() == 0 || tokenValues.Count() > 1) || (clientIdValues.Count() == 0 || clientIdValues.Count() > 1))
            {
                return false;
            }

            var application = ApplicationService.FetchApplication(tokenValues.ElementAt(0));

            if (application == null)
            {
                return false;
            }

            var urlReferer = GetUrlReferer(request);

            return application.ApplicationReferralUrls.Any(x => x.Url == urlReferer);
        }

        protected override bool UserAuthorized(
            System.Security.Principal.IPrincipal user)
        {
            return true;
        }

        private string GetUrlReferer(
            IRequest request)
        {
            return Helpers.HttpHelper.GetUrlReferer(request.Headers["Referer"]);
        }
    }
}
