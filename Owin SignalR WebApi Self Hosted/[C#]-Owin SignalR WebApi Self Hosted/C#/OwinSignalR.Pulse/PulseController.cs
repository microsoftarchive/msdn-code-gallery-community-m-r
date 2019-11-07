using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using OwinSignalR.Data.Services;
using OwinSignalR.Pulse.Attributes;

using Microsoft.AspNet.SignalR.Hubs;

namespace OwinSignalR.Pulse
{
    [ApiAuthorize]
    public class PulseController
        : ApiController
    {
        public IApplicationService ApplicationService { get; set; }

        public PulseController() 
        {
            StructureMap.ObjectFactory.BuildUp(this);
        }        
        
        [HttpPost]
        public IHttpActionResult Connect(
              string token
            , string clientId
            , string method            
            , [System.Web.Http.FromBody]object value
            , string applicationSecret = "")
        {
            try
            {   
                InvokeClientHubMethod(token, clientId, method, value);
                return Ok();
            }
            catch (Exception excp)
            {
                Startup.Logger.Error("Error on PulseController.Connect", excp);
                return InternalServerError(excp);
            }            
        }

        #region Private Methods
        private void InvokeClientHubMethod(
              string token
            , string clientId
            , string method
            , params object[] args)
        {
            var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<Pulse.PulseHub>();
            IClientProxy clientProxy;

            if (String.IsNullOrEmpty(clientId))
            {
                clientProxy = context.Clients.All;
            }
            else
            {
                clientProxy = context.Clients.Clients(ConnectionManager.ActiveConnections(token, clientId).Connections.Select(x => x.ConnectionId).ToList());
            }

            clientProxy.Invoke(method, args);
        }        
        #endregion
    }
}
