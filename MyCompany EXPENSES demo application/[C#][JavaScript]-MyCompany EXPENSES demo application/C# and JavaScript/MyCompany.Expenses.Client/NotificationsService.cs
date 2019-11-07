
namespace MyCompany.Expenses.Client
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using MyCompany.Expenses.Client.Web;

    /// <summary>
    /// <see cref="MyCompany.Expenses.Client.INotificationsService"/>
    /// </summary>
    public class NotificationsService : BaseRequest, INotificationsService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        /// <param name="securityToken">Security Token</param>
        public NotificationsService(string urlPrefix, string securityToken)
            : base(urlPrefix, securityToken)
        {

        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Client.INotificationsService"/>
        /// </summary>
        /// <param name="notificationChannel"><see cref="MyCompany.Expenses.Client.INotificationsService"/></param>
        /// <returns><see cref="MyCompany.Expenses.Client.INotificationsService"/></returns>
        public async Task Add(ClientNotificationChannel notificationChannel)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/notifications", _urlPrefix);

            await base.PostAsync<ClientNotificationChannel>(url, notificationChannel);
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Client.INotificationsService"/>
        /// </summary>
        /// <param name="notificationChannel"><see cref="MyCompany.Expenses.Client.INotificationsService"/></param>
        /// <returns><see cref="MyCompany.Expenses.Client.INotificationsService"/></returns>
        public async Task Delete(ClientNotificationChannel notificationChannel)
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/notifications", _urlPrefix);

            await base.PostAsync<ClientNotificationChannel>(url, notificationChannel);
        }  
    }
}
