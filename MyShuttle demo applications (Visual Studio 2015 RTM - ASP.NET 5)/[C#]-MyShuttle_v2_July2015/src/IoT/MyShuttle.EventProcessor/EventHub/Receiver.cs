
namespace MyShuttle.EventProcessor
{
    using Microsoft.ServiceBus.Messaging;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    class Receiver
    {
        string eventHubName;
        EventHubConsumerGroup defaultConsumerGroup;
        string eventHubConnectionString;
        EventProcessorHost eventProcessorHost;
        string consumerGroup;

        public Receiver(string eventHubName, string consumerGroup, string eventHubConnectionString)
        {
            this.eventHubConnectionString = eventHubConnectionString;
            this.eventHubName = eventHubName;
            this.consumerGroup = consumerGroup;
        }

        public void MessageProcessingWithPartitionDistribution()
        {
            EventHubClient eventHubClient = EventHubClient.CreateFromConnectionString(eventHubConnectionString, this.eventHubName);

            // Get the default Consumer Group
            defaultConsumerGroup = eventHubClient.GetConsumerGroup(this.consumerGroup);
            string blobConnectionString =
                ConfigurationManager.AppSettings["AzureStorageConnectionString"]; // Required for checkpoint/state

            eventProcessorHost =
                new EventProcessorHost("singleworker", eventHubClient.Path, defaultConsumerGroup.GroupName, this.eventHubConnectionString, blobConnectionString);

            eventProcessorHost.RegisterEventProcessorAsync<MetricsEventProcessor>().Wait();
        }

        public void UnregisterEventProcessor()
        {
            eventProcessorHost.UnregisterEventProcessorAsync().Wait();
        }
    }
}
