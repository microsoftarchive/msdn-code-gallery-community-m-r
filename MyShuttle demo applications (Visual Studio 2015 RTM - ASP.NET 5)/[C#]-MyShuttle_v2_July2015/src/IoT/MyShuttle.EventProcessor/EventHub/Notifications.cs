
namespace MyShuttle.EventProcessor.EventHub
{
    using Microsoft.AspNet.SignalR.Client;
    using MyShuttle.Model;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Notifications
    {
        HubConnection _hubConnection;
        IHubProxy _hubProxy;

        public Notifications()
        {
            string SignalRUrl = ConfigurationManager.AppSettings["SignalRUrl"];
            string signalRHubName = ConfigurationManager.AppSettings["SignalRHubName"];
            _hubConnection = new HubConnection(SignalRUrl);
            _hubProxy = _hubConnection.CreateHubProxy(signalRHubName);
            _hubConnection.Start().Wait();
        }

        public void Notify(EventMessage message)
        {
            try
            {
                _hubProxy.Invoke("UpdateEvents", message);
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
