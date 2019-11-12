namespace MyCompany.Common.EventBus
{
    using Microsoft.ServiceBus.Messaging;

    /// <summary>
    /// Interface for event bus
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Publish a new event to registered subscribers (evt-handlers)
        /// </summary>
        /// <typeparam name="TEvent">The event type</typeparam>
        /// <param name="event">The event instance</param>
        /// <param name="contentType"></param>
        void Publish<TEvent>(TEvent @event, string contentType);

        /// <summary>
        /// Register Handler
        /// </summary>
        void RegisterHandler();

    }
}
