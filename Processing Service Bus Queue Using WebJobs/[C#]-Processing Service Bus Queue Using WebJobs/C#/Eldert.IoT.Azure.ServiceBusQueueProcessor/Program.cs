using Microsoft.Azure.WebJobs;

namespace Eldert.IoT.Azure.ServiceBusQueueProcessor
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            // Create job host configuration
            var config = new JobHostConfiguration();

            // Tell configuration we want to use Azure Service Bus
            config.UseServiceBus();

            // Add the configuration to the job host
            var host = new JobHost(config);

            // The following code ensures that the WebJob will be running continuously
            host.RunAndBlock();
        }
    }
}
