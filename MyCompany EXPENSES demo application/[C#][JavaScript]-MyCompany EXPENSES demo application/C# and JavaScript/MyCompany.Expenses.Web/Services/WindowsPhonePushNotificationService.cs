namespace MyCompany.Expenses.Web.Services
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    /// <summary>
    /// Push notification service
    /// </summary>
    public class WindowsPhonePushNotificationService : IPushNotificationService
    {
        /// <summary>
        /// Gets the notification type.
        /// </summary>
        public Model.NotificationType NotificationType
        {
            get { return Model.NotificationType.WindowsPhoneNotification; }
        }

        /// <summary>
        /// Sends a toast to the specidied channel with the specified message.
        /// </summary>
        /// <param name="channelUri">The channel URI.</param>
        /// <param name="message">The message.</param>
        /// <param name="argKey">The arg key.</param>
        /// <param name="expenseId">The expense id.</param>
        /// <returns></returns>
        public string SendNewExpenseAddedToast(string channelUri, string message, string argKey, int expenseId)
        {
            return SendToast(channelUri, message, string.Format("/Views/AuthenticationPage.xaml?expenseId={0}", expenseId));
        }

        /// <summary>
        /// Sends the expense status changed toast.
        /// </summary>
        /// <param name="channelUri">The channel URI.</param>
        /// <param name="message">The message.</param>
        /// <param name="argKey">The arg key.</param>
        /// <param name="expenseId">The expense id.</param>
        /// <returns></returns>
        public string SendExpenseStatusChangedToast(string channelUri, string message, string argKey, int expenseId)
        {
            return SendToast(channelUri, message, string.Format("/Views/AuthenticationPage.xaml?expenseId={0}", expenseId));
        }

        private string SendToast(string channelUri, string message, string argValue)
        {
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<wp:Notification xmlns:wp=\"WPNotification\">" +
                    "<wp:Toast>" +
                        "<wp:Text1>{0}</wp:Text1>" +
                        "<wp:Text2>{1}</wp:Text2>" +
                        "<wp:Param>{2}</wp:Param>" +
                    "</wp:Toast>" +
                "</wp:Notification>";

            string args = argValue;

            xml = string.Format(xml, "My Company", message, args);
            return Post(channelUri, xml);
        }

        private string Post(string channelUri, string xml)
        {
            UTF8Encoding encoder = new UTF8Encoding(false);
            byte[] contentInBytes = encoder.GetBytes(xml);
            string notificationType = "toast";
            int toastNotificationTypeId = 2;

            HttpWebRequest request = HttpWebRequest.Create(channelUri) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "text/xml";
            request.Headers.Add("X-WindowsPhone-Target", notificationType);
            request.Headers.Add("X-NotificationClass", toastNotificationTypeId.ToString());

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(contentInBytes, 0, contentInBytes.Length);
            }

            using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse())
            {
                return webResponse.StatusCode.ToString();
            }
        }
    }
}