using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using MyEvents.Api.Authentication;
using MyEvents.Data;
using MyEvents.Model;

namespace MyEvents.Api.Tests.WebTests
{
    [TestClass]
    public class WebEventDefinitionTests
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }



        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void GetEventDefinitionCountIntegration_CallWebAPI_GetCount_NotFail_Test()
        {
            int expected = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            using (var context = new MyEventsContext())
            {
                expected = context.EventDefinitions.Count();
            }

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);

            IAsyncResult ar = service.EventDefinitionService.GetEventDefinitionCountAsync((int eventDefinitionId) =>
            {
                TestHelper.ValidateResult(expected, eventDefinitionId, manualResetEvent, ref exceptionResult);
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);

        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void GetAllEventDefinitions_CallWebAPI_GetDefinitionsPaged_NotFail_Test()
        {
            int expected = 0;
            int pageSize = 0;
            int pageIndex = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            using (var context = new MyEventsContext())
            {
                expected = context.EventDefinitions.Count();
                pageSize = expected;
            }

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();

            var service = new Client.MyEventsClient(urlPrefix);
            IAsyncResult ar = service.EventDefinitionService.GetAllEventDefinitionsAsync(pageSize, pageIndex, (IList<Client.EventDefinition> results) => 
            {
                ValidateAttendeeCount(expected, manualResetEvent, ref exceptionResult, results);
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void GetLastEventDefinitions_CallWebAPI_NotFail_Test()
        {
            int expected = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            using (var context = new MyEventsContext())
            {
                expected = context.EventDefinitions.Count();
            }

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();

            var service = new Client.MyEventsClient(urlPrefix);

            IAsyncResult ar = service.EventDefinitionService.GetLastEventDefinitionsAsync(expected, (IList<Client.EventDefinition> results) =>
            {
                ValidateAttendeeCount(expected, manualResetEvent, ref exceptionResult, results);

            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        private static void ValidateAttendeeCount(int expected, ManualResetEvent manualResetEvent, ref Exception exceptionResult, IEnumerable<Client.EventDefinition> results)
        {
            try
            {
                Assert.AreEqual(expected, results.Count());

                // Validate that the events contains the number of attendees. it´s very important to get this value through API
                using (var context = new MyEventsContext())
                {
                    foreach (var result in results)
                    {
                        expected = context.EventDefinitions.Include("RegisteredUsers")
                            .Where(q => q.EventDefinitionId == result.EventDefinitionId)
                            .Select(q => q.RegisteredUsers.Count).First();

                        Assert.AreEqual(expected, result.AttendeesCount);
                    }
                }
            }
            catch (Exception ex)
            {
                exceptionResult = ex;
            }
            finally
            {
                manualResetEvent.Set();
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void GetEventDefinitionCountByOrganizerId_CallWebAPI_GetDefinitionCount_NotFail_Test()
        {
            int expected = 0;
            int organizerId = 0;
            string filter = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            using (var context = new MyEventsContext())
            {
                organizerId = context.EventDefinitions.First().OrganizerId;
                expected = context.EventDefinitions.Where(q => q.OrganizerId == organizerId).Count();
            }

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);

            IAsyncResult ar = service.EventDefinitionService.GetEventDefinitionCountByOrganizerIdAsync(organizerId, filter, (int actual) =>
            {
                TestHelper.ValidateResult(expected, actual, manualResetEvent, ref exceptionResult);
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void GetEventDefinitionByOrganizerId_CallWebAPI_GetDefinitionPaged_NotFail_Test()
        {
            int expected = 0;
            int organizerId = 0;
            int pageSize = 0;
            int pageIndex = 0;
            string filter = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            using (var context = new MyEventsContext())
            {
                organizerId = context.EventDefinitions.First().OrganizerId;
                expected = context.EventDefinitions.Where(q => q.OrganizerId == organizerId).Count();
                pageSize = expected;
            }

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);

            IAsyncResult ar = service.EventDefinitionService.GetEventDefinitionByOrganizerIdAsync(organizerId, filter, pageSize, pageIndex, (IList<Client.EventDefinition> actual) =>
            {
                TestHelper.ValidateResult(expected, actual.Count(), manualResetEvent, ref exceptionResult);
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void GetEventDefinitionById_CallWebAPI_GetDefinition_NotFail_Test()
        {
            EventDefinition expected;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            using (var context = new MyEventsContext())
            {
                expected = context.EventDefinitions.First();
            }

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);
            service.SetAccessToken(MyEventsToken.CreateToken(expected.OrganizerId)); 
            IAsyncResult ar = service.EventDefinitionService.GetEventDefinitionByIdAsync(expected.EventDefinitionId, (Client.EventDefinition actual) =>
            {
                try
                {
                    Assert.AreEqual(expected.Name, actual.Name);
                    Assert.AreEqual(expected.OrganizerId, actual.OrganizerId);
                    Assert.AreEqual(expected.EventDefinitionId, actual.EventDefinitionId);
                }
                catch (Exception ex)
                {
                    exceptionResult = ex;
                }
                finally
                {
                    manualResetEvent.Set();
                }
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void PostEventDefinition_CallWebAPI_EventAdded_NotFail_Test()
        {
            int organizerId = 0;
            int expected = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            // Method to Test
            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();

            // Get values to test
            using (var context = new MyEventsContext())
            {
                organizerId = context.EventDefinitions.First().OrganizerId;
                expected = context.EventDefinitions.Count(q => q.OrganizerId == organizerId) + 1;
            }

            // Create object to add
            var eventDef = new Client.EventDefinition();
            eventDef.OrganizerId = organizerId;
            eventDef.Name = Guid.NewGuid().ToString();
            eventDef.Description = Guid.NewGuid().ToString();
            eventDef.Address = Guid.NewGuid().ToString();
            eventDef.City = Guid.NewGuid().ToString();
            eventDef.Tags = Guid.NewGuid().ToString();
            eventDef.TwitterAccount = Guid.NewGuid().ToString();
            eventDef.RoomNumber = 1;
            eventDef.Date = System.DateTime.Now;
            eventDef.StartTime = System.DateTime.Now;
            eventDef.EndTime = System.DateTime.Now.AddDays(1);
            eventDef.TimeZoneOffset = 2;
            eventDef.Latitude = 0;
            eventDef.Longitude = 0;
            eventDef.Likes = 0;
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            eventDef.Logo = encoding.GetBytes("sample");

            var service = new Client.MyEventsClient(urlPrefix);
            service.SetAccessToken(MyEventsToken.CreateToken(eventDef.OrganizerId));
            IAsyncResult ar = service.EventDefinitionService.AddEventDefinitionAsync(eventDef, (int eventDefinitionId) =>
            {
                try
                {
                    // Asserts
                    using (var context = new MyEventsContext())
                    {
                        Assert.IsTrue(eventDefinitionId > 0);
                        int actual = context.EventDefinitions.Count(q => q.OrganizerId == organizerId);
                        Assert.AreEqual(expected, actual);
                    }
                }
                catch (Exception ex)
                {
                    exceptionResult = ex;
                }
                finally
                {
                    manualResetEvent.Set();
                }
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void PutEventDefinition_CallWebAPI_EventUpdated_NotFail_Test()
        {
            EventDefinition expected;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            // Get values to test
            using (var context = new MyEventsContext())
            {
                expected = context.EventDefinitions.First();
            }

            // Get Event To Update
            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();

            var service = new Client.MyEventsClient(urlPrefix);
            service.SetAccessToken(MyEventsToken.CreateToken(expected.OrganizerId)); 
            IAsyncResult ar = service.EventDefinitionService.GetEventDefinitionByIdAsync(expected.EventDefinitionId, (Client.EventDefinition eventToUpdate) =>
            {
                eventToUpdate.Name = Guid.NewGuid().ToString();
                eventToUpdate.RegisteredUsers = null;
                eventToUpdate.Organizer = null;
                eventToUpdate.Sessions = null;
                IAsyncResult arUpdate = service.EventDefinitionService.UpdateEventDefinitionAsync(eventToUpdate, (HttpStatusCode statusCode) =>
                {
                    using (var context = new MyEventsContext())
                    {
                        string actual = context.EventDefinitions.FirstOrDefault(q => q.EventDefinitionId == eventToUpdate.EventDefinitionId).Name;
                        TestHelper.ValidateResult(eventToUpdate.Name, actual, manualResetEvent, ref exceptionResult);
                    }
                });

            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void DeleteEventDefinition_CallWebAPI_EventDeleted_NotFail_Test()
        {
            EventDefinition eventDef;
            int organizerId = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            // Get values to test
            using (var context = new MyEventsContext())
            {
                organizerId = context.RegisteredUsers.First().RegisteredUserId;

                // Create event to delete
                eventDef = new EventDefinition();
                eventDef.OrganizerId = organizerId;
                eventDef.Name = Guid.NewGuid().ToString();
                eventDef.Description = Guid.NewGuid().ToString();
                eventDef.Address = Guid.NewGuid().ToString();
                eventDef.City = Guid.NewGuid().ToString();
                eventDef.Tags = Guid.NewGuid().ToString();
                eventDef.TwitterAccount = Guid.NewGuid().ToString();
                eventDef.RoomNumber = 1;
                eventDef.Date = System.DateTime.Now;
                eventDef.StartTime = System.DateTime.Now;
                eventDef.EndTime = System.DateTime.Now.AddDays(1);
                eventDef.TimeZoneOffset = 2;
                eventDef.Likes = 0;
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                eventDef.Logo = encoding.GetBytes("sample");
                context.EventDefinitions.Add(eventDef);
                context.SaveChanges();
            }

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();

            var service = new Client.MyEventsClient(urlPrefix);
            service.SetAccessToken(MyEventsToken.CreateToken(eventDef.OrganizerId)); 
            IAsyncResult ar = service.EventDefinitionService.GetEventDefinitionByIdAsync(eventDef.EventDefinitionId, (Client.EventDefinition getEvent) =>
            {
                IAsyncResult arUpdate = service.EventDefinitionService.DeleteEventDefinitionAsync(getEvent.EventDefinitionId, (HttpStatusCode statusCode) =>
                {
                    using (var context = new MyEventsContext())
                    {
                        var actual = context.EventDefinitions.FirstOrDefault(q => q.EventDefinitionId == eventDef.EventDefinitionId);
                        TestHelper.ValidateResult(null, actual, manualResetEvent, ref exceptionResult);
                    }
                });

            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

    }
}
