namespace MyCompany.Expenses.WebApiRole.Services
{
    using MyCompany.Expenses.Model;

    /// <summary>
    /// Notification service interface
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// New expense added.
        /// </summary>
        /// <param name="notificationChannel"></param>
        /// <param name="expense"></param>
        void NewExpenseAdded(NotificationChannel notificationChannel, Expense expense);
    }
}