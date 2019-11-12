
namespace MyCompany.Expenses.Client
{
    using System.Threading.Tasks;

    /// <summary>
    /// Class to access to the notification webapi Controller 
    /// </summary>
    public interface INotificationsService
    {
        /// <summary>
        /// Add new notification channel
        /// </summary>
        /// <param name="notificationChannel">notification channel information</param>
        Task Add(ClientNotificationChannel notificationChannel);

        /// <summary>
        /// Add new notification channel
        /// </summary>
        /// <param name="notificationChannel">notification channel information</param>
        Task Delete(ClientNotificationChannel notificationChannel);
    }
}
