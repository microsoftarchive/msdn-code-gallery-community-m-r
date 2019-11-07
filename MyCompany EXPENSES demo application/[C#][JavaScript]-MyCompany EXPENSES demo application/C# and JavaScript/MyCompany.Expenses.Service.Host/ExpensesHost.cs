

namespace MyCompany.Expenses.Service.Host
{
    using MyCompany.Common.CrossCutting;
    using System.ServiceProcess;
    using System.Threading;
    using Microsoft.Owin.Hosting;
    using System;
    using System.Net;
    using System.Configuration;

    /// <summary>
    /// Host expenses webapi
    /// </summary>
    public partial class ExpensesHost : ServiceBase
    {
        private IDisposable _webApp = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public ExpensesHost()
        {
            InitializeComponent();
        }

        /// <summary>
        /// OnStart
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            TraceManager.TraceInfo("OnStart");

            ServicePointManager.DefaultConnectionLimit = 12;
            _webApp = WebApp.Start<Startup>(url: ConfigurationManager.AppSettings["baseUri"]);
            

        }


        /// <summary>
        /// OnStop
        /// </summary>
        protected override void OnStop()
        {
            TraceManager.TraceInfo("OnStop");

            if (_webApp != null)
            {
                _webApp.Dispose();
            }

        }
    }
}
