
namespace MyCompany.Expenses.Web.Services
{
    using MyCompany.Expenses.Model;

    /// <summary>
    /// PushNotification service interface
    /// </summary>
    public interface IPushNotificationService
    {
        /// <summary>
        /// NotificationType
        /// </summary>
        NotificationType NotificationType { get; }

        /// <summary>
        /// Sends a toast to the specidied channel with the specified message.
        /// </summary>
        /// <param name="channelUri">The channel URI.</param>
        /// <param name="message">The message.</param>
        /// <param name="argKey">The arg key.</param>
        /// <param name="expenseId">The expense id.</param>
        /// <returns></returns>
        string SendNewExpenseAddedToast(string channelUri, string message, string argKey, int expenseId);

        /// <summary>
        /// Sends the expense status changed toast.
        /// </summary>
        /// <param name="channelUri">The channel URI.</param>
        /// <param name="message">The message.</param>
        /// <param name="argKey">The arg key.</param>
        /// <param name="expenseId">The expense id.</param>
        /// <returns></returns>
        string SendExpenseStatusChangedToast(string channelUri, string message, string argKey, int expenseId);
    }
}