namespace MyCompany.Expenses.WebApiRole.Models
{
    using MyCompany.Expenses.Model;

    /// <summary>
    /// Notification Channel
    /// </summary>
    public class ClientNotificationChannel
    {
        /// <summary>
        /// Gets or sets the channel URI.
        /// </summary>
        /// <value>
        /// The channel URI.
        /// </value>
        public string ChannelUri { get; set; }

        /// <summary>
        /// Gets or sets the type of the notification.
        /// </summary>
        /// <value>
        /// The type of the notification.
        /// </value>
        public NotificationType NotificationType { get; set; }
    }
}