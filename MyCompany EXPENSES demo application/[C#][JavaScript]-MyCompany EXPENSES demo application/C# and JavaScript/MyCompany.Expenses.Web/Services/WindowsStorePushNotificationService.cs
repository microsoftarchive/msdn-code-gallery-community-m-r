namespace MyCompany.Expenses.Web.Services
{
    using MyCompany.Expenses.Web.Models;
    using System;
    using System.Configuration;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Web;

    /// <summary>
    /// Push notification service
    /// </summary>
    public class WindowsStorePushNotificationService : IPushNotificationService
    {
        private string _secret = null;
        private string _sid = null;
        private string _accessToken = null;

        /// <summary>
        /// WindowsStorePushNotificationService
        /// </summary>
        public WindowsStorePushNotificationService()
        {
            _secret = ConfigurationManager.AppSettings["PushNotificationSecret"];
            _sid = ConfigurationManager.AppSettings["PushNotificationSid"];
        }

        /// <summary>
        /// Gets the notification type.
        /// </summary>
        public Model.NotificationType NotificationType
        {
            get { return Model.NotificationType.WindowsStoreNotification; }
        }

        /// <summary>
        /// Sends a toast to the specidied channel with the specified message.
        /// </summary>
        /// <param name="channelUri">The channel URI.</param>
        /// <param name="message">The message.</param>
        /// <param name="argKey">The arg key.</param>
        /// <param name="expenseId">The expense id.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public string SendNewExpenseAddedToast(string channelUri, string message, string argKey, int expenseId)
        {
            return SendToast(channelUri, message, argKey, expenseId.ToString());
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
            return SendToast(channelUri, message, argKey, expenseId.ToString());
        }

        private string SendToast(string channelUri, string message, string argKey, string argValue)
        {
            if (String.IsNullOrEmpty(_sid) || String.IsNullOrEmpty(_sid))
                return string.Empty;

            string xml = string.Format("<toast launch=\"{{&quot;{0}&quot;:&quot;{1}&quot;}}\">"
               + "<visual>"
                    +"<binding template=\"ToastText01\">"
                        +"<text id=\"1\">{2}</text>"
                    +"</binding>"
                +"</visual>"
            +"</toast>", argKey, argValue, message);

            return PostToWns(_secret, _sid, channelUri, xml, "wns/toast");
        }

        private string PostToWns(string secret, string sid, string uri, string xml, string type = "wns/badge")
        {
            try
            {
                if (string.IsNullOrEmpty(_accessToken))
                    _accessToken = GetAccessToken(secret, sid).AccessToken;

                byte[] contentInBytes = Encoding.UTF8.GetBytes(xml);

                // uri is the channel URI
                HttpWebRequest request = HttpWebRequest.Create(uri) as HttpWebRequest;
                request.Method = "POST";
                request.Headers.Add("X-WNS-Type", type);
                request.Headers.Add("X-WNS-RequestForStatus", "true");

                request.Headers.Add("Authorization", String.Format("Bearer {0}", _accessToken));

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(contentInBytes, 0, contentInBytes.Length);
                }

                using (HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse())
                {
                    return webResponse.StatusCode.ToString();
                }
            }
            catch (WebException webException)
            {
                Console.WriteLine(webException.Data);
                return null;
            }
        }

        private OAuthToken GetAccessToken(string secret, string sid)
        {
            var urlEncodedSecret = HttpUtility.UrlEncode(secret);
            var urlEncodedSid = HttpUtility.UrlEncode(sid);

            var body =
              String.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=notify.windows.com", urlEncodedSid, urlEncodedSecret);

            string response;
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                response = client.UploadString("https://login.live.com/accesstoken.srf", body);
            }
            return GetOAuthTokenFromJson(response);
        }

        private OAuthToken GetOAuthTokenFromJson(string jsonString)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                try
                {
                    var ser = new DataContractJsonSerializer(typeof(OAuthToken));
                    var serObject = ser.ReadObject(ms);
                    var oAuthToken = (OAuthToken)serObject;
                    return oAuthToken;
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }
    }
}