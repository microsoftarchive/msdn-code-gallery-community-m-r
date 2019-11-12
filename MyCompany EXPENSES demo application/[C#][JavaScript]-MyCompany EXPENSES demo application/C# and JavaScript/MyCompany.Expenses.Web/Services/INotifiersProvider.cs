
namespace MyCompany.Expenses.Web.Services
{
    using System.Collections.Generic;

    /// <summary>
    /// Notificators repository interface
    /// </summary>
    public interface INotifiersProvider
    {
        /// <summary>
        /// GetNotificators
        /// </summary>
        /// <returns></returns>
        List<IPushNotificationService> GetNotificators();
    }
}