using Microsoft.Practices.Prism.PubSubEvents;
using System;

namespace PrismPubSubEventSample
{
    public class TimestampPublisher
    {
        private IEventAggregator eventAggregator;

        public TimestampPublisher(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        /// <summary>
        /// イベントを発行する
        /// </summary>
        public void Publish(string message)
        {
            this.eventAggregator
                .GetEvent<PubSubEvent<TimestampPayload>>()
                .Publish(
                    new TimestampPayload 
                    { 
                        Timestamp = DateTimeOffset.Now, 
                        Message = message
                    });
        }
    }
}
