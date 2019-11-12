
namespace MyCompany.Expenses.Web.Services
{
    using MyCompany.Expenses.Model;
    using MyCompany.Expenses.Web.Resources;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Notification service
    /// </summary>
    public class NotificationService : INotificationService
    {
        private Dictionary<NotificationType, IPushNotificationService> _notifiers = new Dictionary<NotificationType, IPushNotificationService>();
        
        /// <summary>
        /// Notification service constructor
        /// </summary>
        /// <param name="notificatorsProvider"></param>
        public NotificationService(INotifiersProvider notificatorsProvider)
        {
            if (null == notificatorsProvider)
                throw new ArgumentNullException("notificatorsProvider");

            var notificators = notificatorsProvider.GetNotificators();
            foreach (var notificator in notificators)
            {
                if (!_notifiers.ContainsKey(notificator.NotificationType))
                    _notifiers.Add(notificator.NotificationType, notificator);
            }
        }

        /// <summary>
        /// Notify new expense added
        /// </summary>
        /// <param name="notificationChannel"></param>
        /// <param name="expense"></param>
        public void NewExpenseAdded(NotificationChannel notificationChannel, Expense expense)
        {
            string toastMessage = string.Format(CultureInfo.InvariantCulture,
                NotificationResources.AddExpenseNotificationMessage,
                expense.Employee.FirstName, expense.Employee.LastName);

            _notifiers[notificationChannel.NotificationType].SendNewExpenseAddedToast(
                notificationChannel.ChannelUri, 
                toastMessage, 
                "expenseId", 
                expense.ExpenseId);
        }

        /// <summary>
        /// Expense status changed.
        /// </summary>
        /// <param name="notificationChannel">The notification channel.</param>
        /// <param name="expense">The expense.</param>
        public void ExpenseStatusChanged(NotificationChannel notificationChannel, Expense expense)
        {
            string toastMessage = string.Empty;
            if (expense.Status == ExpenseStatus.Approved)
            {
                toastMessage = NotificationResources.ExpenseApprovedNotificationMessage;
            }
            else if (expense.Status == ExpenseStatus.Denied)
            {
                toastMessage = NotificationResources.ExpenseDeclinedNotificationMessage;
            }

            _notifiers[notificationChannel.NotificationType].SendExpenseStatusChangedToast(
                notificationChannel.ChannelUri,
                toastMessage,
                "expenseId",
                expense.ExpenseId);
        }
    }
}