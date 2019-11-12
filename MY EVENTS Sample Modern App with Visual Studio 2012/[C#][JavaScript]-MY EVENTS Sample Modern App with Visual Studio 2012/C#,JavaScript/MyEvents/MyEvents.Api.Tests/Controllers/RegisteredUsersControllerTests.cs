using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyEvents.Api.Authentication.Fakes;
using MyEvents.Api.Controllers;
using MyEvents.Data;
using MyEvents.Data.Fakes;
using MyEvents.Model;

namespace MyEvents.Api.Tests.Controllers
{
    [TestClass]
    public class RegisteredUsersControllerTests
    {
        [TestMethod]
        public void RegisteredUsersController_Contructor_NotFail_Test()
        {
            IRegisteredUserRepository registeredUserService = new StubIRegisteredUserRepository();
            var target = new RegisteredUsersController(registeredUserService);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RegisteredUsersController_ContructorWithNullDependency_Fail_Test()
        {
            var target = new RegisteredUsersController(null);
        }

        [TestMethod]
        public void GetRegisteredUser_Called_NotFail_Test()
        {
            string expectedFacebookId = "10";
            bool called = false;

            IRegisteredUserRepository registeredUserService = new StubIRegisteredUserRepository()
            {
                GetString = facebookId =>
                {
                    Assert.AreEqual(expectedFacebookId, facebookId);
                    called = true;
                    return new RegisteredUser() { FacebookId = expectedFacebookId };
                }
            };

            var target = new RegisteredUsersController(registeredUserService);

            RegisteredUser result = target.GetRegisteredUser(expectedFacebookId);

            Assert.IsTrue(called);
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedFacebookId, result.FacebookId);
        }

        [TestMethod]
        public void GetFakeRegisteredUser_Called_NotFail_Test()
        {
            int expectedId = 10;
            bool called = false;

            IRegisteredUserRepository registeredUserService = new StubIRegisteredUserRepository()
            {
                GetByIdInt32 = registerUserId =>
                {
                    int fakeUserId = Int32.Parse(WebConfigurationManager.AppSettings["fakeUserId"]);
                    Assert.AreEqual(fakeUserId, registerUserId);
                    called = true;
                    return new RegisteredUser() { RegisteredUserId = expectedId };
                }
            };

            var target = new RegisteredUsersController(registeredUserService);

            RegisteredUser result = target.GetFakeRegisteredUser();

            Assert.IsTrue(called);
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedId, result.RegisteredUserId);
        }

        [TestMethod]
        public void GetAllRegisteredUsersByEventId_GetNull_NotFail_Test()
        {
            int expectedId = 10;
            bool called = false;

            IRegisteredUserRepository registeredUserService = new StubIRegisteredUserRepository() 
            {
                GetAllByEventIdInt32 = eventId =>
                                                    {
                                                        Assert.AreEqual(expectedId, eventId);
                                                        called = true;
                                                        return null;
                                                    }
            };

            var target = new RegisteredUsersController(registeredUserService);

            IEnumerable<RegisteredUser> result = target.GetAllRegisteredUsersByEventId(expectedId);

            Assert.IsTrue(called); 
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetAllRegisteredUsersByEventId_GetEmptyResults_NotFail_Test()
        {
            int expectedId = 10;
            bool called = false;

            IRegisteredUserRepository registeredUserService = new StubIRegisteredUserRepository()
            {
                GetAllByEventIdInt32 = eventId =>
                {
                    Assert.AreEqual(expectedId, eventId);
                    called = true;
                    return new List<RegisteredUser>();
                }
            };

            var target = new RegisteredUsersController(registeredUserService);

            IEnumerable<RegisteredUser> result = target.GetAllRegisteredUsersByEventId(expectedId);

            Assert.IsTrue(called); 
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 0);
        }

        [TestMethod]
        public void GetAllRegisteredUsersByEventId_GetResults_Test()
        {
            int expectedId = 10;
            bool called = false;

            IRegisteredUserRepository registeredUserService = new StubIRegisteredUserRepository()
            {
                GetAllByEventIdInt32 = eventId =>
                {
                    Assert.AreEqual(expectedId, eventId);
                    called = true;
                    return new List<RegisteredUser>() { new RegisteredUser(), new RegisteredUser() };
                }
            };

            var target = new RegisteredUsersController(registeredUserService);

            IEnumerable<RegisteredUser> result = target.GetAllRegisteredUsersByEventId(expectedId);

            Assert.IsTrue(called); 
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 2);
        }

        [TestMethod]
        public void GetAllRegisteredUsersBySessionId_GetNull_NotFail_Test()
        {
            int expectedId = 10;
            bool called = false;

            IRegisteredUserRepository registeredUserService = new StubIRegisteredUserRepository()
            {
                GetAllBySessionIdInt32 = sessionId =>
                {
                    Assert.AreEqual(expectedId, sessionId);
                    called = true;
                    return null;
                }
            };

            var target = new RegisteredUsersController(registeredUserService);

            IEnumerable<RegisteredUser> result = target.GetAllRegisteredUsersBySessionId(expectedId);

            Assert.IsTrue(called); 
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetAllRegisteredUsersBySessionId_GetEmptyResults_NotFail_Test()
        {
            int expectedId = 10;
            bool called = false;

            IRegisteredUserRepository registeredUserService = new StubIRegisteredUserRepository()
            {
                GetAllBySessionIdInt32 = sessionId =>
                {
                    Assert.AreEqual(expectedId, sessionId);
                    called = true;
                    return new List<RegisteredUser>();
                }
            };

            var target = new RegisteredUsersController(registeredUserService);

            IEnumerable<RegisteredUser> result = target.GetAllRegisteredUsersBySessionId(expectedId);

            Assert.IsTrue(called); 
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 0);
        }

        [TestMethod]
        public void GetAllRegisteredUsersBySessionId_GetResults_Test()
        {
            int expectedId = 10;
            bool called = false;

            IRegisteredUserRepository registeredUserService = new StubIRegisteredUserRepository()
            {
                GetAllBySessionIdInt32 = sessionId =>
                {
                    Assert.AreEqual(expectedId, sessionId);
                    called = true;
                    return new List<RegisteredUser>() { new RegisteredUser(), new RegisteredUser() };
                }
            };

            var target = new RegisteredUsersController(registeredUserService);

            IEnumerable<RegisteredUser> result = target.GetAllRegisteredUsersBySessionId(expectedId);

            Assert.IsTrue(called);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 2);
        }

        [TestMethod]
        public void PostRegisteredUser_Call_NotFail_Test()
        {
            int expected = 1;
            bool called = false;
            RegisteredUser registeredUser = new RegisteredUser();

            IRegisteredUserRepository registeredUserService = new StubIRegisteredUserRepository()
            {
                AddRegisteredUser = RegisteredUserParam =>
                {
                    called = true;
                    return expected;
                }
            };

            var target = new RegisteredUsersController(registeredUserService);

            int actual = target.PostRegisteredUserCreateIfNotExists(registeredUser);

            Assert.IsTrue(called);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PostRegisteredUser_ArgumentNullException_Test()
        {
            IRegisteredUserRepository registeredUserService = new StubIRegisteredUserRepository();
            var target = new RegisteredUsersController(registeredUserService);

            target.PostRegisteredUserCreateIfNotExists(null);
        }

        [TestMethod]
        public void AddRegisteredUserToSession_Call_NotFail_Test()
        {
            bool called = false;
            int expectedRegisteredUserId = 10;
            int expectedSessionId = 20;

            IRegisteredUserRepository registeredUserService = new StubIRegisteredUserRepository()
            {
                AddRegisteredUserToSessionInt32Int32 = (registeredUserId, sessionId) =>
                {
                    Assert.AreEqual(expectedRegisteredUserId, registeredUserId);
                    Assert.AreEqual(expectedSessionId, sessionId);
                    called = true;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return expectedRegisteredUserId; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new RegisteredUsersController(registeredUserService);

                target.PostRegisteredUserToSession(expectedRegisteredUserId, expectedSessionId);

                Assert.IsTrue(called);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void AddRegisteredUserToSession_Call_UnauthorizedException_Test()
        {
            bool called = false;
            int expectedRegisteredUserId = 10;
            int expectedSessionId = 20;

            IRegisteredUserRepository registeredUserService = new StubIRegisteredUserRepository();
            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 10000; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new RegisteredUsersController(registeredUserService);

                target.PostRegisteredUserToSession(expectedRegisteredUserId, expectedSessionId);

                Assert.IsTrue(called);
            }
        }

        [TestMethod]
        public void DeleteRegisteredUserFromSession_Call_NotFail_Test()
        {
            bool called = false;
            int expectedRegisteredUserId = 10;
            int expectedSessionId = 20;

            IRegisteredUserRepository registeredUserService = new StubIRegisteredUserRepository()
            {
                DeleteRegisteredUserFromSessionInt32Int32 = (registeredUserId, sessionId) =>
                {
                    Assert.AreEqual(expectedRegisteredUserId, registeredUserId);
                    Assert.AreEqual(expectedSessionId, sessionId);
                    called = true;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return expectedRegisteredUserId; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new RegisteredUsersController(registeredUserService);

                target.DeleteRegisteredUserFromSession(expectedRegisteredUserId, expectedSessionId);

                Assert.IsTrue(called);
            }

        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void DeleteRegisteredUserFromSession_Call_UnauthorizedException_Test()
        {
            bool called = false;
            int expectedRegisteredUserId = 10;
            int expectedSessionId = 20;

            IRegisteredUserRepository registeredUserService = new StubIRegisteredUserRepository();
            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 10000; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new RegisteredUsersController(registeredUserService);

                target.DeleteRegisteredUserFromSession(expectedRegisteredUserId, expectedSessionId);

                Assert.IsTrue(called);
            }

        }

        [TestMethod]
        public void GetRegisteredUserEventDefinitions_GetResults_Test()
        {
            int expectedId = 10;
            bool called = false;

            IRegisteredUserRepository registeredUserService = new StubIRegisteredUserRepository()
            {
                GetEventDefinitionsInt32 = registeredUserId =>
                {
                    Assert.AreEqual(expectedId, registeredUserId);
                    called = true;
                    return new List<EventDefinition>() { new EventDefinition(), new EventDefinition() };
                }
            };

            var target = new RegisteredUsersController(registeredUserService);

            IEnumerable<EventDefinition> result = target.GetRegisteredUserEventDefinitions(expectedId);

            Assert.IsTrue(called);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 2);
        }

        [TestMethod]
        public void PostRegisteredUserScore_Call_NotFail_Test()
        {
            bool called = false;
            int expectedRegisteredUserId = 10;
            int expectedSessionId = 20;
            int expectedScore = 3;

            IRegisteredUserRepository registeredUserService = new StubIRegisteredUserRepository()
            {
                AddRegisteredUserScoreInt32Int32Double = (registeredUserId, sessionId, score) =>
                {
                    Assert.AreEqual(expectedRegisteredUserId, registeredUserId);
                    Assert.AreEqual(expectedSessionId, sessionId);
                    Assert.AreEqual(expectedScore, score);
                    called = true;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return expectedRegisteredUserId; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };
                var target = new RegisteredUsersController(registeredUserService);

                target.PostRegisteredUserScore(expectedRegisteredUserId, expectedSessionId, expectedScore);

                Assert.IsTrue(called);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PostRegisteredUserScore_Call_UnauthorizedException_Test()
        {
            bool called = false;
            int expectedRegisteredUserId = 10;
            int expectedSessionId = 20;
            int expectedScore = 3;

            IRegisteredUserRepository registeredUserService = new StubIRegisteredUserRepository();
            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 10000; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };
                var target = new RegisteredUsersController(registeredUserService);

                target.PostRegisteredUserScore(expectedRegisteredUserId, expectedSessionId, expectedScore);

                Assert.IsTrue(called);
            }
        }

        [TestMethod]
        public void AddRegisteredUserToEvent_Call_NotFail_Test()
        {
            bool called = false;
            int expectedRegisteredUserId = 10;
            int expectedEventId = 20;

            IRegisteredUserRepository registeredUserService = new StubIRegisteredUserRepository()
            {
                AddRegisteredUserToEventInt32Int32 = (registeredUserId, eventId) =>
                {
                    Assert.AreEqual(expectedRegisteredUserId, registeredUserId);
                    Assert.AreEqual(expectedEventId, eventId);
                    called = true;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return expectedRegisteredUserId; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new RegisteredUsersController(registeredUserService);

                target.PostRegisteredUserToEvent(expectedRegisteredUserId, expectedEventId);

                Assert.IsTrue(called);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void AddRegisteredUserToEvent_Call_UnauthorizedException_Test()
        {
            bool called = false;
            int expectedRegisteredUserId = 10;
            int expectedEventId = 20;

            IRegisteredUserRepository registeredUserService = new StubIRegisteredUserRepository();
            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 10000; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new RegisteredUsersController(registeredUserService);

                target.PostRegisteredUserToEvent(expectedRegisteredUserId, expectedEventId);

                Assert.IsTrue(called);
            }
        }

        [TestMethod]
        public void DeleteRegisteredUserFromEvent_Call_NotFail_Test()
        {
            bool called = false;
            int expectedRegisteredUserId = 10;
            int expectedEventId = 20;

            IRegisteredUserRepository registeredUserService = new StubIRegisteredUserRepository()
            {
                DeleteRegisteredUserFromEventInt32Int32 = (registeredUserId, eventId) =>
                {
                    Assert.AreEqual(expectedRegisteredUserId, registeredUserId);
                    Assert.AreEqual(expectedEventId, eventId);
                    called = true;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return expectedRegisteredUserId; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new RegisteredUsersController(registeredUserService);

                target.DeleteRegisteredUserFromEvent(expectedRegisteredUserId, expectedEventId);

                Assert.IsTrue(called);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void DeleteRegisteredUserFromEvent_Call_UnauthorizedException_Test()
        {
            bool called = false;
            int expectedRegisteredUserId = 10;
            int expectedEventId = 20;

            IRegisteredUserRepository registeredUserService = new StubIRegisteredUserRepository();
            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 10000; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new RegisteredUsersController(registeredUserService);

                target.DeleteRegisteredUserFromEvent(expectedRegisteredUserId, expectedEventId);

                Assert.IsTrue(called);
            }
        }

    }
}
