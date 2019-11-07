namespace MyCompany.Expenses.WebApiRole.Services
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    /// <summary>
    /// Push notification service
    /// </summary>
    public class WindowsPhonePushNotificationService : IWindowPhonePushNotificationService
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
        /// <param name="channelUri"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public string SendToast(string channelUri, string message, string argKey, string argValue)
        {
            var subscriptionUri = new Uri(channelUri);

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
            byte[] contentInBytes = Encoding.UTF8.GetBytes(xml);
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