namespace MyCompany.Expenses.Client.WP.Services.Notification
{
    using System;

    /// <summary>
    /// Service to manage push notifications
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Creates a new channel and subscribes to it
        /// </summary>
        void Subscribe();

        /// <summary>
        /// Unsubscribe from the channel
        /// </summary>
        void Unsubscribe();
    }
}
