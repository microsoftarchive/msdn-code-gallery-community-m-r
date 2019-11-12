namespace MyCompany.Expenses.Web.Controllers
{
    using System;
    using System.Web.Http;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Web;
    using MyCompany.Expenses.Data;
    using MyCompany.Expenses.Model;
    using MyCompany.Expenses.Web.Models;
    using MyCompany.Expenses.Data.Repositories;
    using System.Threading.Tasks;


    /// <summary>
    /// Notifications Controller
    /// </summary>   
    [RoutePrefix("api/notifications")]
    public class NotificationsController : ApiController
    {
        INotificationChannelRepository _notificationChannelRepository = null;
        ISecurityHelper _securityHelper = null;

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
        public async Task Add(ClientNotificationChannel notificationChannel)
        {
            var identity = _securityHelper.GetUser();
            await _notificationChannelRepository.AddUserChannelAsync(identity, notificationChannel.ChannelUri, notificationChannel.NotificationType);
        }

        /// <summary>
        /// Deletes an existing notification channel.
        /// </summary>
        /// <param name="notificationChannel"></param>
        public async Task Delete(ClientNotificationChannel notificationChannel)
        {
            var identity = _securityHelper.GetUser();
            await _notificationChannelRepository.RemoveUserChannelAsync(identity, notificationChannel.ChannelUri, notificationChannel.NotificationType);
        }
    }
}
