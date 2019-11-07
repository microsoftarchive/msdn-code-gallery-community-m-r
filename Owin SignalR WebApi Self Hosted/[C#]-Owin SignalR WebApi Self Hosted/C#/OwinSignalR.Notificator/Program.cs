using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

using OwinSignalR.Common;

namespace OwinSignalR.Notificator
{
    class Program
    {
        public INotificationService NotificationService { get; set; }

        static void Main(string[] args)
        {
            Console.ReadLine();
            NotificationHelper.Start();
            ServerLoadHelper.Start();      
            Console.ReadLine();
        }
    }
}
