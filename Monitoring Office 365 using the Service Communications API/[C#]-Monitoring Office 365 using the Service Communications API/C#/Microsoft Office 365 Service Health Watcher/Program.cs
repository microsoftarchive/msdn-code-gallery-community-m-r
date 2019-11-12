using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft_Office_365_Service_Health_Watcher {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main() {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new O365ServiceHealthWatcher() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
