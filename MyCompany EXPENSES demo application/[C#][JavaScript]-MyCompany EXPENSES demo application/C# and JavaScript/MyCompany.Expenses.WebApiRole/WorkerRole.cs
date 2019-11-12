using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Owin.Hosting;

namespace MyCompany.Expenses.WebApiRole
{
    // THIS SAMPLE hosts the same WebAPI that MyCompany.Expenses.Web. 
        //  The source code is duplicate to review it easier
    public class WorkerRole : RoleEntryPoint
    {
        private IDisposable _app = null;


        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.TraceInformation("MyCompany.Expenses.WebApiRole entry point called", "Information");

            while (true)
            {
                Thread.Sleep(10000);
                Trace.TraceInformation("Working", "Information");
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            string baseUri = string.Empty;
            if (!RoleEnvironment.IsEmulated)
            {
                var endpoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["WebEndpoint"];
                baseUri = string.Format("{0}://{1}", endpoint.Protocol, endpoint.IPEndpoint);
            }
            else
            {
                baseUri = RoleEnvironment.GetConfigurationSettingValue("Audience");
            }

            Trace.TraceInformation(String.Format("Starting OWIN at {0}", baseUri),
                "Information");

            _app = WebApp.Start<Startup>(new StartOptions(url: baseUri));

            return base.OnStart();
        }

        public override void OnStop()
        {
            if (_app != null)
            {
                _app.Dispose();
            }
            base.OnStop();
        }
    }
}
