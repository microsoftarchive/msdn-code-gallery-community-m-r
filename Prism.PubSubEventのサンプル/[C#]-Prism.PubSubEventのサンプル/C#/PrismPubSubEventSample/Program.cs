using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismPubSubEventSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var aggregator = new EventAggregator();

            // イベントの発行を行うPublisherを作成
            var pub = new TimestampPublisher(aggregator);
            // イベントの購読を行うSubscriberを作成
            var sub = new TimestampSubscriber(aggregator);

            // イベントの購読開始
            sub.Subscribe();
            // イベントを発行
            pub.Publish("Hello");
            pub.Publish("world");

            // イベントの購読解除
            sub.Unsbscribe();
            // イベントの発行
            pub.Publish("こんにちは");
            pub.Publish("世界");
        }
    }
}
