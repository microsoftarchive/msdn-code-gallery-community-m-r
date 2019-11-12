namespace MyCompany.Expenses.Web.Services
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

        /// <summary>
        /// Expense status changed.
        /// </summary>
        /// <param name="notificationChannel">The notification channel.</param>
        /// <param name="expense">The expense.</param>
        void ExpenseStatusChanged(NotificationChannel notificationChannel, Expense expense);
    }
}