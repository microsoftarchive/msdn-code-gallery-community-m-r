namespace MyCompany.Expenses.Client.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NotificationsServiceTests
    {
       
        [TestMethod]
        [TestCategory("Integration")]
        public async Task ExpenseTravelService_AddNotificationChannel_Added_NotFail_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var notificationChannel = GetSampleNotificationChannel();
            await client.NotificationsService.Add(notificationChannel);
        }              

        [TestMethod]
        [TestCategory("Integration")]
        public async Task ExpenseTravelService_DeleteNotificationChannel_NotFail_Test()
        {
            var client = new MyCompanyClient(SecurityHelper.UrlBase, SecurityHelper.AccessToken);
            var notificationChannel = GetSampleNotificationChannel();
            await client.NotificationsService.Delete(notificationChannel);
        }

        private static ClientNotificationChannel GetSampleNotificationChannel()
        {
            var channelUri = "http://mycompany.com/testChannel";
            var notificationType = NotificationType.WindowsPhoneNotification;

            var notificationChannel = new ClientNotificationChannel
            {
                ChannelUri = channelUri,
                NotificationType = notificationType
            };
            return notificationChannel;
        }  
    }
}
