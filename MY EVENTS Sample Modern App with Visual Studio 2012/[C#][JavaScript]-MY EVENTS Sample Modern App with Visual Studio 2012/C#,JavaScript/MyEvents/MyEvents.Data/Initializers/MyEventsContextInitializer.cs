using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MyEvents.Data.Initializers.Events;
using MyEvents.Model;

namespace MyEvents.Data.Initializers
{
    /// <summary>
    /// The default initializer for testing . Yoy can
    /// learn more about initializers in 
    /// http://msdn.microsoft.com/en-us/library/gg696323(v=VS.103).aspx
    /// </summary>
    public class MyEventsContextInitializer
        : DropCreateDatabaseIfModelChanges<MyEventsContext>  //** DropCreateDatabaseAlways / DropCreateDatabaseIfModelChanges / CreateDatabaseIfNotExists **
    {
        private static readonly Random Randomize = new Random();
        private static RegisteredUser _defaultUser;
        private static List<RegisteredUser> _registeredUsers = new List<RegisteredUser>();
        private static List<int> _eventIds;
        private static List<int> _sessionIds;
        private static List<int> _organizerIds;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(MyEventsContext context)
        {
            InitializeDefaultUser(context);

            var instances = from t in Assembly.GetExecutingAssembly().GetTypes()
                            where t.GetInterfaces().Contains(typeof(IEvent))
                                     && t.GetConstructor(Type.EmptyTypes) != null
                            select Activator.CreateInstance(t) as IEvent;

            foreach (var instance in instances)
            {
                instance.Create(context);
            }

            //(CDLTLL) Event' & Sessions' & Organizers' IDs
            _eventIds = context.EventDefinitions.Select(e => e.EventDefinitionId).ToList();
            _sessionIds = context.Sessions.Select(s => s.SessionId).ToList();
            _organizerIds = context.RegisteredUsers.Select(o => o.RegisteredUserId).ToList();

            //(CDLTLL) Global initializations
            InitializeAttendees(context, _eventIds);
            InitializeMaterials(context, _sessionIds);
            InitializeComments(context, _sessionIds, _registeredUsers, 1);
            AddRegisteredUserToSessions(context, _registeredUsers, _sessionIds);
        }

        private static void InitializeAttendees(MyEventsContext context, List<int> eventDefinitionIds)
        {
            foreach (var eventDefinitionId in eventDefinitionIds)
            {
                var eventDefinition = context.EventDefinitions.FirstOrDefault(q => q.EventDefinitionId == eventDefinitionId);
                int number = GetNumber(0, 50);
                for (int i = 0; i < number; i++)
                {
                    AddAttendee(context, eventDefinition);
                }
            }
            context.SaveChanges();
            _registeredUsers = context.RegisteredUsers.Where(o => o.RegisteredUserId > 7).Take(10).ToList();
            _registeredUsers.Insert(0, _defaultUser);
        }

        private static void InitializeDefaultUser(MyEventsContext context)
        {
            var attendee = new RegisteredUser();
            attendee.Name = CommonInitializer.FakeUserName;
            attendee.FacebookId = CommonInitializer.FakeUserFacebookId;
            attendee.Email = "attendee@domain.com";
            attendee.City = GetCity()[0];
            attendee.Bio = "Attendee";
            context.RegisteredUsers.Add(attendee);
            context.SaveChanges();
            _defaultUser = attendee;
        }

        private static void AddAttendee(MyEventsContext context, EventDefinition eventDefinition)
        {
            var attendee = new RegisteredUser();
            var facebookData = GetFacebookData();
            attendee.FacebookId = facebookData.FacebookId;
            attendee.Name = facebookData.Name;
            attendee.Email = "attendee@domain.com";
            attendee.City = GetCity()[0];
            attendee.Bio = facebookData.Bio;
            attendee.AttendeeEventDefinitions = new List<EventDefinition>();
            attendee.AttendeeEventDefinitions.Add(eventDefinition);
            context.RegisteredUsers.Add(attendee);
        }

        private static FacebookData GetFacebookData()
        {
           var data = new List<FacebookData>() { 
                new FacebookData { FacebookId = "100001182502601", Name = "Brian Hill", Bio = ""}, 
                new FacebookData { FacebookId = "100004258750017", Name = "Nicolas Brandon", Bio = ""}, 
                new FacebookData { FacebookId = "104572553029531", Name = "Jason Zander", Bio = ""}, 
                new FacebookData { FacebookId = "100004415730506", Name = "Josh Bailey", Bio = ""}, 
                new FacebookData { FacebookId = "10150507160869628", Name = "Eric Lang", Bio = ""}, 
                new FacebookData { FacebookId = "1010246007", Name = "Humberto Acevedo", Bio = ""}, 
                new FacebookData { FacebookId = "1007691855", Name = "Tom Perham", Bio = ""}, 
                new FacebookData { FacebookId = "1453295348", Name = "Jeff Phillips", Bio = ""}, 
                new FacebookData { FacebookId = "814188598", Name = "Carole Poland", Bio = ""}, 
                new FacebookData { FacebookId = "100004118912757", Name = "Cristina Gonzalez", Bio = ""}, 
                new FacebookData { FacebookId = "1026120498", Name = "Adam Barr", Bio = ""}, 
                new FacebookData { FacebookId = "1350603074", Name = "David Jones", Bio = ""}, 
                //{"1", "David Hamilton"}, 
                //{"2", "Christa Geller"}, 
                //{"3", "Garth Fort"}, 
                //{"4", "Andrew Davis", }, 
                //{"5", "David Rodriguez"}, 
                };
            int index = Randomize.Next(0, data.Count);

            return data.ElementAt(index);
        }

        private static string GetFacebookId()
        {
            List<string> ids = new List<string>() { "100001182502601", "100004258750017", "104572553029531", "100004415730506", "10150507160869628", "1010246007", "1007691855", "1453295348", "814188598", "1026120498", "1350603074" };
            int index = Randomize.Next(0, ids.Count);
            return ids[index];
        }

        private static string GetFacebookUsername()
        {
            List<string> ids = new List<string>() { "cesardemos", "nicolefake", "jasonfake", "davidcsa@outlook.com", "dros", "alfredofl", "albabarrerajimenez", "ibon.landa", "josueyeray" };
            int index = Randomize.Next(0, ids.Count);
            return ids[index];
        }

        private static int GetNumber(int min = 0, int max = 300)
        {
            max++;
            return Randomize.Next(min, max);
        }

        private static List<string> GetCity()
        {
            List<List<string>> cities = new List<List<string>>() { 
                new List<string>() { "Madrid Spain", "Club Deportivo numero 1"}, 
                new List<string>() { "Irving Texas", "7000 N State Hwy 161"}, 
                new List<string>() { "Raleigh North Carolina", "1101 Gorman St"}, 
                new List<string>() { "Tampa Florida", "5426 Bay Center Dr Suite 700"}, 
                new List<string>() { "Los Angeles California", "3333 Bristol St"}, 
                new List<string>() { "Irvine California", "3 Park Plaza Suite, 1600"}, 
            };
            int index = Randomize.Next(0, cities.Count);
            return cities[index];
        }

        private static void InitializeMaterials(MyEventsContext context, List<int> sessionIds)
        {
            foreach (var sessionId in sessionIds)
            {
                int number = GetNumber(1, 5);
                for (int i = 0; i < number; i++)
                {
                    AddMaterial(context, sessionId);
                }
            }
            context.SaveChanges();
        }

        private static void AddMaterial(MyEventsContext context, int sessionId)
        {
            var material = new Material();
            material.SessionId = sessionId;
            material.Name = string.Format("file {0}.jpeg", GetNumber(1, 10));
            material.ContentType = "image/jpeg";
            material.Content = GetDefaultImage();

            context.Materials.Add(material);
        }

        private static void InitializeComments(MyEventsContext context, List<int> sessionIds, List<RegisteredUser> registeredUsers, int count)
        {
            foreach (var sessionId in sessionIds)
            {
                foreach (var registeredUser in registeredUsers)
                {
                    for (int i = 0; i < count; i++)
                    {
                        AddComment(context, sessionId, registeredUser, i);
                    }
                }
            }

            context.SaveChanges();
        }

        private static void AddComment(MyEventsContext context, int sessionId, RegisteredUser registeredUser, int minuteToAdd)
        {
            var comment = new Comment();
            comment.SessionId = sessionId;
            comment.RegisteredUserId = registeredUser.RegisteredUserId;
            comment.Text = GetComment();
            comment.AddedDateTime = DateTime.UtcNow.AddMinutes(minuteToAdd);
            context.Comments.Add(comment);
        }

        private static string GetComment()
        {
            List<string> comments = new List<string>() { 
                "Great session! Great speaker! I love it!", 
                "Nice session, I'd only wish a longer session", 
                "Awesome! This kind of sessions are very useful for me. I would like to have more!", 
                "Good content!, these demos were super cool!."
            };
            int index = Randomize.Next(0, comments.Count);
            return comments[index];
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
                        SessionId = sessionId,
                        Score = GetNumber(1, 5),
                        Rated = GetRated()
                    });

                    /*
                    // If Session is even and RegisteredUser is even
                    if ((sessionId % 2 == 0) || (registeredUser.RegisteredUserId % 2 == 0))
                    {                        
                            RegisteredUser.SessionRegisteredUsers.Add(new SessionRegisteredUser()
                            {
                                RegisteredUserId = registeredUser.RegisteredUserId,
                                FacebookId = registeredUser.FacebookId,
                                SessionId = sessionId,
                                Score = GetNumber(1, 5),
                                Rated = GetRated()                           
                            });                        
                    }
                    
                    // If Session is odd and RegisteredUser is odd
                    if ((!(sessionId % 2 == 0)) || (!(registeredUser.RegisteredUserId % 2 == 0)))
                    {                        
                            RegisteredUser.SessionRegisteredUsers.Add(new SessionRegisteredUser()
                            {
                                RegisteredUserId = registeredUser.RegisteredUserId,
                                FacebookId = registeredUser.FacebookId,
                                SessionId = sessionId,
                                Score = GetNumber(1, 5),
                                Rated = GetRated()
                            });                                               
                    } 
                    */

                }

                context.SaveChanges();
            }
        }

        private static bool GetRated()
        {
            return new Random().Next(100) % 2 == 0;
        }

        private static byte[] GetDefaultImage()
        {
            string path = new Uri(Assembly.GetAssembly(typeof(MyEventsContextInitializer)).CodeBase).LocalPath;
            FileStream fs = new FileStream(Path.Combine(Path.GetDirectoryName(path),
                                                         "FakeImages\\visualstudio.png"), FileMode.Open, FileAccess.Read);

            using (BinaryReader br = new BinaryReader(fs))
            {
                return br.ReadBytes((int)fs.Length);
            }
        }

    }
}
