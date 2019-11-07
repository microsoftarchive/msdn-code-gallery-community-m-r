using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyEvents.Domain.Entities;

namespace MyEvents.Data.Test
{
    /// <summary>
    /// Tes class to initialize 
    /// </summary>
    [TestClass]
    public static class TestDataInitialize
    {
        /// <summary>
        /// This method is called before launching the unit tests that are in this assembly
        /// </summary>
        /// <param name="context">Test context</param>
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            int eventDefinitions = 5;
            int sessionsPerEvent = 2;
            int AttendeePerEvent = 2;

            using (var myEventsContext = new MyEventsContext())
            {
                DeleteAllAttendees(myEventsContext);
                DeleteAllOrganizers(myEventsContext);

                List<int> eventIds = AddEvent(myEventsContext, eventDefinitions);
                List<int> sessionIds = AddSession(myEventsContext, eventIds, sessionsPerEvent);
                List<int> attendeeIds = AddAttendee(myEventsContext, eventIds, AttendeePerEvent);
                AddAttendeeToSessions(myEventsContext, attendeeIds, sessionIds);
                AddMaterial(myEventsContext, sessionIds, 10);
                AddComment(myEventsContext, sessionIds, attendeeIds, 10);
            }
        }

        /// <summary>
        /// This method is called after launching the unit tests that are in this assembly
        /// </summary>
        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            using (var context = new MyEventsContext())
            {
                DeleteAllOrganizers(context);

                Assert.IsTrue(context.Organizers.Count() == 0);
                Assert.IsTrue(context.Attendees.Count() == 0);
                Assert.IsTrue(context.EventDefinitions.Count() == 0);
                Assert.IsTrue(context.Sessions.Count() == 0);
                Assert.IsTrue(context.Materials.Count() == 0);
                Assert.IsTrue(context.Comments.Count() == 0);
            }
        }

        private static int AddOrganizer(MyEventsContext context)
        {
            var organizer = new Organizer();
            organizer.Name = Guid.NewGuid().ToString();
            context.Organizers.Add(organizer);
            context.SaveChanges();
            return organizer.OrganizerId;
        }

        private static List<int> AddEvent(MyEventsContext context, int count)
        {
            List<int> eventDefinitionIds = new List<int>();

            for (int i = 0; i < count; i++)
            {
                eventDefinitionIds.Add(AddEvent(context));
            }

            return eventDefinitionIds;
        }

        private static int AddEvent(MyEventsContext context)
        {
            var eventDef = new EventDefinition();
            eventDef.OrganizerId = AddOrganizer(context);
            eventDef.Name = Guid.NewGuid().ToString();
            eventDef.Description = Guid.NewGuid().ToString();
            eventDef.Address = Guid.NewGuid().ToString();
            eventDef.City = Guid.NewGuid().ToString();
            eventDef.Tags = Guid.NewGuid().ToString();
            eventDef.TwitterAccount = Guid.NewGuid().ToString();
            eventDef.Rooms = 1;
            eventDef.Date = System.DateTime.Now;
            eventDef.StartTime = System.DateTime.Now;
            eventDef.EndTime = System.DateTime.Now;
            eventDef.Latitude = 0;
            eventDef.Longitude = 0;
            eventDef.Likes = 0;
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            eventDef.Logo = encoding.GetBytes("sample");

            context.EventDefinitions.Add(eventDef);
            context.SaveChanges();
            return eventDef.EventDefinitionId;
        }

        private static List<int> AddSession(MyEventsContext context, List<int> eventDefinitionIds, int count)
        {
            List<int> sessionIds = new List<int>();

            foreach (var eventDefinitionId in eventDefinitionIds)
            {
                for (int i = 0; i < count; i++)
                {
                    sessionIds.Add(AddSession(context, eventDefinitionId));
                }
            }

            return sessionIds;
        }

        private static int AddSession(MyEventsContext context, int eventDefinitionId)
        {
            var session = new Session();
            session.Title = Guid.NewGuid().ToString();
            session.Description = Guid.NewGuid().ToString();
            session.Speaker = Guid.NewGuid().ToString();
            session.Biography = Guid.NewGuid().ToString();
            session.TwitterAccount = Guid.NewGuid().ToString();
            session.EventDefinitionId = eventDefinitionId;
            session.StartTime = DateTime.Now;
            session.Duration = 60;

            context.Sessions.Add(session);
            context.SaveChanges();
            return session.SessionId;
        }

        private static void AddMaterial(MyEventsContext context, List<int> sessionIds, int count)
        {
            foreach (var sessionId in sessionIds)
            {
                for (int i = 0; i < count; i++)
                {
                    AddMaterial(context, sessionId);
                }
            }
        }

        private static void AddMaterial(MyEventsContext context, int sessionId)
        {
            var material = new Material();
            material.SessionId = sessionId;
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            material.Content = encoding.GetBytes("content");

            context.Materials.Add(material);
            context.SaveChanges();
        }

        private static void AddComment(MyEventsContext context, List<int> sessionIds, List<int> attendeeIds, int count)
        {
            foreach (var sessionId in sessionIds)
            {
                foreach (var attendeeId in attendeeIds)
                {
                    for (int i = 0; i < count; i++)
                    {
                        AddComment(context, sessionId, attendeeId);
                    }
                }
            }
        }

        private static void AddComment(MyEventsContext context, int sessionId, int attendeeId)
        {
            var comment = new Comment();
            comment.SessionId = sessionId;
            comment.AttendeeId = attendeeId;
            comment.Text = "comment sample";
            context.Comments.Add(comment);
            context.SaveChanges();
        }

        private static List<int> AddAttendee(MyEventsContext context, List<int> eventDefinitionIds, int count)
        {
            List<int> attendeeIds = new List<int>();

            foreach (var eventDefinitionId in eventDefinitionIds)
            {
                for (int i = 0; i < count; i++)
                {
                    attendeeIds.Add(AddAttendee(context, eventDefinitionId));
                }
            }

            return attendeeIds;
        }

        private static int AddAttendee(MyEventsContext context, int eventDefinitionId)
        {
            var attendee = new Attendee();
            attendee.EventDefinitionId = eventDefinitionId;
            attendee.Name = Guid.NewGuid().ToString();
            context.Attendees.Add(attendee);
            context.SaveChanges();
            return attendee.AttendeeId;
        }

        private static void AddAttendeeToSessions(MyEventsContext context, List<int> attendeeIds, List<int> sessionIds)
        {
            foreach (var attendeeId in attendeeIds)
            {
                var attendee = context.Attendees.FirstOrDefault(q => q.AttendeeId == attendeeId);
                attendee.SessionAttendees = new List<SessionAttendee>();

                foreach (var sessionId in sessionIds)
                {
                    attendee.SessionAttendees.Add(new SessionAttendee() { AttendeeId = attendeeId, SessionId = sessionId, Score = 0 });
                }

                context.SaveChanges();
            }
        }

        private static void AddAttendeeToSessions(MyEventsContext context, int attendeeId, List<int> sessionIds)
        {
            var attendee = context.Attendees.FirstOrDefault(q => q.AttendeeId == attendeeId);
            attendee.SessionAttendees = new List<SessionAttendee>();
            foreach (var sessionId in sessionIds)
            {
                attendee.SessionAttendees.Add(new SessionAttendee() { AttendeeId = attendeeId, SessionAttendeeId = sessionId });
            }
            context.SaveChanges();
        }

        private static void DeleteAllAttendees(MyEventsContext context)
        {
            var attendees = context.Attendees.ToList();
            foreach (var attendee in attendees)
            {
                context.Attendees.Remove(attendee);
            }
            context.SaveChanges();
        }

        private static void DeleteAllOrganizers(MyEventsContext context)
        {
            var organizers = context.Organizers.ToList();
            foreach (var organizer in organizers)
            {
                context.Organizers.Remove(organizer);
            }
            context.SaveChanges();
        }

    }
}
