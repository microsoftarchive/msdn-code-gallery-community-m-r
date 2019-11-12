
namespace MyCompany.Expenses.WebApiRole.Services
{
    using MyCompany.Expenses.Model;


    /// <summary>
    /// PushNotification service interface
    /// </summary>
    public interface IPushNotificationService
    {
        NotificationType NotificationType { get; }

        /// <summary>
        /// Sends a toast to the specidied channel with the specified message.
        /// </summary>
        /// <param name="channelUri"></param>
        /// <param name="message"></param>
        /// <param name="argKey"></param>
        /// <param name="argValue"></param>
        /// <returns></returns>
        string SendToast(string channelUri, string message, string argKey, string argValue);
    }
}