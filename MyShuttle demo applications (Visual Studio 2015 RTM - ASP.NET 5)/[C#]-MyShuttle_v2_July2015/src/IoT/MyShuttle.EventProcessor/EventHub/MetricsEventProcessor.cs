
namespace MyShuttle.EventProcessor
{
    using System.Diagnostics;
    using System.Runtime.Serialization.Json;
    using System.Threading;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.ServiceBus.Messaging;
    using Newtonsoft.Json;
    using MyShuttle.Model;
    using MyShuttle.EventProcessor.EventHub;

    public class MetricsEventProcessor : IEventProcessor
    {
        IDictionary<string, int> map;
        PartitionContext partitionContext;
        Stopwatch checkpointStopWatch;

        public MetricsEventProcessor()
        {
            this.map = new Dictionary<string, int>();
        }

        public Task OpenAsync(PartitionContext context)
        {
            Console.WriteLine(string.Format("MetricsEventProcessor initialize.  Partition: '{0}', Offset: '{1}'", context.Lease.PartitionId, context.Lease.Offset));

            this.partitionContext = context;
            this.checkpointStopWatch = new Stopwatch();
            this.checkpointStopWatch.Start();

            return Task.FromResult<object>(null);
        }

        public async Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> events)
        {
            try
            {
                Notifications notifications = new Notifications();
                foreach (EventData eventData in events)
                {
                    var dataString = Encoding.UTF8.GetString(eventData.GetBytes());
                    var newData = JsonConvert.DeserializeObject<MetricEvent>(dataString);

                    EventMessage message = GetMessage(newData.Type, dataString);
                    if (message == null)
                        message = newData.GetMessage();

                    notifications.Notify(message);

                    Console.WriteLine(string.Format("Message received.Partition:'{0}',{1},Device:'{2}'", this.partitionContext.Lease.PartitionId, newData.Type, newData.DeviceId));
                }

                //Call checkpoint every 5 minutes, so that worker can resume processing from the 5 minutes back if it restarts.
                if (this.checkpointStopWatch.Elapsed > TimeSpan.FromMinutes(5))
                {
                    await context.CheckpointAsync();
                    this.checkpointStopWatch.Restart();
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("Error in processing: " + exp.Message);
            }
        }

        public async Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            Console.WriteLine(string.Format("Processor Shuting Down.  Partition '{0}', Reason: '{1}'.", this.partitionContext.Lease.PartitionId, reason.ToString()));
            if (reason == CloseReason.Shutdown)
            {
                await context.CheckpointAsync();
            }
        }

        EventMessage GetMessage(string dataType, string serialezedData)
        {
            var type = typeof(MetricEvent);

            var newObject = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface)
                .Where(p => (Activator.CreateInstance(p) as MetricEvent)
                .CanHandle(dataType))
                .Select(p => Activator.CreateInstance(p) as MetricEvent)
                .SingleOrDefault();

            if (newObject != null)
            {
                return newObject.GetMessage(serialezedData);
            }

            return null;
        }
    }
}
