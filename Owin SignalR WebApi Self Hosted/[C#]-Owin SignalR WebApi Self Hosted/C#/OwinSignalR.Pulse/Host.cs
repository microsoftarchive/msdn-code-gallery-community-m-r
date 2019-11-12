using System;

using Microsoft.Owin.Hosting;

namespace OwinSignalR.Pulse
{
    public class Host
    {
        private static IDisposable _webServer;

        public static void Start()
        {
            var url = System.Configuration.ConfigurationManager.AppSettings["PulseUrl"];
            _webServer = WebApp.Start(url);
            Console.WriteLine(String.Format("OwinSignalR notifications now running on {0}", url));
        }

        public static void Stop()
        {
            if (_webServer != null)
            {
                _webServer.Dispose();
            }
        }
    }
}
