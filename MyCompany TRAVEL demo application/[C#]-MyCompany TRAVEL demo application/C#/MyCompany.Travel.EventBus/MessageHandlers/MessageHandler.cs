namespace MyCompany.Travel.AzureEventBusPlugin.MessageHandlers
{
    using Microsoft.ServiceBus.Messaging;

    /// <summary>
    /// Represent the message handler
    /// </summary>
    public abstract class MessageHandler
    {
        /// <summary>
        /// Check if this handler can execute some message
        /// </summary>
        /// <param name="message">The message to check</param>
        /// <returns></returns>
        public abstract bool CanExecute(BrokeredMessage message);

        /// <summary>
        /// Execute a message
        /// </summary>
        /// <param name="message">The message to execute</param>
        public abstract void Handle(BrokeredMessage message);
    }
}
