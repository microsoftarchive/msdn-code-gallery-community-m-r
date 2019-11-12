using OwinSignalR.Common;
using System;
using System.Threading;

namespace OwinSignalR.Notificator
{
    public class ServerLoadHelper
    {
        private static Timer _serverLoadMemoryTimer;
        private static Timer _serverLoadCPUTimer;
        private static Timer _serverLoadNetworkTimer;

        private static Random _serverLoad = new Random();            

        public static void Start() 
        { 
            _serverLoadMemoryTimer  = new Timer(OnServerLoadMemoryTimer , null, 1000, 1000);
            _serverLoadCPUTimer     = new Timer(OnServerLoadCPUTimer    , null, 1000, 1700);
            _serverLoadNetworkTimer = new Timer(OnServerLoadNetworkTimer, null, 1000, 2300);
        }

        private static void OnServerLoadNetworkTimer(
            object state)
        {
            var service           = new NotificationService();
            var token             = System.Configuration.ConfigurationManager.AppSettings["Token"];
            var applicationSecret = System.Configuration.ConfigurationManager.AppSettings["ApplicationSecret"];

            service.Send(token, applicationSecret, "testUser", "serverLoadNetwork", _serverLoad.Next(100));
        }

        private static void OnServerLoadCPUTimer(
            object state)
        {
            var service           = new NotificationService();
            var token             = System.Configuration.ConfigurationManager.AppSettings["Token"];
            var applicationSecret = System.Configuration.ConfigurationManager.AppSettings["ApplicationSecret"];

            service.Send(token, applicationSecret, "testUser", "serverLoadCPU", _serverLoad.Next(100));
        }

        private static void OnServerLoadMemoryTimer(
            object state)
        {
            var service           = new NotificationService();
            var token             = System.Configuration.ConfigurationManager.AppSettings["Token"];
            var applicationSecret = System.Configuration.ConfigurationManager.AppSettings["ApplicationSecret"];

            service.Send(token, applicationSecret, "testUser", "serverLoadMemory", _serverLoad.Next(100));
        }
    }
}
