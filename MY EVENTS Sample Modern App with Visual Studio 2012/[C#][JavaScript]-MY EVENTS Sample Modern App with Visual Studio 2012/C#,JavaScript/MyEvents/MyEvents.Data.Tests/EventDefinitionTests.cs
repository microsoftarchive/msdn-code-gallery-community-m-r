using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using MyEvents.Model;

namespace MyEvents.Data.Test
{
    [TestClass]
    public class EventDefinitionTests
    {
        [TestMethod]
        public void GetEventDefinitionCount_Call_NotFail_Test()
        {
            var context = new MyEventsContext();
            int expected = context.EventDefinitions.Count();
          

            IEventDefinitionRepository target = new EventDefinitionRepository();
            int actual = target.GetCount();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetEventDefinitions_CallGettingAll_NotFailGetAll_Test()
        {
            var context = new MyEventsContext();
            int expected = context.EventDefinitions.Where(q => q.Date >= DateTime.UtcNow).Count();
            int pageIndex = 0;

            IEventDefinitionRepository target = new EventDefinitionRepository();
            int actual = target.GetAll(expected, pageIndex).Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetEventDefinitions_CallGettingFirstPage_NotFailGetFistPage_Test()
        {
            var context = new MyEventsContext();
            int expected = 0;
            if (context.EventDefinitions.Where(q => q.Date >= DateTime.UtcNow).Count() > 0)
                expected = 1;

            int pageIndex = 0;

            IEventDefinitionRepository target = new EventDefinitionRepository();
            int actual = target.GetAll(expected, pageIndex).Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetLastEventDefinitions_CallGettingAll_NotFailGetAll_Test()
        {
            var context = new MyEventsContext();

            var dateTimeToCompare = DateTime.UtcNow.AddMonths(1);
            int number = context.EventDefinitions.Where(q => q.Date >= DateTime.UtcNow && q.Date <= dateTimeToCompare).Count();

            IEventDefinitionRepository target = new EventDefinitionRepository();
            int actual = target.GetLast(number).Count();

            Assert.AreEqual(number, actual);
        }

        [TestMethod]
        public void GetLastEventDefinitions_CallGettingZero_NotFailGetAll_Test()
        {
            var context = new MyEventsContext();
            int expected = 0;

            IEventDefinitionRepository target = new EventDefinitionRepository();
            int actual = target.GetLast(expected).Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetEventDefinitionCount_CallWithEmptyFilter_GetAll_Test()
        {
            var context = new MyEventsContext();
            int organizerId = context.RegisteredUsers.FirstOrDefault().RegisteredUserId;
            int expected = context.EventDefinitions.Where(q => q.OrganizerId == organizerId).Count();

            IEventDefinitionRepository target = new EventDefinitionRepository();
            int actual = target.GetCountByOrganizerId(organizerId, string.Empty);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetEventDefinitionCount_CallWithFilter_GetOnlyOne_Test()
        {
            var context = new MyEventsContext();
            int organizerId = context.EventDefinitions.FirstOrDefault().OrganizerId;
            string filter = context.EventDefinitions.Where(q => q.OrganizerId == organizerId).First().Name;

            IEventDefinitionRepository target = new EventDefinitionRepository();
            int actual = target.GetCountByOrganizerId(organizerId, filter);

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void GetEventDefinition_CallWithEmptyFilter_GetAll_Test()
        {
            var context = new MyEventsContext();
            int organizerId = context.RegisteredUsers.FirstOrDefault().RegisteredUserId;
            int expected = context.EventDefinitions.Where(q => q.OrganizerId == organizerId).Count();
            int pageIndex = 0;

            IEventDefinitionRepository target = new EventDefinitionRepository();
            int actual = target.GetByOrganizerId(organizerId, string.Empty, expected, pageIndex, true).Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetEventDefinition_CallWithEmptyFilterAndPaged_GetFirstPageWithOneElement_Test()
        {
            var context = new MyEventsContext();
            int organizerId = context.RegisteredUsers.FirstOrDefault().RegisteredUserId;
            int expected = 1;
            int pageIndex = 0;

            IEventDefinitionRepository target = new EventDefinitionRepository();
            int actual = target.GetByOrganizerId(organizerId, string.Empty, expected, pageIndex, true).Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetEventDefinition_CallWithFilter_GetOnlyOne_Test()
        {
            var context = new MyEventsContext();
            int organizerId = context.EventDefinitions.FirstOrDefault().OrganizerId;
            string filter = context.EventDefinitions.Where(q => q.OrganizerId == organizerId).First().Name;
            int pageSize = context.EventDefinitions.Where(q => q.OrganizerId == organizerId).Count();
            int pageIndex = 0;

            IEventDefinitionRepository target = new EventDefinitionRepository();
            int actual = target.GetByOrganizerId(organizerId, filter, pageSize, pageIndex, true).Count();

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            var context = new MyEventsContext();
            int eventDefinitionId = context.EventDefinitions.First().EventDefinitionId;

            IEventDefinitionRepository target = new EventDefinitionRepository();
            EventDefinition actual = target.GetById(eventDefinitionId);

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void GetEventLogoTest()
        {
            var context = new MyEventsContext();
            int eventDefinitionId = context.EventDefinitions.First(e => e.Logo != null).EventDefinitionId;

            IEventDefinitionRepository target = new EventDefinitionRepository();
            byte[] logo = target.GetEventLogo(eventDefinitionId);

            Assert.IsNotNull(logo);
        }

        [TestMethod]
        public void AddEventDefinition_Added_NotFail_Test()
        {
            var context = new MyEventsContext();
            int organizerId = context.RegisteredUsers.FirstOrDefault().RegisteredUserId;

            int expected = context.EventDefinitions.Count() + 1;

            IEventDefinitionRepository target = new EventDefinitionRepository();

            var eventDef = new EventDefinition();
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
            eventDef.TimeZoneOffset = 2;
            eventDef.EndTime = System.DateTime.Now.AddMinutes(1);
            eventDef.Likes = 0;
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            eventDef.Logo = encoding.GetBytes("sample");

            target.Add(eventDef);

            int actual = context.EventDefinitions.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateEventDefinition_Updated_NotFail_Test()
        {
            var context = new MyEventsContext();
            var eventToUpdate = context.EventDefinitions.FirstOrDefault();

            IEventDefinitionRepository target = new EventDefinitionRepository();

            eventToUpdate.Name = Guid.NewGuid().ToString();
            target.Update(eventToUpdate);

            var eventUpdated = context.EventDefinitions.FirstOrDefault(q => q.EventDefinitionId == eventToUpdate.EventDefinitionId);

            Assert.AreEqual(eventToUpdate.Name, eventUpdated.Name);
        }

        [TestMethod]
        public void DeleteEventDefinition_Deleted_NotFail_Test()
        {
            var context = new MyEventsContext();
            int organizerId = context.RegisteredUsers.FirstOrDefault().RegisteredUserId;

            var eventDef = new EventDefinition();
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
            eventDef.TimeZoneOffset = 2;
            eventDef.EndTime = System.DateTime.Now.AddMinutes(1);
            eventDef.Likes = 0;
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            eventDef.Logo = encoding.GetBytes("sample");

            IEventDefinitionRepository target = new EventDefinitionRepository();
            target.Add(eventDef);

            var eventDefinition = context.EventDefinitions.FirstOrDefault();
            int expected = context.EventDefinitions.Count() - 1;

            target.Delete(eventDef.EventDefinitionId);

            int actual = context.EventDefinitions.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteEventDefinition_NoExist_NotFail_Test()
        {
            var context = new MyEventsContext();
            var eventDefinition = context.EventDefinitions.FirstOrDefault();
            int expected = context.EventDefinitions.Count();

            IEventDefinitionRepository target = new EventDefinitionRepository();
            target.Delete(0);

            int actual = context.EventDefinitions.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetRoomImage_Coverage_NotFail_Test()
        {
            var context = new MyEventsContext();

            int eventDefinitionId = context.EventDefinitions.First().EventDefinitionId;

            IEventDefinitionRepository target = new EventDefinitionRepository();
            target.GetRoomImage(eventDefinitionId);
        }

        [TestMethod]
        public void UpdateRoomImage_Updated_Test()
        {
            var context = new MyEventsContext();

            int eventDefinitionId = context.EventDefinitions.First(e => e.MapImage == null).EventDefinitionId;

            IEventDefinitionRepository target = new EventDefinitionRepository();
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            target.UpdateRoomImage(new EventDefinition() { EventDefinitionId = eventDefinitionId, MapImage = encoding.GetBytes("sample") });

            var result = target.GetRoomImage(eventDefinitionId);
            Assert.IsNotNull(result);
        }
       
        [TestMethod]
        public void AddRoomPoints_Added_Test()
        {
            var context = new MyEventsContext();

            int eventDefinitionId = context.EventDefinitions.Include("RoomPoints")
                .Where(q => !q.RoomPoints.Any())
                .First().EventDefinitionId;

            List<RoomPoint> points = new List<RoomPoint>()
            {
                new RoomPoint() { EventDefinitionId = eventDefinitionId, PointX = 0, PointY = 0 },
                new RoomPoint() { EventDefinitionId = eventDefinitionId, PointX = 1, PointY = 2 },
                new RoomPoint() { EventDefinitionId = eventDefinitionId, PointX = 3, PointY = 4 },
            };

            IEventDefinitionRepository target = new EventDefinitionRepository();
            target.AddRoomPoints(points);

            int actual = target.GetAllRoomPoints(eventDefinitionId).Count();

            Assert.AreEqual(points.Count(), actual);
        }

        [TestMethod]
        public void DeleteRoomPoints_Added_Test()
        {
            var context = new MyEventsContext();

            int eventDefinitionId = context.EventDefinitions.Include("RoomPoints")
                .Where(q => !q.RoomPoints.Any())
                .First().EventDefinitionId;

            List<RoomPoint> points = new List<RoomPoint>()
            {
                new RoomPoint() { EventDefinitionId = eventDefinitionId, PointX = 0, PointY = 0, RoomNumber = 20 },
                new RoomPoint() { EventDefinitionId = eventDefinitionId, PointX = 1, PointY = 2, RoomNumber = 20 },
                new RoomPoint() { EventDefinitionId = eventDefinitionId, PointX = 3, PointY = 4, RoomNumber = 20 },
            };

            IEventDefinitionRepository target = new EventDefinitionRepository();
            target.AddRoomPoints(points);
            int actual = target.GetAllRoomPoints(eventDefinitionId).Count();
            Assert.AreEqual(points.Count(), actual);

            target.DeleteRoomPoints(eventDefinitionId, 20);
            actual = target.GetAllRoomPoints(eventDefinitionId).Count();
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void GetRoomPoints_Coverage_Test()
        {
            var context = new MyEventsContext();

            IEventDefinitionRepository target = new EventDefinitionRepository();
            target.GetRoomPoints(context.Sessions.First().SessionId);
        }

        [TestMethod]
        public void GetTopSpeakers_Coverage_Called_Test()
        {
           var context = new MyEventsContext();
           IEventDefinitionRepository target = new EventDefinitionRepository();
           var result = target.GetTopSpeakers(1);
           Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTopTags_Coverage__Called_Test()
        {
            var context = new MyEventsContext();
            IEventDefinitionRepository target = new EventDefinitionRepository();
            var result = target.GetTopTags(1);
            Assert.IsNotNull(result);
        }
    }
}
