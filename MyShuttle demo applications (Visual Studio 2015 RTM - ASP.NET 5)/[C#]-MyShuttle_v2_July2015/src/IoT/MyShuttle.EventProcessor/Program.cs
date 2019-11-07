using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using System.Configuration;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using MyShuttle.EventProcessor.EventHub;
using MyShuttle.Model;

namespace MyShuttle.EventProcessor
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            string eventHubName = ConfigurationManager.AppSettings["EventHubName"];
            string consumerGroup = ConfigurationManager.AppSettings["ConsumerGroup"];

            string connectionString = GetServiceBusConnectionString();
            NamespaceManager namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
            EventHubDescription ehd = namespaceManager.GetEventHub(eventHubName);
            namespaceManager.CreateConsumerGroupIfNotExists(ehd.Path, consumerGroup);

            var receiver = new Receiver(eventHubName, consumerGroup, connectionString);
            receiver.MessageProcessingWithPartitionDistribution();

            // The following code ensures that the WebJob will be running continuously
            var host = new JobHost();
            host.RunAndBlock();
        }

        private static string GetServiceBusConnectionString()
        {
            string connectionString = ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];

            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("Did not find Service Bus connections string in appsettings (app.config)");
                return string.Empty;
            }

            ServiceBusConnectionStringBuilder builder = new ServiceBusConnectionStringBuilder(connectionString);
            builder.TransportType = TransportType.Amqp;
            return builder.ToString();
        }
    }
}
