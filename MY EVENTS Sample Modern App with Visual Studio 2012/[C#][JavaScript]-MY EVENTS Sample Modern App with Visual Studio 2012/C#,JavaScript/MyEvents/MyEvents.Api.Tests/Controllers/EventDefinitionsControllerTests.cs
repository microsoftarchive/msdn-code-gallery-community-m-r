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
    public class EventDefinitionsControllerTests
    {
        [TestMethod]
        public void EventDefinitionsController_Contructor_NotFail_Test()
        {
            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository();
            var target = new EventDefinitionsController(eventDefinitionService);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EventDefinitionsController_ContructorWithNullDependency_Fail_Test()
        {
            var target = new EventDefinitionsController(null);
        }

        [TestMethod]
        public void GetEventDefinitionCountGetResult_NotFail_Test()
        {
            int expected = 10;
            bool called = false;

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                GetCount = () =>
                    {
                        called = true;
                        return expected;
                    }
            };

            var target = new EventDefinitionsController(eventDefinitionService);

            int actual = target.GetEventDefinitionCount();

            Assert.IsTrue(called); 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetAllEventDefinitions_GetResult_NotFail_Test()
        {
            var expected = new List<EventDefinition>() { new EventDefinition() };
            bool called = false;
            int expectedPageSize = 10;
            int expectedPageIndex = 1;

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                GetAllWithUserInfoInt32Int32Int32 = (userId, pageSize, pageIndex) =>
                {
                    Assert.AreEqual(expectedPageSize, pageSize);
                    Assert.AreEqual(expectedPageIndex, pageIndex);
                    called = true;
                    return expected;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 0; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new EventDefinitionsController(eventDefinitionService);

                IEnumerable<EventDefinition> actual = target.GetAllEventDefinitions(expectedPageSize, expectedPageIndex);

                Assert.IsTrue(called);
                Assert.AreEqual(expected.Count, actual.Count());
            }

        }

        [TestMethod]
        public void GetAllEventDefinitions_NotGetResult_NotFail_Test()
        {
            var expected = new List<EventDefinition>();
            bool called = false;
            int expectedPageSize = 10;
            int expectedPageIndex = 1;

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                GetAllWithUserInfoInt32Int32Int32 = (userId, pageSize, pageIndex) =>
                {
                    Assert.AreEqual(expectedPageSize, pageSize);
                    Assert.AreEqual(expectedPageIndex, pageIndex);
                    called = true;
                    return expected;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 0; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new EventDefinitionsController(eventDefinitionService);

                IEnumerable<EventDefinition> actual = target.GetAllEventDefinitions(expectedPageSize, expectedPageIndex);

                Assert.IsTrue(called);
                Assert.AreEqual(expected.Count, actual.Count());
            }


        }

        [TestMethod]
        public void GetAllEventDefinitions_GetNull_NotFailt_Test()
        {
            bool called = false;
            int expectedPageSize = 10;
            int expectedPageIndex = 1;

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                GetAllWithUserInfoInt32Int32Int32 = (userId, pageSize, pageIndex) =>
                {
                    Assert.AreEqual(expectedPageSize, pageSize);
                    Assert.AreEqual(expectedPageIndex, pageIndex);
                    called = true;
                    return null;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 0; }; 
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };


                var target = new EventDefinitionsController(eventDefinitionService);

                IEnumerable<EventDefinition> actual = target.GetAllEventDefinitions(expectedPageSize, expectedPageIndex);

                Assert.IsTrue(called);
                Assert.IsNull(actual);
            }


        }

        [TestMethod]
        public void GetEventDefinitionCountByOrganizerId_GetResult_NotFail_Test()
        {
            int expected = 10;
            int expectedOrganizerId = 1;
            bool called = false;
            string expectedFilter = "testfilter";

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                GetCountByOrganizerIdInt32String = (organizerId, filter) =>
                {
                    Assert.AreEqual(expectedOrganizerId, organizerId);
                    Assert.AreEqual(expectedFilter, filter);
                    called = true;
                    return expected;
                }
            };

            var target = new EventDefinitionsController(eventDefinitionService);

            int actual = target.GetEventDefinitionCountByOrganizerId(expectedOrganizerId, expectedFilter);

            Assert.IsTrue(called); 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetEventDefinitionByOrganizerId_GetResult_NotFail_Test()
        {
            var expected = new List<EventDefinition>() { new EventDefinition() };
            bool called = false;
            int expectedOrganizerId = 10;
            string expectedFilter = "testfilter";
            int expectedPageSize = 10;
            int expectedPageIndex = 1;

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                GetByOrganizerIdInt32StringInt32Int32Boolean = (organizerId, filter, pageSize, pageIndex, completeInfo) =>
                {
                    Assert.AreEqual(expectedOrganizerId, organizerId);
                    Assert.AreEqual(expectedFilter, filter);
                    Assert.AreEqual(expectedPageSize, pageSize);
                    Assert.AreEqual(expectedPageIndex, pageIndex);
                    Assert.IsFalse(completeInfo);
                    called = true;
                    return expected;
                }
            };

            var target = new EventDefinitionsController(eventDefinitionService);

            IEnumerable<EventDefinition> actual = target.GetEventDefinitionByOrganizerId(expectedOrganizerId, expectedFilter, expectedPageSize, expectedPageIndex);

            Assert.IsTrue(called);
            Assert.AreEqual(expected.Count, actual.Count());
        }

        [TestMethod]
        public void GetEventDefinitionByOrganizerId_GetEmptyResult_NotFail_Test()
        {
            var expected = new List<EventDefinition>();
            bool called = false;
            int expectedOrganizerId = 10;
            string expectedFilter = "testfilter";
            int expectedPageSize = 10;
            int expectedPageIndex = 1;

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                GetByOrganizerIdInt32StringInt32Int32Boolean = (organizerId, filter, pageSize, pageIndex, completeInfo) =>
                {
                    Assert.AreEqual(expectedOrganizerId, organizerId);
                    Assert.AreEqual(expectedFilter, filter);
                    Assert.AreEqual(expectedPageSize, pageSize);
                    Assert.AreEqual(expectedPageIndex, pageIndex);
                    Assert.IsFalse(completeInfo);
                    called = true;
                    return expected;
                }
            };

            var target = new EventDefinitionsController(eventDefinitionService);

            IEnumerable<EventDefinition> actual = target.GetEventDefinitionByOrganizerId(expectedOrganizerId, expectedFilter, expectedPageSize, expectedPageIndex);

            Assert.IsTrue(called);
            Assert.AreEqual(expected.Count, actual.Count());
        }


        [TestMethod]
        public void GetEventDefinitionByOrganizerId_GetNull_NotFail_Test()
        {
            bool called = false;
            int expectedOrganizerId = 10;
            string expectedFilter = "testfilter";
            int expectedPageSize = 10;
            int expectedPageIndex = 1;

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                GetByOrganizerIdInt32StringInt32Int32Boolean = (organizerId, filter, pageSize, pageIndex, completeInfo) =>
                {
                    Assert.AreEqual(expectedOrganizerId, organizerId);
                    Assert.AreEqual(expectedFilter, filter);
                    Assert.AreEqual(expectedPageSize, pageSize);
                    Assert.AreEqual(expectedPageIndex, pageIndex);
                    Assert.IsFalse(completeInfo);
                    called = true;
                    return null;
                }
            };

            var target = new EventDefinitionsController(eventDefinitionService);

            IEnumerable<EventDefinition> actual = target.GetEventDefinitionByOrganizerId(expectedOrganizerId, expectedFilter, expectedPageSize, expectedPageIndex);

            Assert.IsTrue(called);
            Assert.IsNull(actual);
        }


        [TestMethod]
        public void GetEventDefinitionById_GetResult_NotFail_Test()
        {
            var expected = new EventDefinition() { Name = "EventName" };
            bool called = false;
            int expectedEventDefinitionId = 10;

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                GetByIdWithUserInfoInt32Int32 = (userId, eventDefinitionId) =>
                {
                    Assert.AreEqual(expectedEventDefinitionId, eventDefinitionId);
                    called = true;
                    return expected;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 0; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new EventDefinitionsController(eventDefinitionService);

                EventDefinition actual = target.GetEventDefinitionById(expectedEventDefinitionId);

                Assert.IsTrue(called);
                Assert.AreEqual(expected.Name, actual.Name);
            }

        }

        [TestMethod]
        public void GetEventDefinitionById_GetNull_NotFail_Test()
        {
            bool called = false;
            int expectedEventDefinitionId = 10;

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                GetByIdWithUserInfoInt32Int32 = (userId, eventDefinitionId) =>
                {
                    Assert.AreEqual(expectedEventDefinitionId, eventDefinitionId);
                    called = true;
                    return null;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 0; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new EventDefinitionsController(eventDefinitionService);

                EventDefinition actual = target.GetEventDefinitionById(expectedEventDefinitionId);

                Assert.IsTrue(called);
                Assert.IsNull(actual);
            }


        }

        [TestMethod]
        public void PostEventDefinition_NotFail_Test()
        {
            var expected = new EventDefinition() { Name = "EventName", EventDefinitionId = 1, OrganizerId = 1};
            bool called = false;

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                AddEventDefinition = (eventDefinition) =>
                {
                    Assert.AreEqual(expected.EventDefinitionId, eventDefinition.EventDefinitionId);
                    Assert.AreEqual(expected.Name, eventDefinition.Name);
                    called = true;
                    return eventDefinition.EventDefinitionId;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return expected.OrganizerId; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };


                var target = new EventDefinitionsController(eventDefinitionService);

                int actualId = target.Post(expected);

                Assert.IsTrue(called);
                Assert.AreEqual(expected.EventDefinitionId, actualId);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PostEventDefinition_UnauthorizedException_Test()
        {
            var expected = new EventDefinition() { Name = "EventName", EventDefinitionId = 1, OrganizerId = 1 };
            bool called = false;

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository();
            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 10; }; // It´s not authorized! 
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };


                var target = new EventDefinitionsController(eventDefinitionService);

                int actualId = target.Post(expected);

                Assert.IsTrue(called);
                Assert.AreEqual(expected.EventDefinitionId, actualId);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PostEventDefinition_ArgumentNullException_Test()
        {
            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository();
            var target = new EventDefinitionsController(eventDefinitionService);

            target.Post(null);
        }


        [TestMethod]
        public void PutEventDefinition_NotFail_Test()
        {
            var expected = new EventDefinition() { Name = "EventName", EventDefinitionId = 1, OrganizerId = 1 };
            bool called = false;

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                UpdateEventDefinition = (eventDefinition) =>
                {
                    Assert.AreEqual(expected.EventDefinitionId, eventDefinition.EventDefinitionId);
                    Assert.AreEqual(expected.Name, eventDefinition.Name);
                    called = true;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return expected.OrganizerId; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new EventDefinitionsController(eventDefinitionService);

                target.Put(expected);

                Assert.IsTrue(called);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PutEventDefinition_UnauthorizedException_Test()
        {
            var expected = new EventDefinition() { Name = "EventName", EventDefinitionId = 1, OrganizerId = 1 };
            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository();
            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 10000; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new EventDefinitionsController(eventDefinitionService);

                target.Put(expected);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PutEventDefinition_ArgumentNullException_Test()
        {
            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository();
            var target = new EventDefinitionsController(eventDefinitionService);

            target.Put(null);
        }

        [TestMethod]
        public void DeleteEventDefinition_NotFail_Test()
        {
            var expected = new EventDefinition() { Name = "EventName", EventDefinitionId = 1, OrganizerId = 1 };
            bool called = false;

            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                DeleteInt32 = (eventDefinitionId) =>
                {
                    Assert.AreEqual(expected.EventDefinitionId, eventDefinitionId);
                    called = true;
                },
                GetByIdInt32 = (id) =>
                {
                    Assert.IsTrue(id == 1);
                    return expected;
                }
            };

            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return expected.OrganizerId; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new EventDefinitionsController(eventDefinitionService);

                target.Delete(expected.EventDefinitionId);

                Assert.IsTrue(called);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void DeleteEventDefinition_UnauthorizedException_Test()
        {
            var expected = new EventDefinition() { Name = "EventName", EventDefinitionId = 1, OrganizerId = 1 };
            IEventDefinitionRepository eventDefinitionService = new StubIEventDefinitionRepository()
            {
                GetByIdInt32 = ( id) =>
                {
                    Assert.IsTrue(id == 1);
                    return expected;
                }
            };
            using (ShimsContext.Create())
            {
                MyEvents.Api.Authentication.Fakes.ShimMyEventsToken myeventToken = new Authentication.Fakes.ShimMyEventsToken();
                myeventToken.RegisteredUserIdGet = () => { return 10; };
                ShimMyEventsToken.GetTokenFromHeader = () => { return myeventToken; };

                var target = new EventDefinitionsController(eventDefinitionService);

                target.Delete(expected.EventDefinitionId);
            }
        }

     
    }
}
