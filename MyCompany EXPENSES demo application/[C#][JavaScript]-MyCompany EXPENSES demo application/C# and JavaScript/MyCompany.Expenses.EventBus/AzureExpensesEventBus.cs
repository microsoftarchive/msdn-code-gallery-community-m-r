
namespace MyCompany.Expenses.EventBusPlugin
{
    using System.ComponentModel.Composition;
    using MyCompany.Common.EventBus;
    using MyCompany.Expenses.AzureEventBusPlugin;

    /// <summary>
    /// <see cref="MyCompany.Common.EventBus.IEventBus"/>
    /// </summary>
    [Export(typeof(IEventBus))]
    public class AzureExpensesEventBus : AzureEventBus, IEventBus
    {
        static string _expenseTopicName = "Expenses";
        static string _expenseSubscriptionName = "Expenses";
        static string[] _staffTopicName = { "Staff" };

        /// <summary>
        /// Constructor
        /// </summary>
        public AzureExpensesEventBus()
            : base(_expenseTopicName, _expenseSubscriptionName, _staffTopicName)
        {
            base.Handler = new AzureExpensesHandler();
        }

    }
}
