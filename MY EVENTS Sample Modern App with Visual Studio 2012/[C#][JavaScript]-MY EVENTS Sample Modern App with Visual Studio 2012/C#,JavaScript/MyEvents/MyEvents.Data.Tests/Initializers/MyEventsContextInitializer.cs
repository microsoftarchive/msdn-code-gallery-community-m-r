using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvents.Model;

namespace MyEvents.Data.Test.Initializers
{
    /// <summary>
    /// The default initializer for testing . Yoy can
    /// learn more about initializers in 
    /// http://msdn.microsoft.com/en-us/library/gg696323(v=VS.103).aspx
    /// </summary>
    public class MyEventsContextInitializer
        : DropCreateDatabaseAlways<MyEventsContext>
    {
        protected override void Seed(MyEventsContext context)
        {
            int eventDefinitions = 5;
            int sessionsPerEvent = 2;
            int RegisteredUserPerEvent = 2;

            List<int> eventIds = AddEvent(context, eventDefinitions);
            List<int> sessionIds = AddSession(context, eventIds, sessionsPerEvent);
            List<RegisteredUser> registeredUsers = AddRegisteredUser(context, eventIds, RegisteredUserPerEvent);
            AddRegisteredUserToSessions(context, registeredUsers, sessionIds);
            AddMaterial(context, sessionIds, 10);
            AddComment(context, sessionIds, registeredUsers, 10);
        }

        private static int AddOrganizer(MyEventsContext context)
        {
            var organizer = new RegisteredUser();
            organizer.Name = Guid.NewGuid().ToString();
            organizer.FacebookId = Guid.NewGuid().ToString();
            organizer.Email = Guid.NewGuid().ToString();
            organizer.City = Guid.NewGuid().ToString();
            organizer.Bio = Guid.NewGuid().ToString();
            context.RegisteredUsers.Add(organizer);
            context.SaveChanges();
            return organizer.RegisteredUserId;
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
            eventDef.Tags = "Windows,Azure,WP7,Visual Studio,HTML,ASP.NET";
            eventDef.TwitterAccount = Guid.NewGuid().ToString();
            eventDef.RoomNumber = 1;
            eventDef.Date = System.DateTime.Now;
            eventDef.StartTime = System.DateTime.Now;
            eventDef.EndTime = System.DateTime.Now.AddMinutes(1);
            eventDef.TimeZoneOffset = 2;
            eventDef.Likes = 0;
            eventDef.Latitude = 1.0f;
            eventDef.Longitude = 1.0f;
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
            session.TimeZoneOffset = 2;
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
            material.Name = Guid.NewGuid().ToString();
            material.SessionId = sessionId;
            material.ContentType = "image/jpeg";
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            material.Content = encoding.GetBytes("content");

            context.Materials.Add(material);
            context.SaveChanges();
        }

        private static void AddComment(MyEventsContext context, List<int> sessionIds, List<RegisteredUser> registeredUsers, int count)
        {
            foreach (var sessionId in sessionIds)
            {
                foreach (var registeredUser in registeredUsers)
                {
                    for (int i = 0; i < count; i++)
                    {
                        AddComment(context, sessionId, registeredUser);
                    }
                }
            }
        }

        private static void AddComment(MyEventsContext context, int sessionId, RegisteredUser registeredUser)
        {
            var comment = new Comment();
            comment.SessionId = sessionId;
            comment.RegisteredUserId = registeredUser.RegisteredUserId;
            comment.Text = "comment sample";
            comment.AddedDateTime = DateTime.UtcNow;
            context.Comments.Add(comment);
            context.SaveChanges();
        }

        private static List<RegisteredUser> AddRegisteredUser(MyEventsContext context, List<int> eventDefinitionIds, int count)
        {
            List<RegisteredUser> registeredUsers = new List<RegisteredUser>();

            foreach (var eventDefinitionId in eventDefinitionIds)
            {
                for (int i = 0; i < count; i++)
                {
                    registeredUsers.Add(AddRegisteredUser(context, eventDefinitionId));
                }
            }

            return registeredUsers;
        }

        private static RegisteredUser AddRegisteredUser(MyEventsContext context, int eventDefinitionId)
        {
            var RegisteredUser = new RegisteredUser();
            RegisteredUser.Name = Guid.NewGuid().ToString();
            RegisteredUser.FacebookId = Guid.NewGuid().ToString();
            RegisteredUser.Email = Guid.NewGuid().ToString();
            RegisteredUser.City = Guid.NewGuid().ToString();
            RegisteredUser.Bio = Guid.NewGuid().ToString();
            context.RegisteredUsers.Add(RegisteredUser);
            context.SaveChanges();

            RegisteredUser.AttendeeEventDefinitions = new List<EventDefinition>();
            RegisteredUser.AttendeeEventDefinitions.Add(context.EventDefinitions.FirstOrDefault(q => q.EventDefinitionId == eventDefinitionId));

            return RegisteredUser;
        }

        private static void AddRegisteredUserToSessions(MyEventsContext context, List<RegisteredUser> registeredUsers, List<int> sessionIds)
        {
            foreach (var registeredUser in registeredUsers)
            {
                var RegisteredUser = context.RegisteredUsers.FirstOrDefault(q => q.RegisteredUserId == registeredUser.RegisteredUserId);
                RegisteredUser.SessionRegisteredUsers = new List<SessionRegisteredUser>();

                foreach (var sessionId in sessionIds)
                {
                    RegisteredUser.SessionRegisteredUsers.Add(new SessionRegisteredUser()
                    { 
                        RegisteredUserId = registeredUser.RegisteredUserId, 
                        FacebookId = registeredUser.FacebookId,
                        SessionId = sessionId, Score = 0, Rated = true });
                }

                context.SaveChanges();
            }
        }

        private static void AddRegisteredUserToSessions(MyEventsContext context, RegisteredUser registeredUser, List<int> sessionIds)
        {
            var RegisteredUser = context.RegisteredUsers.FirstOrDefault(q => q.RegisteredUserId == registeredUser.RegisteredUserId);
            RegisteredUser.SessionRegisteredUsers = new List<SessionRegisteredUser>();
            foreach (var sessionId in sessionIds)
            {
                RegisteredUser.SessionRegisteredUsers.Add(new SessionRegisteredUser() { 
                    RegisteredUserId = registeredUser.RegisteredUserId,
                    FacebookId = registeredUser.FacebookId,
                    SessionRegisteredUserId = sessionId });
            }
            context.SaveChanges();
        }

    }
}
