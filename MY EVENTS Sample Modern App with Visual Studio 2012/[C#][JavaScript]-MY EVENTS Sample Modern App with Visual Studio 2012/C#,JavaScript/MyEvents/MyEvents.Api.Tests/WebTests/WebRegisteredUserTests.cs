using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using MyEvents.Api.Authentication;
using MyEvents.Data;
using MyEvents.Model;

namespace MyEvents.Api.Tests.WebTests
{
    [TestClass]
    public class WebRegisteredUserTests
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
        public void GetRegisteredUser_Integration_CallWebAPI_GetResult_NotFail_Test()
        {
            string facebookId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            using (var context = new MyEventsContext())
            {
                facebookId = context.RegisteredUsers.First().FacebookId;
            }

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);

            IAsyncResult ar = service.RegisteredUserService.GetRegisteredUserAsync(facebookId, (Client.RegisteredUser registeredUser) =>
            {
                TestHelper.ValidateResult(facebookId, registeredUser.FacebookId, manualResetEvent, ref exceptionResult);
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);

        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void GetFakeRegisteredUser_Integration_CallWebAPI_GetResult_NotFail_Test()
        {
            string facebookId = string.Empty;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            using (var context = new MyEventsContext())
            {
                facebookId = context.RegisteredUsers.First(q => q.RegisteredUserId == 1).FacebookId; // 1 is set as Fake User
            }

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);

            IAsyncResult ar = service.RegisteredUserService.GetFakeRegisteredUserAsync((Client.RegisteredUser registeredUser) =>
            {
                TestHelper.ValidateResult(facebookId, registeredUser.FacebookId, manualResetEvent, ref exceptionResult);
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);

        }


        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void GetAllRegisteredUsersByEventId_Integration_CallWebAPI_GetRegisteredUsers_NotFail_Test()
        {
            int eventDefinitionId = 0;
            int expected = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            using (var context = new MyEventsContext())
            {
                eventDefinitionId = context.RegisteredUsers.Include("AttendeeEventDefinitions")
                    .First(q => q.AttendeeEventDefinitions.Any()).AttendeeEventDefinitions.First().EventDefinitionId;
                expected = context.RegisteredUsers.Include("AttendeeEventDefinitions").Count(q => q.AttendeeEventDefinitions.Any(s => s.EventDefinitionId == eventDefinitionId));
            }

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);

            IAsyncResult ar = service.RegisteredUserService.GetAllRegisteredUsersByEventIdAsync(eventDefinitionId, (IList<Client.RegisteredUser> registeredUsers) =>
            {
                TestHelper.ValidateResult(expected, registeredUsers.Count(), manualResetEvent, ref exceptionResult);
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void GetAllRegisteredUsersBySessionId_Integration_CallWebAPI_GetRegisteredUsers_NotFail_Test()
        {
            int sessionId = 0;
            int expected = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            using (var context = new MyEventsContext())
            {
                sessionId = context.SessionRegisteredUsers.First().SessionId;
                expected = context.SessionRegisteredUsers.Count(q => q.SessionId == sessionId);
            }

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();

            var service = new Client.MyEventsClient(urlPrefix);

            IAsyncResult ar = service.RegisteredUserService.GetAllRegisteredUsersBySessionIdAsync(sessionId, (IList<Client.RegisteredUser> registeredUsers) =>
            {
                TestHelper.ValidateResult(expected, registeredUsers.Count(), manualResetEvent, ref exceptionResult);
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void PostRegisteredUser_Integration_CallWebAPI_RegisteredUserAdded_NotFail_Test()
        {
            int expected = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            // Method to Test
            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            string urlToTest = String.Format("{0}api/RegisteredUser/PostRegisteredUser", urlPrefix);

            // Get values to test
            using (var context = new MyEventsContext())
            {
                expected = context.RegisteredUsers.Count() + 1;
            }

            // Create object to add
            Client.RegisteredUser registeredUser = new Client.RegisteredUser();
            registeredUser.Name = Guid.NewGuid().ToString();
            registeredUser.FacebookId = Guid.NewGuid().ToString();
            registeredUser.Email = Guid.NewGuid().ToString();

            var service = new Client.MyEventsClient(urlPrefix);
            IAsyncResult ar = service.RegisteredUserService.AddRegisteredUserCreateIfNotExistsAsync(registeredUser, (int registeredUserId) =>
            {
                // Asserts
                using (var context = new MyEventsContext())
                {
                    Assert.IsTrue(registeredUserId > 0);
                    int actual = context.RegisteredUsers.Count();
                    TestHelper.ValidateResult(expected, actual, manualResetEvent, ref exceptionResult);
                }
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
            
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void AddRegisteredUserToSession_Integration_CallWebAPI_RegisteredUserAdded_NotFail_Test()
        {
            int registeredUserId = 0;
            int sessionId = 0;
            int expected = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            // Get values to test
            using (var context = new MyEventsContext())
            {
                registeredUserId = context.RegisteredUsers.First().RegisteredUserId;
                sessionId = context.Sessions.First().SessionId;
                expected = context.SessionRegisteredUsers.Count() + 1;
            }

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);
            service.SetAccessToken(MyEventsToken.CreateToken(registeredUserId));
            IAsyncResult ar = service.RegisteredUserService.AddRegisteredUserToSessionAsync(registeredUserId, sessionId, (HttpStatusCode statusCode) =>
            {
                // Asserts
                using (var context = new MyEventsContext())
                {
                    int actual = context.SessionRegisteredUsers.Count();
                    TestHelper.ValidateResult(expected, actual, manualResetEvent, ref exceptionResult);
                }
            });


            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void DeleteRegisteredUserFromSession_Integration_CallWebAPI_RegisteredUserIdeleted_NotFail_Test()
        {
            int registeredUserId = 0;
            int sessionId = 0;
            int expected = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            // Get values to test
            using (var context = new MyEventsContext())
            {
                SessionRegisteredUser sessionRegisteredUser = context.SessionRegisteredUsers.First();
                registeredUserId = sessionRegisteredUser.RegisteredUserId;
                sessionId = sessionRegisteredUser.SessionId;
                expected = context.SessionRegisteredUsers.Count() - 1;
            }

            // Method to Test
            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);
            service.SetAccessToken(MyEventsToken.CreateToken(registeredUserId));
            IAsyncResult ar = service.RegisteredUserService.DeleteRegisteredUserFromSessionAsync(registeredUserId, sessionId, (HttpStatusCode statusCode) =>
            {
                // Asserts
                using (var context = new MyEventsContext())
                {
                    int actual = context.SessionRegisteredUsers.Count();
                    TestHelper.ValidateResult(expected, actual, manualResetEvent, ref exceptionResult);
                }
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void GetRegisteredUserEventDefinitions_Integration_CallWebAPI_GetEventDefinitionListOftheUser_NotFail_Test()
        {
            int registerUserId = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            using (var context = new MyEventsContext())
            {
                registerUserId = context.RegisteredUsers.FirstOrDefault().RegisteredUserId;
            }

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();

            var service = new Client.MyEventsClient(urlPrefix);

            IAsyncResult ar = service.RegisteredUserService.GetRegisteredUserEventDefinitionsAsync(registerUserId, (IList<Client.EventDefinition> eventDefinitions) =>
            {
                try
                {
                    Assert.IsNotNull(eventDefinitions);
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

            ar.AsyncWaitHandle.WaitOne();
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void PostRegisteredUserScore_Integration_CallWebAPI_RegisteredUserAdded_NotFail_Test()
        {
            int registeredUserId = 0;
            int sessionId = 0;
            int expected = 4;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            // Get values to test
            using (var context = new MyEventsContext())
            {
                registeredUserId = context.SessionRegisteredUsers.First().RegisteredUserId;
                sessionId = context.SessionRegisteredUsers.First().SessionId;
            }

            // Method to Test
            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);
            service.SetAccessToken(MyEventsToken.CreateToken(registeredUserId));
            IAsyncResult ar = service.RegisteredUserService.AddRegisteredUserScoreAsync(registeredUserId, sessionId, expected, (HttpStatusCode statusCode) =>
            {
                // Asserts
                using (var context = new MyEventsContext())
                {
                    double actual = context.SessionRegisteredUsers.Where(q => q.RegisteredUserId == registeredUserId && q.SessionId == sessionId).First().Score;
                    TestHelper.ValidateResult(expected, actual, manualResetEvent, ref exceptionResult);
                }
            });


            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void AddRegisteredUserToEvent_Integration_CallWebAPI_RegisteredUserAdded_NotFail_Test()
        {
            int registeredUserId = 0;
            int eventDefinitionId = 0;
            int expected = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            // Get values to test
            using (var context = new MyEventsContext())
            {
                registeredUserId = context.RegisteredUsers.First().RegisteredUserId;
                var eventDefinition = context.EventDefinitions.Include("RegisteredUsers")
                    .Where(e => !e.RegisteredUsers.Any(r => r.RegisteredUserId == registeredUserId)).First();
                eventDefinitionId = eventDefinition.EventDefinitionId;
                expected = eventDefinition.RegisteredUsers.Count() + 1;
            }

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);
            service.SetAccessToken(MyEventsToken.CreateToken(registeredUserId));
            IAsyncResult ar = service.RegisteredUserService.AddRegisteredUserToEventAsync(registeredUserId, eventDefinitionId, (HttpStatusCode statusCode) =>
            {
                // Asserts
                using (var context = new MyEventsContext())
                {
                    var eventDefinition = context.EventDefinitions.Include("RegisteredUsers").First(e => e.EventDefinitionId == eventDefinitionId);
                    int actual = eventDefinition.RegisteredUsers.Count();
                    TestHelper.ValidateResult(expected, actual, manualResetEvent, ref exceptionResult);
                }
            });


            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void DeleteRegisteredUserFromEvent_Integration_CallWebAPI_RegisteredUserIdeleted_NotFail_Test()
        {
            int registeredUserId = 0;
            int eventDefinitionId = 0;
            int expected = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            // Get values to test
            using (var context = new MyEventsContext())
            {
                var eventDefinition = context.EventDefinitions.Include("RegisteredUsers")
                        .Where(e => e.RegisteredUsers.Any()).First();

                registeredUserId = eventDefinition.RegisteredUsers.First().RegisteredUserId;
                eventDefinitionId = eventDefinition.EventDefinitionId;
                expected = eventDefinition.RegisteredUsers.Count() - 1;
            }

            // Method to Test
            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);
            service.SetAccessToken(MyEventsToken.CreateToken(registeredUserId)); 
            IAsyncResult ar = service.RegisteredUserService.DeleteRegisteredUserFromEventAsync(registeredUserId, eventDefinitionId, (HttpStatusCode statusCode) =>
            {
                // Asserts
                using (var context = new MyEventsContext())
                {
                    var eventDefinition = context.EventDefinitions.Include("RegisteredUsers").First(e => e.EventDefinitionId == eventDefinitionId);
                    int actual = eventDefinition.RegisteredUsers.Count();
                    TestHelper.ValidateResult(expected, actual, manualResetEvent, ref exceptionResult);
                }
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }
    }
}
