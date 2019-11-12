

namespace MyCompany.Common.EventBus.Plugin.Dummy
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Text;

    [Export(typeof(IEventBus))]
    public class EventBusDummy : AzureEventBus, IEventBus
    {
        static string topicName = string.Empty;
        static string subscriptionName = string.Empty;
        static string[] readTopics = { };

        public EventBusDummy()
            : base (topicName, subscriptionName, readTopics)
        {
            base.Handler = new HandleDummy();
        }
    }
}
