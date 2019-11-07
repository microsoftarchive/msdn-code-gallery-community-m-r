namespace MyCompany.Expenses.Data.Repositories
{
    using System.Collections.Generic;
    using MyCompany.Expenses.Model;
    using System.Threading.Tasks;
    using System;

    /// <summary>
    /// Notification Channel Repository interface
    /// </summary>
    public interface INotificationChannelRepository : IDisposable
    {
        /// <summary>
        /// Gets the user channels.
        /// </summary>
        /// <param name="userIdentity">The user identity.</param>
        /// <returns>A List of notification channels</returns>
        Task<IEnumerable<NotificationChannel>> GetUserChannelsAsync(string userIdentity);

        /// <summary>
        /// Adds the user channel.
        /// </summary>
        /// <param name="userIdentity">The user identity.</param>
        /// <param name="channelUri">The channel URI.</param>
        /// <param name="notificationType">The notification type.</param>
        Task AddUserChannelAsync(string userIdentity, string channelUri, NotificationType notificationType);

        /// <summary>
        /// Gets the managers channels.
        /// </summary>
        /// <returns>A list of notification channels</returns>
        Task<IEnumerable<NotificationChannel>> GetManagersChannelsAsync();

        /// <summary>
        /// Removes the user channel.
        /// </summary>
        /// <param name="userIdentity">The user identity.</param>
        /// <param name="channelUri">The channel URI.</param>
        /// <param name="notificationType">The notification type.</param>
        Task RemoveUserChannelAsync(string userIdentity, string channelUri, NotificationType notificationType);
    }
}
