namespace MyCompany.Common.EventBus.Worker
{
    using System.Net;
    using System.Threading;
    using Microsoft.WindowsAzure.ServiceRuntime;
    using System.Collections.Generic;
    using MyCompany.Expenses.EventBusPlugin;
    using MyCompany.Visitors.AzureEventBusPlugin;
    using MyCompany.Travel.EventBusPlugin;
    using MyCompany.Vacation.AzureEventBusPlugin;
    using MyCompany.Staff.AzureEventBusPlugin;

    /// <summary>
    /// Role Entry Point
    /// </summary>
    public class WorkerRole : RoleEntryPoint
    {
        bool IsStopped;

        /// <summary>
        /// Run
        /// </summary>
        public override void Run()
        {
            EventBusPluginLoader plugins = new EventBusPluginLoader();

            ThreadPool.QueueUserWorkItem(delegate
            {
                plugins = new EventBusPluginLoader();

                IEnumerable<IEventBus> eventBusCollection = new List<IEventBus>()
                {
                    new AzureExpensesEventBus(),
                    new AzureVisitorsEventBus(),
                    new AzureTravelEventBus(),
                    new AzureVacationEventBus(),
                    new AzureStaffEventBus(),
                };

                plugins.Initialize(eventBusCollection);
            });

            while (!IsStopped)
            {
                Thread.Sleep(10000);
            }
        }
        /// <summary>
        /// On Start
        /// </summary>
        /// <returns></returns>
        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            IsStopped = false;
            return base.OnStart();
        }

        /// <summary>
        /// On Stop
        /// </summary>
        public override void OnStop()
        {
            // Close the connection to Service Bus Queue
            IsStopped = true;
            base.OnStop();
        }

       
    }
}
