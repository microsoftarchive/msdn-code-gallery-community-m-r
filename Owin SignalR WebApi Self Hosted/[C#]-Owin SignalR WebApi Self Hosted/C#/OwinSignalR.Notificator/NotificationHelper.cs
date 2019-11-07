using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

using OwinSignalR.Common;

namespace OwinSignalR.Notificator
{
    public static class NotificationHelper
    {
        private static Timer _notificationTimer;

        private static List<string> _possibleMessages = new List<string> { { "Batch process complete" }, { "SQL Indexes rebuild complete" }, { "Integrity check complete" }, { "Temp files deleted" }, { "File ready for processing" }, { "Server now available" }, { "Inactive accounts deactivated" }, { "Welcome emails job complete" }, { "Annual renew program emails sent" } };
        private static Random       _randomMessage    = new Random();

        public static void Start() 
        {
            _notificationTimer = new Timer(OnNotificationTimerCallBack, null, 1000, 2500);
        }

        private static void OnNotificationTimerCallBack(
            object state)
        {
            var service           = new NotificationService();
            var token             = System.Configuration.ConfigurationManager.AppSettings["Token"];
            var applicationSecret = System.Configuration.ConfigurationManager.AppSettings["ApplicationSecret"];

            service.Send(token, applicationSecret, "testUser", "testCallBack", new TestClass
            {
                CreateDate  = DateTime.Now.ToString("yyyy/MM/dd HH:ss"),
                Message     = _possibleMessages.ElementAt(_randomMessage.Next(_possibleMessages.Count - 1))
            });

            Console.WriteLine("Sent {0}", DateTime.Now);
        }

        public class TestClass 
        {
            public string CreateDate { get; set; }
            public string Message    { get; set; }
        }
    }
}
