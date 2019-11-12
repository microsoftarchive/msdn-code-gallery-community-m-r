
namespace MyCompany.Visitors.AzureEventBusPlugin
{
    using System.ComponentModel.Composition;
    using MyCompany.Common.EventBus;
    using MyCompany.Visitors.AzureEventBusPlugin;
    using System;

    /// <summary>
    /// <see cref="MyCompany.Common.EventBus.IEventBus"/>
    /// </summary>
    [Export(typeof(IEventBus))]
    public class AzureVisitorsEventBus : AzureEventBus, IEventBus
    {
        static string _visitorsSubscriptionName = "Visitors";
        static string[] _staffTopicName = { "Staff" };

        /// <summary>
        /// Constructor
        /// </summary>
        public AzureVisitorsEventBus()
            : base(string.Empty, _visitorsSubscriptionName, _staffTopicName)
        {
            base.Handler = new AzureVisitorsHandler();
        }

    }
}
