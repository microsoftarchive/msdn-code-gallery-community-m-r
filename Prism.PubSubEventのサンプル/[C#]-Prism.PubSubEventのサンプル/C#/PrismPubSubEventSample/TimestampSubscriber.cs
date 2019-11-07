using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismPubSubEventSample
{
    public class TimestampSubscriber
    {
        private IEventAggregator eventAggregator;
        /// <summary>
        /// EventAggregatorからイベントを購読したときに発行されるトークン
        /// </summary>
        private SubscriptionToken token;

        public TimestampSubscriber(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        /// <summary>
        /// まだ、イベントを購読してないときにイベントを購読する
        /// </summary>
        public void Subscribe()
        {
            if (this.token != null)
            {
                return;
            }

            this.token = this.eventAggregator.GetEvent<PubSubEvent<TimestampPayload>>()
                .Subscribe(this.PrintTimestampPayload);ThreadOption
        }

        /// <summary>
        /// イベントの購読を解除する
        /// </summary>
        public void Unsbscribe()
        {
            if (this.token == null)
            {
                return;
            }

            this.token.Dispose();
            this.token = null;
        }

        /// <summary>
        /// 受け取ったTimestampPayloadを標準出力に出力する
        /// </summary>
        /// <param name="p"></param>
        private void PrintTimestampPayload(TimestampPayload p)
        {
            Console.WriteLine("{0}: {1}", p.Timestamp, p.Message);
        }

    }
}
