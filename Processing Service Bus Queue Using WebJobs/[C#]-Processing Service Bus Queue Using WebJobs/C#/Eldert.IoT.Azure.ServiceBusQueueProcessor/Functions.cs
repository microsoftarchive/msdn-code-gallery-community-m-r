using System;
using System.IO;

using Eldert.IoT.Data.DataTypes;

using Microsoft.Azure.WebJobs;
using Microsoft.ServiceBus.Messaging;

namespace Eldert.IoT.Azure.ServiceBusQueueProcessor
{
    public class Functions
    {
        private static readonly IoTDatabaseContext database = new IoTDatabaseContext();

        /// <summary>
        /// This function will get triggered/executed when a new message is written on an Azure Service Bus Queue.
        /// </summary>
        public static void ProcessQueueMessage([ServiceBusTrigger("queueerrorsandwarnings")] BrokeredMessage message, TextWriter log)
        {
            try
            {
                log.WriteLine($"Processing message: {message.Properties["exceptionmessage"]} Ship: {message.Properties["ship"]}");

                // Add the message we received from our queue to the database
                database.ErrorAndWarningsEntries.Add(new ErrorAndWarning()
                {
                    CreatedDateTime = DateTime.Parse(message.Properties["time"].ToString()),
                    ShipName = message.Properties["ship"].ToString(),
                    Message = message.Properties["exceptionmessage"].ToString()
                });

                // Save changes in the database
                database.SaveChanges();
            }
            catch (Exception exception)
            {
                log.WriteLine($"Exception in ProcessQueueMessage: {exception}");
            }
        }
    }
}
