namespace MyCompany.Common.Notification.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class HubTester : BaseHub
    {
        public IEnumerable<string> TestGetUserConnections(string userId)
        {
            return BaseHub.GetUserConnections(userId);
        }
    }

    [TestClass]
    public class BaseHubTests
    {
        [TestMethod]
        public void BaseHub_Constructor_Test()
        {
            var hub = new BaseHub();
        }

        [TestMethod]
        public void BaseHub_GetUserConnections_Test()
        {
            var hub = new HubTester();
            HubTester.Users.TryAdd("user", new User() { Id = "userId", ConnectionIds = new HashSet<string>() { "connection1" } });
            var result = hub.TestGetUserConnections("user");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("connection1", result.First());
        }
    }
}
