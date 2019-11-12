using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyEvents.Api.Controllers;
using MyEvents.Model;
using MyEvents.Data;
using MyEvents.Data.Fakes;
using MyEvents.Api.Authentication.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using System.Web;
using System.Net.Http;
using System.Web.Http;

namespace MyEvents.Api.Tests.Controllers
{
    [TestClass]
    public class SessionsControllerTests
    {
        [TestMethod]
        public void SessionsController_Contructor_NotFail_Test()
        {
            ISessionRepository sessionRepository = new StubISessionRepository();
            IEventDefinitionRepository eventRepository = new StubIEventDefinitionRepository();
            var target = new SessionsController(sessionRepository, eventRepository);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SessionsController_ContructorSessionWithNullDependency_Fail_Test()
        {
            ISessionRepository sessionRepository = new StubISessionRepository();
            IEventDefinitionRepository eventRepository = new StubIEventDefinitionRepository();
            var target = new SessionsController(null , eventRepository);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SessionsController_ContructorEventWithNullDependency_Fail_Test()
        {
            ISessionRepository sessionRepository = new StubISessionRepository();
            IEventDefinitionRepository eventRepository = new StubIEventDefinitionRepository();
            var target = new SessionsController(sessionRepository, null);
        }

        [TestMethod]
        public void GetAllSessions_GetEmptyResults_NotFail_Test()
        {
            int expectedEventDefinitionId = 10;
            bool called = false;
            var expected = new List<Session>();

            IEventDefinitionRepository eventRepository = new StubIEventDefinitionRepository();
            ISessionRepository sessionRepository = new StubISessionRepository()
            {
                GetAllWithUserInfoInt32Int32 = (userId, eventId) =>
                {
                    Assert.AreEqual(expectedEventDefinitionId, eventId);
                    called = true;
                    return expected;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 0; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new SessionsController(sessionRepository, eventRepository);

                IEnumerable<Session> actual = target.GetAll(expectedEventDefinitionId);

                Assert.IsTrue(called);
                Assert.AreEqual(expected.Count, actual.Count());
            }

        }

        [TestMethod]
        public void GetAllSessions_GetResults_NotFail_Test()
        {
            int expectedEventDefinitionId = 10;
            bool called = false;
            var expected = new List<Session>() { new Session() };

            IEventDefinitionRepository eventRepository = new StubIEventDefinitionRepository();
            ISessionRepository sessionRepository = new StubISessionRepository()
            {
                GetAllWithUserInfoInt32Int32 = (userId, eventId) =>
                {
                    Assert.AreEqual(expectedEventDefinitionId, eventId);
                    called = true;
                    return expected;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 0; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new SessionsController(sessionRepository, eventRepository);

                IEnumerable<Session> actual = target.GetAll(expectedEventDefinitionId);

                Assert.IsTrue(called);
                Assert.AreEqual(expected.Count, actual.Count());
            }
        }

        [TestMethod]
        public void GetSession_GetResult_NotFail_Test()
        {
            int expectedSessionId = 10;
            bool called = false;
            var expected = new Session() { SessionId = expectedSessionId };

            IEventDefinitionRepository eventRepository = new StubIEventDefinitionRepository();
            ISessionRepository sessionRepository = new StubISessionRepository()
            {
                GetWithUserInfoInt32Int32 = (userId, eventId) =>
                {
                    Assert.AreEqual(expectedSessionId, eventId);
                    called = true;
                    return expected;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 0; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new SessionsController(sessionRepository, eventRepository);

                Session actual = target.Get(expectedSessionId);

                Assert.IsTrue(called);
                Assert.AreEqual(expectedSessionId, actual.SessionId);
            }


        }

        [TestMethod]
        public void PostSession_NotFail_Test()
        {
            bool called = false;
            var expectedSession = new Session() { SessionId = 1, EventDefinitionId = 1 };
            var eventDefinition = new EventDefinition() { OrganizerId = 1 };

            IEventDefinitionRepository eventRepository = new StubIEventDefinitionRepository()
            {
                GetByIdInt32 = (id) =>
                {
                    Assert.AreEqual(expectedSession.EventDefinitionId, id);
                    return eventDefinition; 
                }
            };

            ISessionRepository sessionRepository = new StubISessionRepository()
            {
                AddSession = session =>
                {
                    Assert.AreEqual(expectedSession.SessionId, session.SessionId);
                    called = true;
                    return session.SessionId;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return eventDefinition.OrganizerId; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new SessionsController(sessionRepository, eventRepository);

                int actual = target.Post(expectedSession);

                Assert.IsTrue(called);
                Assert.AreEqual(expectedSession.SessionId, actual);
            }

        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PostSession_UnauthorizedException_Test()
        {
            var expectedSession = new Session() { SessionId = 1, EventDefinitionId = 1 };
            var eventDefinition = new EventDefinition() { OrganizerId = 1 };

            IEventDefinitionRepository eventRepository = new StubIEventDefinitionRepository()
            {
                GetByIdInt32 = (id) =>
                {
                    Assert.AreEqual(expectedSession.EventDefinitionId, id);
                    return eventDefinition;
                }
            };

            ISessionRepository sessionRepository = new StubISessionRepository();
            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 10000; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new SessionsController(sessionRepository, eventRepository);

                int actual = target.Post(expectedSession);
            }

        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PostSession_ArgumentNullException_Test()
        {
            ISessionRepository sessionRepository = new StubISessionRepository();
            IEventDefinitionRepository eventRepository = new StubIEventDefinitionRepository();

            var target = new SessionsController(sessionRepository, eventRepository);

            target.Post(null);
        }

        [TestMethod]
        public void PutSession_NotFail_Test()
        {
            bool called = false;
            var expectedSession = new Session() { SessionId = 1 };
            var eventDefinition = new EventDefinition() { OrganizerId = 1 };

            IEventDefinitionRepository eventRepository = new StubIEventDefinitionRepository()
            {
                GetByIdInt32 = (id) =>
                {
                    Assert.AreEqual(expectedSession.EventDefinitionId, id);
                    return eventDefinition;
                }
            };
            ISessionRepository sessionRepository = new StubISessionRepository()
            {
                UpdateSession = session =>
                {
                    Assert.AreEqual(expectedSession.SessionId, session.SessionId);
                    called = true;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return eventDefinition.OrganizerId; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new SessionsController(sessionRepository, eventRepository);

                target.Put(expectedSession);

                Assert.IsTrue(called);
            }


        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PutSession_UnauthorizedException_Test()
        {
            var expectedSession = new Session() { SessionId = 1 };
            var eventDefinition = new EventDefinition() { OrganizerId = 1 };

            IEventDefinitionRepository eventRepository = new StubIEventDefinitionRepository()
            {
                GetByIdInt32 = (id) =>
                {
                    Assert.AreEqual(expectedSession.EventDefinitionId, id);
                    return eventDefinition;
                }
            };
            ISessionRepository sessionRepository = new StubISessionRepository();
            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 1000; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new SessionsController(sessionRepository, eventRepository);

                target.Put(expectedSession);
            }


        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PutSession_ArgumentNullException_Test()
        {
            ISessionRepository sessionRepository = new StubISessionRepository();
            IEventDefinitionRepository eventRepository = new StubIEventDefinitionRepository();

            var target = new SessionsController(sessionRepository, eventRepository);

            target.Put(null);
        }

        [TestMethod]
        public void PutSessionPeriod_NotFail_Test()
        {
            int expectedSessionId = 5;
            int duration = 10;
            DateTime startTime = DateTime.Now;
            bool getCalled = false;
            bool updateCalled = false;
            var expectedSession = new Session() { SessionId = expectedSessionId, EventDefinitionId = 1 };
            var eventDefinition = new EventDefinition() { OrganizerId = 1 };

            IEventDefinitionRepository eventRepository = new StubIEventDefinitionRepository()
            {
                GetByIdInt32 = (id) =>
                {
                    Assert.AreEqual(expectedSession.EventDefinitionId, id);
                    return eventDefinition;
                }
            };

            ISessionRepository sessionRepository = new StubISessionRepository()

            {
                GetInt32 = (sessionId) =>
                {
                    Assert.AreEqual(expectedSessionId, sessionId);
                    getCalled = true;
                    return expectedSession;
                },
                UpdateSession = session =>
                {
                    Assert.AreEqual(expectedSessionId, session.SessionId);
                    Assert.AreEqual(duration, session.Duration);
                    updateCalled = true;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return eventDefinition.OrganizerId; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new SessionsController(sessionRepository, eventRepository);

                target.PutSessionPeriod(expectedSessionId, startTime.ToString(), duration);

                Assert.IsTrue(getCalled);
                Assert.IsTrue(updateCalled);
            }


        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PutSessionPeriod_UnauthorizedException_Test()
        {
            int expectedSessionId = 5;
            int duration = 10;
            DateTime startTime = DateTime.Now;
            var expectedSession = new Session() { SessionId = expectedSessionId, EventDefinitionId = 1 };
            var eventDefinition = new EventDefinition() { OrganizerId = 1 };

            IEventDefinitionRepository eventRepository = new StubIEventDefinitionRepository()
            {
                GetByIdInt32 = (id) =>
                {
                    Assert.AreEqual(expectedSession.EventDefinitionId, id);
                    return eventDefinition;
                }
            };

            ISessionRepository sessionRepository = new StubISessionRepository()

            {
                GetInt32 = (sessionId) =>
                {
                    Assert.AreEqual(expectedSessionId, sessionId);
                    return expectedSession;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 1000; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new SessionsController(sessionRepository, eventRepository);

                target.PutSessionPeriod(expectedSessionId, startTime.ToString(), duration);
            }
        }

        [TestMethod]
        public void PutSessionPeriod_SessionNotFound_NotFail_Test()
        {
            int expectedSessionId = 5;
            int duration = 10;
            DateTime startTime = DateTime.Now;
            bool getCalled = false;
            bool updateCalled = false;
            var expectedSession = new Session() { SessionId = expectedSessionId };

            IEventDefinitionRepository eventRepository = new StubIEventDefinitionRepository();
            ISessionRepository sessionRepository = new StubISessionRepository()

            {
                GetInt32 = (sessionId) =>
                {
                    Assert.AreEqual(expectedSessionId, sessionId);
                    getCalled = true;
                    return null;
                },
                UpdateSession = session =>
                {
                    Assert.AreEqual(expectedSessionId, session.SessionId);
                    Assert.AreEqual(startTime, session.StartTime);
                    Assert.AreEqual(duration, session.Duration);
                    updateCalled = true;
                }
            };

            var target = new SessionsController(sessionRepository, eventRepository);

            target.PutSessionPeriod(expectedSessionId, startTime.ToString(), duration);

            Assert.IsTrue(getCalled);
            Assert.IsFalse(updateCalled);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PutSessionPeriod_DurationBadParam_ExceptionExpected_Test()
        {
            int sessionId = 0;
            int duration = 0;
            DateTime startTime = DateTime.Now;

            IEventDefinitionRepository eventRepository = new StubIEventDefinitionRepository();
            ISessionRepository sessionRepository = new StubISessionRepository();

            var target = new SessionsController(sessionRepository, eventRepository);

            target.PutSessionPeriod(sessionId, startTime.ToString(), duration);
        }

        [TestMethod]
        public void DeleteSession_NotFail_Test()
        {
            bool called = false;
            var expectedSession = new Session() { SessionId = 1, EventDefinitionId = 1 };

            var eventDefinition = new EventDefinition() { OrganizerId = 1 };
            IEventDefinitionRepository eventRepository = new StubIEventDefinitionRepository()
            {
                GetByIdInt32 = (id) =>
                {
                    Assert.AreEqual(expectedSession.EventDefinitionId, id);
                    return eventDefinition;
                }
            };

            ISessionRepository sessionRepository = new StubISessionRepository()
            {
                DeleteInt32 = sessionId =>
                {
                    Assert.AreEqual(expectedSession.SessionId, sessionId);
                    called = true;
                },
                GetInt32 = (sessionId) =>
                {
                    Assert.AreEqual(expectedSession.SessionId, sessionId);
                    return expectedSession;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return eventDefinition.OrganizerId; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };
                var target = new SessionsController(sessionRepository, eventRepository);

                target.Delete(expectedSession.SessionId);

                Assert.IsTrue(called);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void DeleteSession_UnauthorizedException_Test()
        {
            var expectedSession = new Session() { SessionId = 1, EventDefinitionId = 1 };

            var eventDefinition = new EventDefinition() { OrganizerId = 1 };
            IEventDefinitionRepository eventRepository = new StubIEventDefinitionRepository()
            {
                GetByIdInt32 = (id) =>
                {
                    Assert.AreEqual(expectedSession.EventDefinitionId, id);
                    return eventDefinition;
                }
            };

            ISessionRepository sessionRepository = new StubISessionRepository()
            {
                GetInt32 = (sessionId) =>
                {
                    Assert.AreEqual(expectedSession.SessionId, sessionId);
                    return expectedSession;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 10000; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new SessionsController(sessionRepository, eventRepository);

                target.Delete(expectedSession.SessionId);
            }
        }
    }
}
