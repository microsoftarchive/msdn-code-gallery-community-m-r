

namespace MyCompany.Common.EventBus.Service
{
    using System;
    using System.ServiceProcess;
    using System.Threading;
    using MyCompany.Common.CrossCutting;
    using System.Collections.Generic;
    using MyCompany.Expenses.EventBusPlugin;
    using MyCompany.Visitors.AzureEventBusPlugin;
    using MyCompany.Travel.EventBusPlugin;
    using MyCompany.Vacation.AzureEventBusPlugin;
    using MyCompany.Staff.AzureEventBusPlugin;

    /// <summary>
    /// Windows Service to handle event bus events
    /// </summary>
    public partial class EventBusService : ServiceBase
    {
        EventBusPluginLoader plugins = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public EventBusService()
        {
            GetSomeTimeForAttachingTheDebugger();

            InitializeComponent();
        }

        /// <summary>
        /// OnStart
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            TraceManager.TraceInfo("OnStart");

            ThreadPool.QueueUserWorkItem(delegate
            {
                plugins = new EventBusPluginLoader();

                IEnumerable<IEventBus> eventBusCollection  = new List<IEventBus>()
                {
                    //new AzureExpensesEventBus(),
                    //new AzureVisitorsEventBus(),
                    //new AzureTravelEventBus(),
                    //new AzureVacationEventBus(),
                    new AzureStaffEventBus(),
                };

                plugins.Initialize(eventBusCollection);
            });
        }

        /// <summary>
        /// OnStop
        /// </summary>
        protected override void OnStop()
        {
            TraceManager.TraceInfo("OnStop");

            if (plugins != null)
                plugins.Dispose();
        }

        [System.Diagnostics.Conditional("DEBUG")]
        private void GetSomeTimeForAttachingTheDebugger()
        {
            System.Threading.Thread.Sleep(15 * 1000);
        }

       
    }
}
