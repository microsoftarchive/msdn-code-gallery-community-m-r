using System;

using Microsoft.AspNet.SignalR;

using OwinSignalR.Common;
using OwinSignalR.Pulse.Attributes;

namespace OwinSignalR.Pulse
{
    [HubAuthorize]
    public class PulseHub
        : Hub
    {
        #region Private Properties
        private string Token
        {
            get
            {
                var token = Context.QueryString[Constants.TOKEN_QUERYSTRING];

                if (String.IsNullOrEmpty(token))
                {
                    throw new UnauthorizedAccessException("Token not specified");
                }

                return token;
            }
        }

        private string ClientId
        {
            get
            {
                var clientId = Context.QueryString[Common.Constants.UNIQUE_CLIENT_ID];

                if (String.IsNullOrEmpty(clientId))
                {
                    throw new UnauthorizedAccessException("ClientId not specified");
                }

                return clientId;
            }
        }
        #endregion

        #region IHub Interface
        public override System.Threading.Tasks.Task OnConnected()
        {
            try
            {
                Console.WriteLine(Context.ConnectionId);

#if DEBUG
                Startup.Logger.Info(String.Format("OnConnected: Token {0} ClientId {1}", Token, ClientId));
#endif

                ConnectionManager.Add(Token, ClientId, Context.ConnectionId);
            }
            catch (Exception excp)
            {
                Startup.Logger.Error(excp.Message);
            }

            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected(
            bool stopCalled)
        {
            try
            {
#if DEBUG
                Startup.Logger.Info(String.Format("SignalR OnDisconnected: Token {0} ClientId {1}", Token, ClientId));
#endif
                ConnectionManager.Remove(Token, ClientId, Context.ConnectionId);
            }
            catch (Exception excp)
            {
                Startup.Logger.Error(excp.Message);
            }

            return base.OnDisconnected(stopCalled);
        }
        #endregion    
    }
}
