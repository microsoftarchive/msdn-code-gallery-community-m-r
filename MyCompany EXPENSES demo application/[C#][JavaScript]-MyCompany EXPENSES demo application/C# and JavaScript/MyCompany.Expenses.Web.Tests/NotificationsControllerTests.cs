namespace MyCompany.Expenses.Web.Tests
{
    using MyCompany.Expenses.Web;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Expenses.Web.Controllers;
    using MyCompany.Expenses.Model;
    using System.Threading.Tasks;

    [TestClass]
    public class NotificationsControllerTests
    {
        [TestMethod]
        public async Task Add_ShouldCallNotificationsRepositoryAdd()
        {
            var called = false;
            var userChannel = "http://www.mycompany.com/testChannel";
            var userNotificationType = NotificationType.WindowsPhoneNotification;

            var notificationChannelRepository = new Data.Repositories.Fakes.StubINotificationChannelRepository();
            notificationChannelRepository.AddUserChannelAsyncStringStringNotificationType = (userIdentity, channel, notificationType) =>
            {
                Assert.AreEqual(userChannel, channel);
                Assert.AreEqual(userNotificationType, notificationType);
                called = true;

                return Task.FromResult(string.Empty);
            };

            var notificationsController = new NotificationsController(notificationChannelRepository, new SecurityHelper());
            await notificationsController.Add(new Models.ClientNotificationChannel { ChannelUri = userChannel, NotificationType = userNotificationType });

            Assert.IsTrue(called);
        }

        [TestMethod]
        public async Task Delete_ShouldCallNotificationsRepositoryRemove()
        {
            var called = false;
            var userChannel = "http://www.mycompany.com/testChannel";
            var userNotificationType = NotificationType.WindowsPhoneNotification;

            var notificationChannelRepository = new Data.Repositories.Fakes.StubINotificationChannelRepository();
            notificationChannelRepository.RemoveUserChannelAsyncStringStringNotificationType = (userIdentity, channel, notificationType) =>
            {
                Assert.AreEqual(userChannel, channel);
                Assert.AreEqual(userNotificationType, notificationType);
                called = true;

                return Task.FromResult(string.Empty);
            };

            var notificationsController = new NotificationsController(notificationChannelRepository, new SecurityHelper());
            await notificationsController.Delete(new Models.ClientNotificationChannel { ChannelUri = userChannel, NotificationType = userNotificationType });

            Assert.IsTrue(called);
        }
    }
}