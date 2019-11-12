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
    public class WebSessionTests
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
        public void GetAllSessions_Integration_CallWebAPI_GetResults_NotFail_Test()
        {
            int expected = 0;
            int eventDefinitionId = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            using (var context = new MyEventsContext())
            {
                eventDefinitionId = context.Sessions.First().EventDefinitionId;
                expected = context.Sessions.Where(q => q.EventDefinitionId == eventDefinitionId).Count();
            }

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);

            IAsyncResult ar = service.SessionService.GetAllSessionsAsync(eventDefinitionId, (IList<Client.Session> sessions) =>
            {
                ValidateAttendeeCount(expected, manualResetEvent, ref exceptionResult, sessions);
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
            
        }

        private static void ValidateAttendeeCount(int expected, ManualResetEvent manualResetEvent, ref Exception exceptionResult, IEnumerable<Client.Session> results)
        {
            try
            {
                Assert.AreEqual(expected, results.Count());

                // Validate that the session contains the number of attendees. it´s very important to get this value through API
                using (var context = new MyEventsContext())
                {
                    foreach (var result in results)
                    {
                        expected = context.SessionRegisteredUsers
                            .Count(q => q.SessionId == result.SessionId);

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
        public void GetSession_Integration_CallWebAPI_GetResult_NotFail_Test()
        {
            int sessionId = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            using (var context = new MyEventsContext())
            {
                sessionId = context.Sessions.First().SessionId;
            }

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);

            IAsyncResult ar = service.SessionService.GetSessionAsync(sessionId, (Client.Session session) =>
            {
                TestHelper.ValidateResult(sessionId, session.SessionId, manualResetEvent, ref exceptionResult);
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void PostSession_Integration_CallWebAPI_Added_NotFail_Test()
        {
            int expected = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);
            EventDefinition eventDefinition;

            // Method to Test
            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            string urlToTest = String.Format("{0}api/session/PostSession", urlPrefix);

            // Get values to test
            using (var context = new MyEventsContext())
            {
                eventDefinition = context.EventDefinitions.Include("Sessions").FirstOrDefault();
                expected = eventDefinition.Sessions.Count() + 1;
            }

            // Create object to add
            Client.Session session = new Client.Session();
            session.EventDefinitionId = eventDefinition.EventDefinitionId;
            session.Title = Guid.NewGuid().ToString();
            session.Description = Guid.NewGuid().ToString();
            session.Speaker = Guid.NewGuid().ToString();
            session.Biography = Guid.NewGuid().ToString();
            session.TwitterAccount = Guid.NewGuid().ToString();
            session.StartTime = DateTime.Now;
            session.Duration = 60;
            session.TimeZoneOffset = 2;

            var service = new Client.MyEventsClient(urlPrefix);
            service.SetAccessToken(MyEventsToken.CreateToken(eventDefinition.OrganizerId));
            IAsyncResult ar = service.SessionService.AddSessionAsync(session, (int sessionId) =>
            {
                // Asserts
                using (var context = new MyEventsContext())
                {
                    int actual = context.Sessions.Count(q => q.EventDefinitionId == eventDefinition.EventDefinitionId);
                    TestHelper.ValidateResult(expected, actual, manualResetEvent, ref exceptionResult);
                }
            });

            
            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void PutSession_Integration_CallWebAPI_Updated_NotFail_Test()
        {
            Session session;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            // Get values to test
            using (var context = new MyEventsContext())
            {
                session = context.Sessions.Include("EventDefinition").First();
            }

            // Method to Test
            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();

            var service = new Client.MyEventsClient(urlPrefix);
            service.SetAccessToken(MyEventsToken.CreateToken(session.EventDefinition.OrganizerId));
            IAsyncResult ar = service.SessionService.GetSessionAsync(session.SessionId, (Client.Session getSession) =>
            {
                Client.Session sessionToUpdate = new Client.Session();
                sessionToUpdate.SessionId = getSession.SessionId;
                sessionToUpdate.EventDefinitionId = getSession.EventDefinitionId;
                sessionToUpdate.Description = getSession.Description;
                sessionToUpdate.Speaker = getSession.Speaker;
                sessionToUpdate.Biography = getSession.Biography;
                sessionToUpdate.TwitterAccount = getSession.TwitterAccount;
                sessionToUpdate.StartTime = getSession.StartTime;
                sessionToUpdate.Duration = getSession.Duration;

                sessionToUpdate.Title = Guid.NewGuid().ToString();

                ar = service.SessionService.UpdateSessionAsync(sessionToUpdate, (HttpStatusCode statusCode) =>
                {
                    // Asserts
                    using (var context = new MyEventsContext())
                    {
                        Session actual = context.Sessions.FirstOrDefault(q => q.SessionId == session.SessionId);
                        TestHelper.ValidateResult(sessionToUpdate.Title, actual.Title, manualResetEvent, ref exceptionResult);
                    }
                });


            });

           
            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void DeleteSession_Integration_CallWebAPI_Deleted_NotFail_Test()
        {
            Session session;
            int expected = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            // Get values to test
            using (var context = new MyEventsContext())
            {
                session = context.Sessions.Include("EventDefinition").First();
                expected = context.Sessions.Count() - 1;
            }

            // Method to Test
            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);
            service.SetAccessToken(MyEventsToken.CreateToken(session.EventDefinition.OrganizerId));
            IAsyncResult ar = service.SessionService.DeleteSessionAsync(session.SessionId, (HttpStatusCode statusCode) =>
            {
                // Asserts
                using (var context = new MyEventsContext())
                {
                    Session actual = context.Sessions.FirstOrDefault(q => q.SessionId == session.SessionId);
                    TestHelper.ValidateResult(null, actual, manualResetEvent, ref exceptionResult);
                }
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void GetAllMaterials_Integration_CallWebAPI_GetResults_NotFail_Test()
        {
            int expected = 0;
            int sessionId = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            using (var context = new MyEventsContext())
            {
                sessionId = context.Materials.First().SessionId;
                expected = context.Materials.Count(q => q.SessionId == sessionId);
            }

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();

            var service = new Client.MyEventsClient(urlPrefix);

            IAsyncResult ar = service.MaterialService.GetAllMaterialsAsync(sessionId, (IList<Client.Material> materials) =>
            {
                TestHelper.ValidateResult(expected, materials.Count(), manualResetEvent, ref exceptionResult);
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);

        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void GetMaterial_Integration_CallWebAPI_GetResults_NotFail_Test()
        {
            Material material;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            using (var context = new MyEventsContext())
            {
                material = context.Materials.First();
            }

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();

            var service = new Client.MyEventsClient(urlPrefix);

            IAsyncResult ar = service.MaterialService.GetMaterialAsync(material.MaterialId, (Client.Material result) =>
            {
                try
                {
                    Assert.IsNotNull(result.Content);
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
        public void PostMaterial_Integration_CallWebAPI_Added_NotFail_Test()
        {
            int expected = 0;
            int sessionId = 0;
            int organizerId = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            using (var context = new MyEventsContext())
            {
                sessionId = context.Materials.First().SessionId;
                expected = context.Materials.Count(q => q.SessionId == sessionId) + 1;
                organizerId = context.Sessions.Include("EventDefinition")
                    .FirstOrDefault(q => q.SessionId == sessionId).EventDefinition.OrganizerId;
            }


            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();

            // Create object to add
            Client.Material material = new Client.Material();
            material.SessionId = sessionId;
            material.Name = Guid.NewGuid().ToString();
            material.ContentType = "image/jpeg";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            material.Content = encoding.GetBytes("content");

            var service = new Client.MyEventsClient(urlPrefix);
            service.SetAccessToken(MyEventsToken.CreateToken(organizerId));
            IAsyncResult ar = service.MaterialService.AddMaterialAsync(material, (int materialId) =>
            {
                // Asserts
                using (var context = new MyEventsContext())
                {
                    int actual = context.Materials.Count(q => q.SessionId == sessionId);
                    TestHelper.ValidateResult(expected, actual, manualResetEvent, ref exceptionResult);
                }
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void DeleteMaterial_Integration_CallWebAPI_Deleted_NotFail_Test()
        {
            int expected = 0;
            int sessionId = 0;
            int organizerId = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);
            Material material;

            using (var context = new MyEventsContext())
            {
                material = context.Materials.First();
                sessionId = material.SessionId;
                expected = context.Materials.Count(q => q.SessionId == sessionId) - 1;
                organizerId = context.Sessions.Include("EventDefinition")
                    .FirstOrDefault(q => q.SessionId == sessionId).EventDefinition.OrganizerId;
            }

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);
            service.SetAccessToken(MyEventsToken.CreateToken(organizerId));
            IAsyncResult arUpdate = service.MaterialService.DeleteMaterialAsync(material.MaterialId, (HttpStatusCode statusCode) =>
            {
                using (var context = new MyEventsContext())
                {
                    int actual = context.Materials.Count(q => q.SessionId == sessionId);
                    TestHelper.ValidateResult(expected, actual, manualResetEvent, ref exceptionResult);
                }
            });


            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void GetAllComments_Integration_CallWebAPI_GetResults_NotFail_Test()
        {
            int expected = 0;
            int sessionId = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            using (var context = new MyEventsContext())
            {
                sessionId = context.Comments.First().SessionId;
                expected = context.Comments.Count(q => q.SessionId == sessionId);
            }

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);

            IAsyncResult ar = service.CommentService.GetAllCommentsAsync(sessionId, (IList<Client.Comment> comments) =>
            {
                TestHelper.ValidateResult(expected, comments.Count(), manualResetEvent, ref exceptionResult);
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);

        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void PostComment_Integration_CallWebAPI_Added_NotFail_Test()
        {
            int expected = 0;
            int registeredUserId = 0;
            int sessionId = 0;
            int organizerId = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            using (var context = new MyEventsContext())
            {
                var firstComment = context.Comments.First();
                sessionId = firstComment.SessionId;
                registeredUserId = firstComment.RegisteredUserId;
                expected = context.Comments.Count(q => q.SessionId == sessionId && q.RegisteredUserId == registeredUserId) + 1;
                organizerId = context.Sessions.Include("EventDefinition")
                     .FirstOrDefault(q => q.SessionId == sessionId).EventDefinition.OrganizerId;
            }


            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            string urlToTest = String.Format("{0}api/session/PostComment", urlPrefix);


            // Create object to add
            Client.Comment comment = new Client.Comment();
            comment.SessionId = sessionId;
            comment.RegisteredUserId = registeredUserId;
            comment.Text = Guid.NewGuid().ToString();

            var service = new Client.MyEventsClient(urlPrefix);
            service.SetAccessToken(MyEventsToken.CreateToken(organizerId));
            IAsyncResult ar = service.CommentService.AddCommentAsync(comment, (int commentId) =>
            {
                // Asserts
                using (var context = new MyEventsContext())
                {
                    int actual = context.Comments.Count(q => q.SessionId == sessionId && q.RegisteredUserId == registeredUserId);
                    TestHelper.ValidateResult(expected, actual, manualResetEvent, ref exceptionResult);
                }
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void DeleteComment_Integration_CallWebAPI_Deleted_NotFail_Test()
        {
            int expected = 0;
            int sessionId = 0;
            int organizerId = 0;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            using (var context = new MyEventsContext())
            {
                sessionId = context.Comments.First().SessionId;
                expected = context.Comments.Count(q => q.SessionId == sessionId) - 1;
                organizerId = context.Sessions.Include("EventDefinition")
                    .FirstOrDefault(q => q.SessionId == sessionId).EventDefinition.OrganizerId;
            }

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);
            service.SetAccessToken(MyEventsToken.CreateToken(organizerId));
            IAsyncResult ar = service.CommentService.GetAllCommentsAsync(sessionId, (IList<Client.Comment> comments) =>
            {
                IAsyncResult arUpdate = service.CommentService.DeleteCommentAsync(comments.First().CommentId, (HttpStatusCode statusCode) =>
                {
                    using (var context = new MyEventsContext())
                    {
                        int actual = context.Comments.Count(q => q.SessionId == sessionId);
                        TestHelper.ValidateResult(expected, actual, manualResetEvent, ref exceptionResult);
                    }
                });

            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);

        }

        [TestMethod]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        [TestCategory("Integration")]
        public void PutSessionPeriod_Integration_CallWebAPI_Updated_NotFail_Test()
        {
            Session session;
            string startTime = DateTime.Now.ToString();
            int duration = 60;
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            using (var context = new MyEventsContext())
            {
                session = context.Sessions.Include("EventDefinition").First();
            }

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();

            var service = new Client.MyEventsClient(urlPrefix);
            service.SetAccessToken(MyEventsToken.CreateToken(session.EventDefinition.OrganizerId));
            IAsyncResult ar = service.SessionService.UpdateSessionPeriodAsync(session.SessionId, startTime, duration, (HttpStatusCode statusCode) =>
            {
                using (var context = new MyEventsContext())
                {
                    var actual = context.Sessions.FirstOrDefault(q => q.SessionId == session.SessionId);
                    TestHelper.ValidateResult(duration, actual.Duration, manualResetEvent, ref exceptionResult);
                }
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        } 
    }
}
