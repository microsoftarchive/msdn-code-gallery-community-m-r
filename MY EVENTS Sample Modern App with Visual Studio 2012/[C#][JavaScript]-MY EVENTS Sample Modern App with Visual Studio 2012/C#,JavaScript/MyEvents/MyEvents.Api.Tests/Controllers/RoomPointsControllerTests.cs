using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyEvents.Api.Controllers;
using MyEvents.Model;
using MyEvents.Data.Fakes;
using MyEvents.Data;
using MyEvents.Api.Authentication;
using MyEvents.Api.Authentication.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using System.Web;
using System.Web.Http;

namespace MyEvents.Api.Tests.Controllers
{
    [TestClass]
    public class RoomPointsControllerTests
    {
        [TestMethod]
        public void RoomPointsController_Contructor_NotFail_Test()
        {
            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository();
            var target = new RoomPointsController(eventDefinitionService);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RoomPointsController_ContructorWithNullDependency_Fail_Test()
        {
            var target = new RoomPointsController(null);
        }

        [TestMethod]
        public void GetRoomImage_NotFail_Test()
        {
            bool called = false;
            var expectedEventId = 1;

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                GetRoomImageInt32 = (eventId) =>
                {
                    Assert.AreEqual(expectedEventId, eventId);
                    called = true;
                    return null;
                }
            };

            var target = new RoomPointsController(eventDefinitionService);

            var result = target.GetRoomImage(expectedEventId);

            Assert.IsTrue(called);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetAllRoomPoints_NotFail_Test()
        {
            bool called = false;
            var expected = new List<RoomPoint>() { new RoomPoint(), new RoomPoint(), new RoomPoint() };
            var expectedEventId = 1;

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                GetAllRoomPointsInt32 = (eventId) =>
                {
                    Assert.AreEqual(expectedEventId, eventId);
                    called = true;
                    return expected;
                }
            };

            var target = new RoomPointsController(eventDefinitionService);

            var result = target.GetAllRoomPoints(expectedEventId);

            Assert.IsTrue(called);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetRoomPoints_NotFail_Test()
        {
            bool called = false;
            var expected = new List<RoomPoint>() { new RoomPoint(), new RoomPoint(), new RoomPoint() };
            var expectedSessionId = 1;

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                GetRoomPointsInt32 = (sessionId) =>
                {
                    Assert.AreEqual(expectedSessionId, sessionId);
                    called = true;
                    return expected;
                }
            };

            var target = new RoomPointsController(eventDefinitionService);

            var result = target.GetRoomPoints(expectedSessionId);

            Assert.IsTrue(called);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PutRoomImage_NotFail_Test()
        {
            bool called = false;
            var expected = new List<RoomPoint>() 
            { new RoomPoint() { EventDefinitionId = 10 }
                , new RoomPoint() { EventDefinitionId = 10 }
                , new RoomPoint() { EventDefinitionId = 10 }};
            EventDefinition expectedEvent = new EventDefinition() { EventDefinitionId = 10 , OrganizerId = 10};

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                UpdateRoomImageEventDefinition = (eventDefinition) =>
                {
                    Assert.AreEqual(expectedEvent.EventDefinitionId, eventDefinition.EventDefinitionId);
                    Assert.IsNull(expectedEvent.MapImage);
                    called = true;
                },
                GetByIdInt32 = (id) =>
                {
                    Assert.IsTrue(id == expectedEvent.EventDefinitionId);
                    return expectedEvent;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return expectedEvent.OrganizerId; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new RoomPointsController(eventDefinitionService);

                target.PutRoomImage(expectedEvent);

                Assert.IsTrue(called);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PutRoomImage_UnauthorizedException_Test()
        {
          
            var expected = new List<RoomPoint>() 
            { new RoomPoint() { EventDefinitionId = 10 }
                , new RoomPoint() { EventDefinitionId = 10 }
                , new RoomPoint() { EventDefinitionId = 10 }};
            EventDefinition expectedEvent = new EventDefinition() { EventDefinitionId = 10, OrganizerId = 10 };

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                GetByIdInt32 = (id) =>
                {
                    Assert.IsTrue(id == expectedEvent.EventDefinitionId);
                    return expectedEvent;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 10000; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new RoomPointsController(eventDefinitionService);

                target.PutRoomImage(expectedEvent);
            }
        }     

        [TestMethod]
        public void PostRoomPoints_NotFail_Test()
        {
            bool called = false;
            var expected = 
                new List<RoomPoint>() { 
                    new RoomPoint() { EventDefinitionId = 1 }, 
                    new RoomPoint() { EventDefinitionId = 1 }, 
                    new RoomPoint() { EventDefinitionId = 1 } };

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                AddRoomPointsIEnumerableOfRoomPoint = (roomPoints) =>
                {
                    Assert.AreEqual(expected.Count(), roomPoints.Count());
                    called = true;
                },
                GetByIdInt32 = (id) =>
                {
                    Assert.IsTrue(id == 1);
                    return new EventDefinition() { OrganizerId = 1 };
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 1; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new RoomPointsController(eventDefinitionService);

                target.PostRoomPoints(expected);

                Assert.IsTrue(called);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PostRoomPoints_UnauthorizedException_Test()
        {
            var expected =
                new List<RoomPoint>() { 
                    new RoomPoint() { EventDefinitionId = 1 }, 
                    new RoomPoint() { EventDefinitionId = 1 }, 
                    new RoomPoint() { EventDefinitionId = 1 } };

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                GetByIdInt32 = (id) =>
                {
                    Assert.IsTrue(id == 1);
                    return new EventDefinition() { OrganizerId = 1 };
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 1000; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new RoomPointsController(eventDefinitionService);

                target.PostRoomPoints(expected);
            }
        }

        [TestMethod]
        public void DeleteRoomPoints_NotFail_Test()
        {
            bool called = false;
            int expectedEventDefinitionId = 1;
            int expectedRoomNumber = 20;

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                DeleteRoomPointsInt32Int32 = (eventDefinitionId, roomNumber) =>
                {
                    Assert.AreEqual(expectedEventDefinitionId, eventDefinitionId);
                    Assert.AreEqual(expectedRoomNumber, roomNumber);
                    called = true;
                },
                GetByIdInt32 = (id) =>
                {
                    Assert.IsTrue(id == expectedEventDefinitionId);
                    return new EventDefinition() { OrganizerId = 1 };
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 1; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new RoomPointsController(eventDefinitionService);

                target.DeleteRoomPoints(expectedEventDefinitionId, expectedRoomNumber);

                Assert.IsTrue(called);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void DeleteRoomPoints_Unauthorized_Test()
        {
            int expectedEventDefinitionId = 1;
            int expectedRoomNumber = 20;

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                GetByIdInt32 = (id) =>
                {
                    Assert.IsTrue(id == expectedEventDefinitionId);
                    return new EventDefinition() { OrganizerId = 1 };
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 10000; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new RoomPointsController(eventDefinitionService);

                target.DeleteRoomPoints(expectedEventDefinitionId, expectedRoomNumber);
            }
        }     
    }
}
