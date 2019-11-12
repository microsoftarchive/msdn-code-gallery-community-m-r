
namespace MyCompany.Expenses.WebApiRole.Services
{
    using System.Collections.Generic;

    /// <summary>
    /// Notificators repository interface
    /// </summary>
    public interface INotifiersProvider
    {
        List<IPushNotificationService> GetNotificators();
    }
}