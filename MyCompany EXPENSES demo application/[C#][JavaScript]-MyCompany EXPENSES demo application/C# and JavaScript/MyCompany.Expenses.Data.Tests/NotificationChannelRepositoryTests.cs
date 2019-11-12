namespace MyCompany.Expenses.Data.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using MyCompany.Expenses.Model;
    using MyCompany.Expenses.Data.Repositories;
    using System.Threading.Tasks;
    using System.Configuration;
    using System;

    [TestClass]
    public class NotificationChannelRepositoryTests
    {
        NotificationChannelRepository notificationChannelRepository;
        private static string tenant = ConfigurationManager.AppSettings["ida:Tenant"];

        [TestInitialize]
        public void TestInitialize()
        {
            notificationChannelRepository = new NotificationChannelRepository(new MyCompanyContext());
        }

        [TestMethod]
        public async Task GetUserChannels_ShouldReturn2Channels_ForTheFirstEmployee()
        {
            var context = new MyCompanyContext();
            var userIdentity = context.Employees.FirstOrDefault(e => e.Expenses.Any()).Email;

            var notificationChannels = await notificationChannelRepository.GetUserChannelsAsync(userIdentity);

            Assert.AreEqual(2, notificationChannels.Count());
            Assert.IsTrue(notificationChannels.Any(nc => nc.NotificationType == NotificationType.WindowsPhoneNotification));
            Assert.IsTrue(notificationChannels.Any(nc => nc.NotificationType == NotificationType.WindowsStoreNotification));
        }

        [TestMethod]
        public async Task AddUserChannel_ShouldAddChannelIfItDoesntExist()
        {
            var context = new MyCompanyContext();
            var userIdentity = String.Format("johndoe2@{0}", tenant);
            string channelUri = "http://mycompany.com/channelUri";

            var notificationChannels = await notificationChannelRepository.GetUserChannelsAsync(userIdentity);
            Assert.AreEqual(0, notificationChannels.Count());

            await notificationChannelRepository.AddUserChannelAsync(userIdentity, channelUri, NotificationType.WindowsPhoneNotification);

            notificationChannels = (await notificationChannelRepository.GetUserChannelsAsync(userIdentity)).ToList();
            Assert.AreEqual(1, notificationChannels.Count());
            Assert.AreEqual(NotificationType.WindowsPhoneNotification, notificationChannels.First().NotificationType);
            Assert.AreEqual(channelUri, notificationChannels.First().ChannelUri);
        }

        [TestMethod]
        public async Task AddUserChannel_ShouldUpdateChannelIfItExists()
        {
            var context = new MyCompanyContext();
            var userIdentity = String.Format("johndoe@{0}", tenant);
            string newChannelUri = "http://mycompany.com/newChannelUri";


            await notificationChannelRepository.AddUserChannelAsync(userIdentity, newChannelUri, NotificationType.WindowsPhoneNotification);
            var notificationChannels = await notificationChannelRepository.GetUserChannelsAsync(userIdentity);
            Assert.AreEqual(2, notificationChannels.Count());
            Assert.AreEqual(newChannelUri, notificationChannels.First(nc => nc.NotificationType == NotificationType.WindowsPhoneNotification).ChannelUri);
        }

        [TestMethod]
        public async Task GetManagersChannels_ShouldReturnTheChannelsFromManagers()
        {
            var managerChannels = await notificationChannelRepository.GetManagersChannelsAsync();

            Assert.AreEqual(2, managerChannels.Count());
            Assert.IsTrue(managerChannels.Any(nc => nc.NotificationType == NotificationType.WindowsPhoneNotification));
            Assert.IsTrue(managerChannels.Any(nc => nc.NotificationType == NotificationType.WindowsStoreNotification));
        }

        [TestMethod]
        public async Task RemoveChannel_ShouldRemoveTheChannel()
        {
            var context = new MyCompanyContext();
            var userIdentity = String.Format("johndoe1@{0}", tenant);
            var notificationChannel = context.NotificationChannels.Where(nc => nc.Employee.Email == userIdentity).First();

            await notificationChannelRepository.RemoveUserChannelAsync(userIdentity, notificationChannel.ChannelUri, notificationChannel.NotificationType);

            var actualNotificationChannel = context.NotificationChannels
                .Where(nc => nc.Employee.Email == userIdentity 
                                && nc.ChannelUri == notificationChannel.ChannelUri 
                                && nc.NotificationType == notificationChannel.NotificationType)
                .FirstOrDefault();

            Assert.IsNull(actualNotificationChannel);
        }
    }
}
