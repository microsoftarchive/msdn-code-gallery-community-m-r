
namespace MyShuttle.Device.EventHub
{
    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;
    using MyShuttle.Model;
    using Newtonsoft.Json;
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.Text;

    public static class Sender
    {
        static readonly string EventHubName = Properties.Settings.Default.EventHubName;

        public static async void SendEvent(MetricEvent info)
        {
            // Create EventHubClient
            EventHubClient client = EventHubClient.CreateFromConnectionString(GetServiceBusConnectionString(), EventHubName); 

            try
            {
                Trace.WriteLine(String.Format("Sending message to Event Hub {0}", client.Path));

                var serializedString = JsonConvert.SerializeObject(info);
                EventData data = new EventData(Encoding.UTF8.GetBytes(serializedString));

                // Send the metric to Event Hub
                await client.SendAsync(data);

            }
            catch (Exception exp)
            {
                Trace.WriteLine("Error on send: " + exp.Message);
            }

            client.CloseAsync().Wait();
        }

        static void OutputMessageInfo(string action, EventData data, MetricEvent info)
        {
            if (data == null)
            {
                return;
            }

            if (info != null)
            {
                Console.WriteLine("{0}{1} - Device {2}.", action, data, info.DeviceId);
            }
        }

        static string GetServiceBusConnectionString()
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
