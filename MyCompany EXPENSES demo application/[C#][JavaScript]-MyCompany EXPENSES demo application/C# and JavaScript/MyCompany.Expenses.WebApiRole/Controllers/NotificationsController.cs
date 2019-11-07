namespace MyCompany.Expenses.WebApiRole.Controllers
{
    using MyCompany.Expenses.Data.Repositories;
    using MyCompany.Expenses.WebApiRole.Models;
    using System;
    using System.Web.Http;


    /// <summary>
    /// Notifications Controller
    /// </summary>   
    [RoutePrefix("api/notifications")]
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly INotificationChannelRepository _notificationChannelRepository = null;
        private readonly ISecurityHelper _securityHelper = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="notificationChannelRepository">INotificationChannelRepository dependency</param>
        /// <param name="securityHelper">ISecurityHelper dependency</param>
        public NotificationsController(INotificationChannelRepository notificationChannelRepository, ISecurityHelper securityHelper)
        {
            if (notificationChannelRepository == null)
                throw new ArgumentNullException("notificationChannelRepository");

            if (securityHelper == null)
                throw new ArgumentNullException("securityHelper");

            _notificationChannelRepository = notificationChannelRepository;
            _securityHelper = securityHelper;
        }

        /// <summary>
        /// Adds a new notification channel.
        /// </summary>
        /// <param name="notificationChannel">The notification channel.</param>
        public void Add(ClientNotificationChannel notificationChannel)
        {
            var identity = _securityHelper.GetUser();
            _notificationChannelRepository.AddUserChannelAsync(identity, notificationChannel.ChannelUri, notificationChannel.NotificationType);
        }

        /// <summary>
        /// Deletes an existing notification channel.
        /// </summary>
        /// <param name="notificationChannel"></param>
        public void Delete(ClientNotificationChannel notificationChannel)
        {
            var identity = _securityHelper.GetUser();
            _notificationChannelRepository.RemoveUserChannelAsync(identity, notificationChannel.ChannelUri, notificationChannel.NotificationType);
        }
    }
}
