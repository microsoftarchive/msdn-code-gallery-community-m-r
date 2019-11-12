
namespace MyCompany.Common.EventBus
{
    using Microsoft.ServiceBus.Messaging;

    /// <summary>
    /// Interface for service bus plugins
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// Handle
        /// </summary>
        void Handle(BrokeredMessageEventArgs e);
    }
}
