namespace MyCompany.Travel.EventBusPlugin
{
    using System.ComponentModel.Composition;
    using MyCompany.Common.EventBus;
    using MyCompany.Travel.AzureEventBusPlugin;

    /// <summary>
    /// <see cref="MyCompany.Common.EventBus.IEventBus"/>
    /// </summary>
    [Export(typeof(IEventBus))]
    public class AzureTravelEventBus : AzureEventBus, IEventBus
    {
        static string _travelSubscriptionName = "Travel";
        static string[] _staffTopicName = { "Staff" };

        /// <summary>
        /// Constructor
        /// </summary>
        public AzureTravelEventBus()
            : base(string.Empty, _travelSubscriptionName, _staffTopicName)
        {
            base.Handler = new AzureTravelHandler();
        }

    }
}
