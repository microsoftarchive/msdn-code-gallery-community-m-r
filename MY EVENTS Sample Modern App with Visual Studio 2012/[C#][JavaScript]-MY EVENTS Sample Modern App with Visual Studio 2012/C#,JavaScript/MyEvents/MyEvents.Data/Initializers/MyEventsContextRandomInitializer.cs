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
    public class MyEventsContextRandomInitializer
        : DropCreateDatabaseAlways<MyEventsContext>
    {
        private const string LoremIpsumText = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

        private const string SessionDescription = "Microsoft Visual Studio 2012 and the Microsoft .NET Framework 4.5 mark the next generation of developer tools from Microsoft. " +
            "Designed to address the latest needs of developers, Visual Studio delivers key innovations to address emerging requirements around Application Lifecycle Management (ALM). With Visual Studio 2012 and the .NET Framework 4.5," +
            "Microsoft delivers tooling and framework support for the latest innovations in application architecture, development, and deployment. The .NET Framework 4.5 contains numerous improvements that make it easier to develop powerful " +
            "and compelling applications. Attend this webcast to learn how Visual Studio 2012 and the .NET Framework 4.5 provide developers with the tooling support and the platform support needed to create amazing solutions. We also explore the core capabilities of these new technologies.";

        private const string SpeakerBio = "He is the corporate vice president of the Visual Studio Team in the Developer Division at Microsoft. Zander's responsibilities include the Visual Studio family of products, which covers a range of technologies: programming languages; JavaScript runtime and tools; integrated development environment and ecosystem; Microsoft Office, SharePoint and cloud tooling integration; source control and work item tracking; and advanced architecture, developer, and testing tools."
                    + "As one of the original developers of the Common Language Runtime (CLR), Zander's primary technical areas of contribution include file formats, metadata, compilers, debugging and profiling, and integration of the system into key platforms such as operating systems and databases. Before joining the Visual Studio Team, Zander was the general manager for the .NET Framework Team. He has worked on numerous products at Microsoft, including the first several releases of the CLR and .NET Framework, Silverlight, SourceSafe, and ODBC. Before joining Microsoft in 1992, Zander worked at IBM Corp. on distributed SQL and SQL/400 at the Rochester lab."
                    + "Zander holds a Bachelor of Science degree in computer science from Minnesota State University. In his spare time, he enjoys playing with his three children and making furniture in his shop.";

        private const int NumberOfEventsForFirstUser = 5;
        private static readonly Random Randomize = new Random();
        private static List<int> _organizerIds;
        private static List<RegisteredUser> _registeredUsers = new List<RegisteredUser>();
        private static List<int> _eventIds;
        private static List<int> _sessionIds;
        private static string _fakeUserFacebookId = "100004210809580";
        private static string _fakeUserName = "Andrew Davis";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(MyEventsContext context)
        {
            const int eventDefinitions = 20;
            InitializeEvents(context, eventDefinitions);
            InitializeSessions(context, _eventIds);
            InitializeAttendees(context, _eventIds);
            InitializeMaterials(context, _sessionIds);
            InitializeComments(context, _sessionIds, _registeredUsers, 1);
            AddRegisteredUserToSessions(context, _registeredUsers, _sessionIds);
        }

        private static void InitializeEvents(MyEventsContext context, int count)
        {
            InitializeOrganizers(context, count);

            for (int i = 0; i < count; i++)
            {
                AddEvent(context, i);
            }
            var eventIAssist = context.EventDefinitions.OrderBy(e => e.EventDefinitionId).Skip(context.EventDefinitions.Count() - 2).Take(2);
            var fakeOrganizer = context.RegisteredUsers.First(ru => ru.FacebookId == _fakeUserFacebookId);
            fakeOrganizer.AttendeeEventDefinitions = new List<EventDefinition>();
            foreach (var eventDefinition in eventIAssist)
            {
                fakeOrganizer.AttendeeEventDefinitions.Add(eventDefinition);
            }
            _eventIds = context.EventDefinitions.Select(e => e.EventDefinitionId).ToList();


        }

        private static void AddEvent(MyEventsContext context, int index)
        {
            DateTime date = System.DateTime.Now.AddDays(index + 15);
            date = new DateTime(date.Year, date.Month, date.Day, 9, 0, 0);
            //date = date.AddHours(date.Hour).AddHours(10);

            var eventDef = new EventDefinition();
            if (index < NumberOfEventsForFirstUser && context.RegisteredUsers.Any())
            {
                eventDef.OrganizerId = _organizerIds.First();
            }
            else
            {
                eventDef.OrganizerId = eventDef.OrganizerId = _organizerIds[GetNumber(0, _organizerIds.Count - 1)];
            }
            eventDef.Name = GetEventName(index);
            eventDef.Description = GetEventDescription();
            List<string> city = GetCity();
            eventDef.Address = city[1];
            eventDef.City = city[0];
            eventDef.Tags = GetEventTags();
            eventDef.TwitterAccount = "@visualstudio";
            eventDef.RoomNumber = GetNumber(1, 3);
            eventDef.Date = date;
            eventDef.StartTime = date;
            eventDef.TimeZoneOffset = 2;
            eventDef.EndTime = date.AddHours(10);
            eventDef.Likes = GetNumber();
            eventDef.Logo = GetDefaultImage();
            eventDef.Latitude = 47.70002f;
            eventDef.Longitude = -122.1303f;
            eventDef.MapImage = GetDefaultMap();

            context.EventDefinitions.Add(eventDef);

            context.SaveChanges();

            InitializeRoomPoint(context, eventDef);
        }

        private static void InitializeOrganizers(MyEventsContext context, int count)
        {
            for (int i = 0; i < count; i++)
            {
                AddOrganizer(context);
            }
            _organizerIds = context.RegisteredUsers.Select(o => o.RegisteredUserId).ToList();
        }

        private static void AddOrganizer(MyEventsContext context)
        {
            var facebookId = GetFacebookId();
            if (!context.RegisteredUsers.Any())
            {
                facebookId = _fakeUserFacebookId;
            }
            var existsOrganizer = context.RegisteredUsers.Any(ru => ru.FacebookId == facebookId);
            if (existsOrganizer)
                return;

            var organizer = new RegisteredUser();

            if (facebookId == _fakeUserFacebookId)
                organizer.Name = _fakeUserName;
            else
                organizer.Name = GetOrganizerName();

            organizer.FacebookId = facebookId;
            organizer.Email = "organizer@domain.com";
            organizer.City = GetCity()[0];
            organizer.Bio = SpeakerBio;
            context.RegisteredUsers.Add(organizer);
            context.SaveChanges();
        }

        private static void InitializeSessions(MyEventsContext context, List<int> eventDefinitionIds)
        {
            foreach (var eventDefinitionId in eventDefinitionIds)
            {
                int number = GetNumber(5, 15);
                for (int i = 0; i < number; i++)
                {
                    AddSession(context, eventDefinitionId, i + 1);
                }
            }

            context.SaveChanges();
            _sessionIds = context.Sessions.Select(s => s.SessionId).ToList();
        }

        private static void AddSession(MyEventsContext context, int eventDefinitionId, int startingHour)
        {
            var parentEvent = context.EventDefinitions.First(ev => ev.EventDefinitionId == eventDefinitionId);
            var session = new Session();
            session.Title = GetSessionName();
            session.Description = SessionDescription;
            session.Speaker = GetName();
            session.Biography = SpeakerBio;
            session.TwitterAccount = "@visualstudio";
            session.EventDefinitionId = eventDefinitionId;
            session.StartTime = parentEvent.StartTime.AddHours(startingHour);
            session.StartTime = new DateTime(session.StartTime.Year, session.StartTime.Month, session.StartTime.Day, session.StartTime.Hour, 0, 0);
            session.TimeZoneOffset = 2;
            session.RoomNumber = GetNumber(0, parentEvent.RoomNumber);
            session.Duration = 60;

            context.Sessions.Add(session);
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
        }

        private static void AddAttendee(MyEventsContext context, EventDefinition eventDefinition)
        {
            var attendee = new RegisteredUser();
            attendee.Name = GetName();
            attendee.FacebookId = GetFacebookId();
            attendee.Email = "attendee@domain.com";
            attendee.City = GetCity()[0];
            attendee.Bio = SpeakerBio;
            attendee.AttendeeEventDefinitions = new List<EventDefinition>();
            attendee.AttendeeEventDefinitions.Add(eventDefinition);
            context.RegisteredUsers.Add(attendee);
        }

        private static void InitializeRoomPoint(MyEventsContext context, EventDefinition eventDefinition)
        {
            RoomPoint room;
            if (eventDefinition.RoomNumber >= 1)
            {
                room = new RoomPoint();
                room.EventDefinitionId = eventDefinition.EventDefinitionId;
                room.PointX = 35;
                room.PointY = 14;
                room.RoomNumber = 1;
                context.RoomPoints.Add(room);

                room = new RoomPoint();
                room.EventDefinitionId = eventDefinition.EventDefinitionId;
                room.PointX = 34;
                room.PointY = 83;
                room.RoomNumber = 1;
                context.RoomPoints.Add(room);

                room = new RoomPoint();
                room.EventDefinitionId = eventDefinition.EventDefinitionId;
                room.PointX = 15;
                room.PointY = 84;
                room.RoomNumber = 1;
                context.RoomPoints.Add(room);

                room = new RoomPoint();
                room.EventDefinitionId = eventDefinition.EventDefinitionId;
                room.PointX = 13;
                room.PointY = 135;
                room.RoomNumber = 1;
                context.RoomPoints.Add(room);

                room = new RoomPoint();
                room.EventDefinitionId = eventDefinition.EventDefinitionId;
                room.PointX = 34;
                room.PointY = 136;
                room.RoomNumber = 1;
                context.RoomPoints.Add(room);

                room = new RoomPoint();
                room.EventDefinitionId = eventDefinition.EventDefinitionId;
                room.PointX = 34;
                room.PointY = 245;
                room.RoomNumber = 1;
                context.RoomPoints.Add(room);

                room = new RoomPoint();
                room.EventDefinitionId = eventDefinition.EventDefinitionId;
                room.PointX = 336;
                room.PointY = 242;
                room.RoomNumber = 1;
                context.RoomPoints.Add(room);

                room = new RoomPoint();
                room.EventDefinitionId = eventDefinition.EventDefinitionId;
                room.PointX = 336;
                room.PointY = 137;
                room.RoomNumber = 1;
                context.RoomPoints.Add(room);

                room = new RoomPoint();
                room.EventDefinitionId = eventDefinition.EventDefinitionId;
                room.PointX = 376;
                room.PointY = 136;
                room.RoomNumber = 1;
                context.RoomPoints.Add(room);

                room = new RoomPoint();
                room.EventDefinitionId = eventDefinition.EventDefinitionId;
                room.PointX = 142;
                room.PointY = 14;
                room.RoomNumber = 1;
                context.RoomPoints.Add(room);
            }

            if (eventDefinition.RoomNumber >= 2)
            {
                room = new RoomPoint();
                room.EventDefinitionId = eventDefinition.EventDefinitionId;
                room.PointX = 445;
                room.PointY = 126;
                room.RoomNumber = 2;
                context.RoomPoints.Add(room);

                room = new RoomPoint();
                room.EventDefinitionId = eventDefinition.EventDefinitionId;
                room.PointX = 389;
                room.PointY = 241;
                room.RoomNumber = 2;
                context.RoomPoints.Add(room);

                room = new RoomPoint();
                room.EventDefinitionId = eventDefinition.EventDefinitionId;
                room.PointX = 763;
                room.PointY = 241;
                room.RoomNumber = 2;
                context.RoomPoints.Add(room);

                room = new RoomPoint();
                room.EventDefinitionId = eventDefinition.EventDefinitionId;
                room.PointX = 571;
                room.PointY = 139;
                room.RoomNumber = 2;
                context.RoomPoints.Add(room);

                room = new RoomPoint();
                room.EventDefinitionId = eventDefinition.EventDefinitionId;
                room.PointX = 559;
                room.PointY = 147;
                room.RoomNumber = 2;
                context.RoomPoints.Add(room);

                room = new RoomPoint();
                room.EventDefinitionId = eventDefinition.EventDefinitionId;
                room.PointX = 516;
                room.PointY = 128;
                room.RoomNumber = 2;
                context.RoomPoints.Add(room);

                room = new RoomPoint();
                room.EventDefinitionId = eventDefinition.EventDefinitionId;
                room.PointX = 487;
                room.PointY = 148;
                room.RoomNumber = 2;
                context.RoomPoints.Add(room);
            }

            if (eventDefinition.RoomNumber >= 3)
            {
                room = new RoomPoint();
                room.EventDefinitionId = eventDefinition.EventDefinitionId;
                room.PointX = 460;
                room.PointY = 299;
                room.RoomNumber = 3;
                context.RoomPoints.Add(room);

                room = new RoomPoint();
                room.EventDefinitionId = eventDefinition.EventDefinitionId;
                room.PointX = 458;
                room.PointY = 455;
                room.RoomNumber = 3;
                context.RoomPoints.Add(room);

                room = new RoomPoint();
                room.EventDefinitionId = eventDefinition.EventDefinitionId;
                room.PointX = 675;
                room.PointY = 455;
                room.RoomNumber = 3;
                context.RoomPoints.Add(room);

                room = new RoomPoint();
                room.EventDefinitionId = eventDefinition.EventDefinitionId;
                room.PointX = 673;
                room.PointY = 351;
                room.RoomNumber = 3;
                context.RoomPoints.Add(room);

                room = new RoomPoint();
                room.EventDefinitionId = eventDefinition.EventDefinitionId;
                room.PointX = 551;
                room.PointY = 342;
                room.RoomNumber = 3;
                context.RoomPoints.Add(room);
            }

            context.SaveChanges();



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
                }

                context.SaveChanges();
            }
        }

        private static string GetEventName(int valueIndex)
        {
            List<string> names = new List<string>() { 
                "Visual Studio 2012",
                "Windows 8 Camp", 
                "Meet Visual Studio 2012", 
                "Visual Studio ALM Day", 
                "Microsoft Worldwide Partner Conference", 
                "Microsoft ALM Summit",
                "Windows 8 Developer Camp",
                "Visual Studio Day",
                "Developer Day",
                "Visual Studio 2012",
                "ALM Open Day",
                "Windows Phone Camp",
                "ASP.NET Conference",
                "SharePoint Summit",
                "MVP Summit",
                "The ALM Series",
                "TechEd North America",
                "Visual Studio Virtual Lab",
                "TechEd Europe", 
                "Visual Studio Public Sector DevCamp Series",
            };

            if (valueIndex >= 0 && valueIndex < names.Count)
            {
                return names[valueIndex];
            }
            else
            {
                int index = Randomize.Next(0, names.Count);
                return names[index];
            }
        }

        private static string GetEventTags()
        {
            List<string> tags = new List<string>() { 
                "Visual Studio, ASP.NET", 
                "ALM", 
                "ASP.NET MVC", 
                "Windows 8", 
                "Visual Studio"
            };
            int index = Randomize.Next(0, tags.Count);
            return tags[index];
        }

        private static string GetComment()
        {
            List<string> comments = new List<string>() { 
                "Great session! Great speaker! I love it!", 
                "The speaker maybe is not the best one to explain this topic. It´s not clear the message.", 
                "Awesome! This kind of sessions are very useful for me. I would like to have more!", 
                "it seems a good technology but I don´t know if I could use it in my environment."
            };
            int index = Randomize.Next(0, comments.Count);
            return comments[index];
        }

        private static string GetOrganizerName()
        {
            List<string> organizers = new List<string>() 
            {   "Microsoft Corporation", 
                "Microsoft Reston Office", 
                "Microsoft Store", 
                "Microsoft Denver Office" };
            int index = Randomize.Next(0, organizers.Count);
            return organizers[index];
        }

        private static string GetEventDescription()
        {
            List<string> descriptions = new List<string>() { 
                "TechEd is Microsoft's premier technology conference for IT professionals and developers, offering the most comprehensive technical education across Microsoft's current and soon-to-be-released suite of products, solutions, tools, and services. TechEd offers hands-on learning, deep product exploration and countless opportunities to build relationships with a community of industry and Microsoft experts that will help your work for years to come.If you are developing, deploying, managing, securing and mobilizing Microsoft solutions, TechEd is your opportunity to focus on the key technology and business issues that will help you solve today's real-world IT challenges and prepare you for tomorrow's innovations."
                , "Microsoft Visual Studio 2012 and the Microsoft .NET Framework 4.5 mark the next generation of developer tools from Microsoft. Designed to address the latest needs of developers, Visual Studio delivers key innovations to address emerging requirements around Application Lifecycle Management (ALM)."
                };
            int index = Randomize.Next(0, descriptions.Count);
            return descriptions[index];
        }

        private static string GetName()
        {
            List<string> names = new List<string>() { 
                "David Alexander"
                , "Thomas Andersen"
                , "Christen Anderson"
                , "Josh Bailey"
                , "Eric Lang"
                , "Humberto Acevedo"
                , "Tom Perham"
                , "Jeff Phillips"
                , "Carole Poland"
                , "Cristina Potra"
                , "Adam Barr" 
                , "David Jones" 
                , "David Hamilton" 
                , "Christa Geller" 
                , "Garth Fort" 
                , "Andrew Davis" 
                };
            int index = Randomize.Next(0, names.Count);
            return names[index];
        }

        private static List<string> GetCity()
        {
            List<List<string>> cities = new List<List<string>>() { 
                new List<string>() { "Washington Columbia", "1331 Pennsylvania Ave NW Washington"}, 
                new List<string>() { "Irving Texas", "7000 N State Hwy 161"}, 
                new List<string>() { "Raleigh North Carolina", "1101 Gorman St"}, 
                new List<string>() { "Tampa Florida", "5426 Bay Center Dr Suite 700"}, 
                new List<string>() { "Los Angeles California", "3333 Bristol St"}, 
                new List<string>() { "Irvine California", "3 Park Plaza Suite, 1600"}, 
            };
            int index = Randomize.Next(0, cities.Count);
            return cities[index];
        }

        private static string GetFacebookId()
        {
            List<string> ids = new List<string>() { "100004118912757", "1010246007", "1007691855", "1453295348", "814188598", "1026120498", "1350603074" };
            int index = Randomize.Next(0, ids.Count);
            return ids[index];
        }

        private static string GetFacebookUsername()
        {
            List<string> ids = new List<string>() { "yngir", "alfredofl", "albabarrerajimenez", "ibon.landa", "josueyeray" };
            int index = Randomize.Next(0, ids.Count);
            return ids[index];
        }

        private static string GetSessionName()
        {
            List<string> names = new List<string>() {
                "ASP.NET & Web Services Security", 
                "Practical LINQ in C# (Level 100)", 
                "Overview of Visual Studio 2012 and the .NET Framework 4.5",
                "Architecture Tools in Visual Studio 2012",
                "My First Mobile Application with Visual Studio 2012",
                "What’s New in Visual Studio 2012",
                "What’s New in Visual Studio LightSwitch",
                "Test-Driven Development Using Visual Studio",
                "Application Lifecycle Management(ALM): It’s a Team Sport" 
            };
            int index = Randomize.Next(0, names.Count);
            return names[index];
        }

        private static int GetNumber(int min = 0, int max = 300)
        {
            max++;
            return Randomize.Next(min, max);
        }

        private static string GetLogoName()
        {
            List<string> names = new List<string>() { 
                "FakeImages\\atlanta.png",
                "FakeImages\\brasil-saopaulo.png",
                "FakeImages\\chicago.png",
                "FakeImages\\mexico-city.png",
                "FakeImages\\seattle.png",
                "FakeImages\\spain-madrid.png",
                "FakeImages\\uk.png",
                "FakeImages\\we-copenhagen.png",
            };
            int index = Randomize.Next(0, names.Count);
            return names[index];
        }

        private static string GetEventMap()
        {
            List<string> maps = new List<string>() { 
                "FakeImages\\map.png" };
            int index = Randomize.Next(0, maps.Count);
            return maps[index];
        }

        private static byte[] GetDefaultImage()
        {
            string path = new Uri(Assembly.GetAssembly(typeof(MyEventsContextInitializer)).CodeBase).LocalPath;
            FileStream fs = new FileStream(Path.Combine(Path.GetDirectoryName(path), GetLogoName()), FileMode.Open, FileAccess.Read);

            using (BinaryReader br = new BinaryReader(fs))
            {
                return br.ReadBytes((int)fs.Length);
            }
        }

        private static byte[] GetDefaultMap()
        {
            string path = new Uri(Assembly.GetAssembly(typeof(MyEventsContextInitializer)).CodeBase).LocalPath;
            FileStream fs = new FileStream(Path.Combine(Path.GetDirectoryName(path), GetEventMap()), FileMode.Open, FileAccess.Read);

            using (BinaryReader br = new BinaryReader(fs))
            {
                return br.ReadBytes((int)fs.Length);
            }
        }

        private static bool GetRated()
        {
            return new Random().Next(100) % 2 == 0;
        }
    }
}
