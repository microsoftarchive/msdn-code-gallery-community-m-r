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
    public class WebRoomPointTests
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
        public void GetRoomImage_CallWebAPI_NotFail_Test()
        {
            var context = new MyEventsContext();
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            int eventDefinitionId = context.EventDefinitions.First().EventDefinitionId;
            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);
            IAsyncResult asynResult = service.RoomPointService.GetRoomImageAsync(eventDefinitionId, (byte[] image) =>
            {
                manualResetEvent.Set();
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void UpdateRoomImage_CallWebAPI_NotFail_Test()
        {
            var context = new MyEventsContext();
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            var eventDefinition = context.EventDefinitions.First(q => q.MapImage == null);
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            var image = encoding.GetBytes("sample");

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);
            service.SetAccessToken(MyEventsToken.CreateToken(eventDefinition.OrganizerId));
            IAsyncResult asynResult = service.RoomPointService.UpdateRoomImageAsync(eventDefinition.EventDefinitionId, image, (HttpStatusCode code) =>
            {
                try
                {
                    using (var contextAfter = new MyEventsContext())
                    {
                        var actual = contextAfter.EventDefinitions.Where(q => q.EventDefinitionId == eventDefinition.EventDefinitionId).FirstOrDefault().MapImage;
                        Assert.IsNotNull(actual);
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
        public void GetAllRoomPoints_CallWebAPI_NotFail_Test()
        {
            var context = new MyEventsContext();
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            int eventDefinitionId = context.EventDefinitions.First().EventDefinitionId;
            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);
            IAsyncResult asynResult = service.RoomPointService.GetAllRoomPointsAsync(eventDefinitionId, (IList<Client.RoomPoint> points) =>
            {
                manualResetEvent.Set();
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void GetRoomPoints_CallWebAPI_Coverage_Test()
        {
            var context = new MyEventsContext();
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            int sessionId = context.Sessions.First().SessionId;
            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);
            IAsyncResult asynResult = service.RoomPointService.GetRoomPointsAsync(sessionId, (IList<Client.RoomPoint> points) =>
            {
                manualResetEvent.Set();
            });

            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void AddRoomPoints_CallWebAPI_NotFail_Test()
        {
            var context = new MyEventsContext();
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            var eventDefinition = context.EventDefinitions.First();
            List<Client.RoomPoint> points = new List<Client.RoomPoint>()
            {
                new Client.RoomPoint() { EventDefinitionId = eventDefinition.EventDefinitionId, PointX = 0, PointY = 0 },
                new Client.RoomPoint() { EventDefinitionId = eventDefinition.EventDefinitionId, PointX = 1, PointY = 2 },
                new Client.RoomPoint() { EventDefinitionId = eventDefinition.EventDefinitionId, PointX = 3, PointY = 4 },
            };

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);
            service.SetAccessToken(MyEventsToken.CreateToken(eventDefinition.OrganizerId));
            IAsyncResult asynResult = service.RoomPointService.AddRoomPointsAsync(points, (HttpStatusCode code) =>
            {
                using (var contextAfter = new MyEventsContext())
                {
                    var actual = contextAfter.RoomPoints.Where(q => q.EventDefinitionId == eventDefinition.EventDefinitionId).Count();
                    TestHelper.ValidateResult(points.Count(), actual, manualResetEvent, ref exceptionResult);
                }
            });


            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [AspNetDevelopmentServer("webapiserver", "..\\..\\..\\MyEvents.Api")]
        public void DeleteRoomPoints_CallWebAPI_NotFail_Test()
        {
            var context = new MyEventsContext();
            var manualResetEvent = new ManualResetEvent(false);
            var exceptionResult = default(Exception);

            var eventDefinition = context.EventDefinitions.First();
            int roomNumber = 20;
            context.RoomPoints.Add(new RoomPoint() { EventDefinitionId = eventDefinition.EventDefinitionId, PointX = 0, PointY = 0, RoomNumber = roomNumber });
            context.RoomPoints.Add(new RoomPoint() { EventDefinitionId = eventDefinition.EventDefinitionId, PointX = 0, PointY = 0, RoomNumber = roomNumber });
            context.RoomPoints.Add(new RoomPoint() { EventDefinitionId = eventDefinition.EventDefinitionId, PointX = 0, PointY = 0, RoomNumber = roomNumber });

            string urlPrefix = testContextInstance.Properties[TestContext.AspNetDevelopmentServerPrefix + "webapiserver"].ToString();
            var service = new Client.MyEventsClient(urlPrefix);
            service.SetAccessToken(MyEventsToken.CreateToken(eventDefinition.OrganizerId));
            IAsyncResult asynResult = service.RoomPointService.DeleteRoomPointsAsync(eventDefinition.EventDefinitionId, roomNumber, (HttpStatusCode code) =>
            {
                using (var contextAfter = new MyEventsContext())
                {
                    var actual = contextAfter.RoomPoints.Where(q => q.EventDefinitionId == eventDefinition.EventDefinitionId && q.RoomNumber == roomNumber).Count();
                    TestHelper.ValidateResult(0, actual, manualResetEvent, ref exceptionResult);
                }
            });


            TestHelper.WaitAll(manualResetEvent, ref exceptionResult);
        }
    }
}
