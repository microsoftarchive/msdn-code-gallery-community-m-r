namespace MyCompany.Travel.Web.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyCompany.Travel.Web.Hubs;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class HubTester : TravelsNotificationHub
    {
        public IEnumerable<string> TestGetUserConnections(string userId)
        {
            return TravelsNotificationHub.GetUserConnections(userId);
        }

        public IEnumerable<string> TestGetRRHHUsersConnections()
        {
            return TravelsNotificationHub.GetRRHHUsersConnections();
        }
    }

    [TestClass]
    public class TravelsNotificationHubTests
    {
        [TestMethod]
        public void TravelsNotificationHub_Constructor_Test()
        {
            var hub = new TravelsNotificationHub();
        }

        [TestMethod]
        public void TravelsNotificationHub_GetUserConnections_Test()
        {
            var hub = new HubTester();
            HubTester.Users.TryAdd("user", new User() { Id = "userId", ConnectionIds = new HashSet<string>() { "connection1" } });
            var result = hub.TestGetUserConnections("user");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("connection1", result.First());
        }

        [TestMethod]
        public void TravelsNotificationHub_GetRRHHUsersConnections()
        {
            var hub = new HubTester();
            HubTester.Users.TryAdd("user1", new User() { Id = "userId", ConnectionIds = new HashSet<string>() { "connection1" }, IsRRHH = false });
            HubTester.Users.TryAdd("user2", new User() { Id = "userId", ConnectionIds = new HashSet<string>() { "connection2" } , IsRRHH = true});
            var result = hub.TestGetRRHHUsersConnections();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("connection2", result.First());
        }
    }
}