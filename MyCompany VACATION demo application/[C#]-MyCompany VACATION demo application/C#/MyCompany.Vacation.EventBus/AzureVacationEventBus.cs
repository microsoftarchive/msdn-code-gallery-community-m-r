
namespace MyCompany.Vacation.AzureEventBusPlugin
{
    using System.ComponentModel.Composition;
    using MyCompany.Common.EventBus;
    using MyCompany.Vacation.AzureEventBusPlugin;
    using System;

    /// <summary>
    /// <see cref="MyCompany.Common.EventBus.IEventBus"/>
    /// </summary>
    [Export(typeof(IEventBus))]
    public class AzureVacationEventBus : AzureEventBus, IEventBus
    {
        static string _vacationTopicName = "vacation";
        static string _vacationSubscriptionName = "vacation";
        static string[] _staffTopicName = { "Staff" };

        /// <summary>
        /// Constructor
        /// </summary>
        public AzureVacationEventBus()
            : base(_vacationTopicName, _vacationSubscriptionName, _staffTopicName)
        {

            Handler = new AzureVacationHandler();
        }

    }
}
