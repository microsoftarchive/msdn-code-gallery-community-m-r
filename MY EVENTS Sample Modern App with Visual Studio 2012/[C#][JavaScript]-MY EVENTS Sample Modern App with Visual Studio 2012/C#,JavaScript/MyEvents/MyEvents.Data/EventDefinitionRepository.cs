using System;
using System.Collections.Generic;
using System.Linq;
using MyEvents.Model;

namespace MyEvents.Data
{
    /// <summary>
    /// <see cref="MyEvents.Data.IEventDefinitionRepository"/>
    /// </summary>
    public class EventDefinitionRepository : IEventDefinitionRepository
    {
        /// <summary>
        /// <see cref="MyEvents.Data.IEventDefinitionRepository"/>
        /// </summary>
        /// <returns><see cref="MyEvents.Data.IEventDefinitionRepository"/></returns>
        public int GetCount()
        {
            using (var context = new MyEventsContext())
            {
                return context.EventDefinitions.Count();
            }
        }

         /// <summary>
        /// <see cref="MyEvents.Data.IEventDefinitionRepository"/>
        /// </summary>
        /// <param name="pageSize"><see cref="MyEvents.Data.IEventDefinitionRepository"/></param>
        /// <param name="pageIndex"><see cref="MyEvents.Data.IEventDefinitionRepository"/></param>
        /// <returns><see cref="MyEvents.Data.IEventDefinitionRepository"/></returns>
        public IList<EventDefinition> GetAll(int pageSize, int pageIndex)
        {
            return GetAllWithUserInfo(0, pageSize, pageIndex);
        }

        /// <summary>
        /// <see cref="MyEvents.Data.IEventDefinitionRepository"/>
        /// </summary>
        /// <param name="registeredUserId"><see cref="MyEvents.Data.IEventDefinitionRepository"/></param>
        /// <param name="pageSize"><see cref="MyEvents.Data.IEventDefinitionRepository"/></param>
        /// <param name="pageIndex"><see cref="MyEvents.Data.IEventDefinitionRepository"/></param>
        /// <returns><see cref="MyEvents.Data.IEventDefinitionRepository"/></returns>
        public IList<EventDefinition> GetAllWithUserInfo(int registeredUserId, int pageSize, int pageIndex)
        {
            var today = DateTime.Now.Date;
            using (var context = new MyEventsContext())
            {
                return context.EventDefinitions
                    .Include("Organizer")
                    .Include("RegisteredUsers")
                    .Where(q => q.Date >= today)
                    .OrderBy(q => q.Date)
                    .Skip(pageIndex * pageSize).Take(pageSize)
                    .ToList()
                    .Select(e => new EventDefinition()
                    {
                        EventDefinitionId = e.EventDefinitionId,
                        OrganizerId = e.OrganizerId,
                        Name = e.Name,
                        Description = e.Description,
                        Address = e.Address,
                        ZipCode = e.ZipCode,
                        City = e.City,
                        Tags = e.Tags,
                        TwitterAccount = e.TwitterAccount,
                        TimeZoneOffset = e.TimeZoneOffset,
                        RoomNumber = e.RoomNumber,
                        Date = e.Date,
                        StartTime = e.StartTime,
                        EndTime = e.EndTime,
                        Likes = e.Likes,
                        Logo = e.Logo,
                        Latitude = e.Latitude,
                        Longitude = e.Longitude,
                        Organizer = new RegisteredUser()
                        {
                            Name = e.Organizer.Name, 
                            RegisteredUserId = e.Organizer.RegisteredUserId
                        },
                        Sessions = e.Sessions,
                        AttendeesCount = e.RegisteredUsers.Count(),
                        Registered = e.RegisteredUsers.Any(rs => rs.RegisteredUserId == registeredUserId)
                    })
                    .OrderBy(q => q.Date)
                    .ToList();
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.IEventDefinitionRepository"/>
        /// </summary>
        /// <param name="number"><see cref="MyEvents.Data.IEventDefinitionRepository"/></param>
        /// <returns><see cref="MyEvents.Data.IEventDefinitionRepository"/></returns>
        public IList<EventDefinition> GetLast(int number)
        {
            return GetLastWithUserInfo(0, number);
        }

        /// <summary>
        /// <see cref="MyEvents.Data.IEventDefinitionRepository"/>
        /// </summary>
        /// <param name="registeredUserId"><see cref="MyEvents.Data.IEventDefinitionRepository"/></param>
        /// <param name="number"><see cref="MyEvents.Data.IEventDefinitionRepository"/></param>
        /// <returns><see cref="MyEvents.Data.IEventDefinitionRepository"/></returns>
        public IList<EventDefinition> GetLastWithUserInfo(int registeredUserId, int number)
        {
            using (var context = new MyEventsContext())
            {
                var dateTimeToCompare = DateTime.Now.Date.AddMonths(1);
                var today = DateTime.Now.Date;
                return  context.EventDefinitions
                    .Include("Organizer")
                    .Include("RegisteredUsers")
                    .Where(q => q.Date >= today && q.Date <= dateTimeToCompare)
                    .OrderBy(q => q.Date)
                    .Take(number)
                    .ToList()
                    .Select(e => new EventDefinition()
                    {
                        EventDefinitionId = e.EventDefinitionId,
                        OrganizerId = e.OrganizerId,
                        Name = e.Name,
                        Description = e.Description,
                        Address = e.Address,
                        ZipCode = e.ZipCode,
                        City = e.City,
                        Tags = e.Tags,
                        TwitterAccount = e.TwitterAccount,
                        TimeZoneOffset = e.TimeZoneOffset,
                        RoomNumber = e.RoomNumber,
                        Date = e.Date,
                        StartTime = e.StartTime,
                        EndTime = e.EndTime,
                        Likes = e.Likes,
                        Logo = e.Logo,
                        Latitude = e.Latitude,
                        Longitude = e.Longitude,
                        Organizer = new RegisteredUser()
                        {
                            Name = e.Organizer.Name,
                            RegisteredUserId = e.Organizer.RegisteredUserId
                        },
                        Sessions = e.Sessions,
                        AttendeesCount = e.RegisteredUsers.Count(),
                        Registered = e.RegisteredUsers.Any(rs => rs.RegisteredUserId == registeredUserId)
                    }).ToList();
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.IEventDefinitionRepository"/>
        /// </summary>
        /// <returns><see cref="MyEvents.Data.IEventDefinitionRepository"/></returns>
        public IList<EventDefinition> GetCurrent()
        {
            var today = DateTime.Now.Date;
            using (var context = new MyEventsContext())
            {
                var currentEvents =  context.EventDefinitions
                    .Where(q => q.Date >= today)
                    .OrderBy(q => q.Date)
                    .ToList()
                    .Select(e => new EventDefinition()
                    {
                        EventDefinitionId = e.EventDefinitionId,
                        Name = e.Name, 
                        Logo = e.Logo
                    });

                return currentEvents.ToList();
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.IEventDefinitionRepository"/>
        /// </summary>
        /// <returns><see cref="MyEvents.Data.IEventDefinitionRepository"/></returns>
        public int GetCountByOrganizerId(int organizerId, string filter)
        {
            using (var context = new MyEventsContext())
            {
                return context.EventDefinitions
                    .Count(q => q.OrganizerId == organizerId && (String.IsNullOrEmpty(filter) || q.Name.Contains(filter)));
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.IEventDefinitionRepository"/>
        /// </summary>
        /// <param name="organizerId"><see cref="MyEvents.Data.IEventDefinitionRepository"/></param>
        /// <param name="filter"><see cref="MyEvents.Data.IEventDefinitionRepository"/></param>
        /// <param name="pageSize"><see cref="MyEvents.Data.IEventDefinitionRepository"/></param>
        /// <param name="pageIndex"><see cref="MyEvents.Data.IEventDefinitionRepository"/></param>
        /// <param name="completeInfo"><see cref="MyEvents.Data.IEventDefinitionRepository"/></param>
        /// <returns><see cref="MyEvents.Data.IEventDefinitionRepository"/></returns>
        public IList<EventDefinition> GetByOrganizerId(int organizerId, string filter, int pageSize, int pageIndex, bool completeInfo)
        {
            using (var context = new MyEventsContext())
            {
                return context.EventDefinitions
                    .Include("Organizer")
                    .Include("RegisteredUsers")
                    .Include("Sessions")
                    .Where(q => q.OrganizerId == organizerId && (String.IsNullOrEmpty(filter) || q.Name.Contains(filter)))
                    .OrderByDescending(q => q.Date)
                    .Skip(pageIndex * pageSize).Take(pageSize)
                    .ToList()
                    .Select(e => new EventDefinition()
                    {
                        EventDefinitionId = e.EventDefinitionId,
                        OrganizerId = e.OrganizerId,
                        Name = e.Name,
                        Description = e.Description,
                        Address = e.Address,
                        ZipCode = e.ZipCode,
                        City = e.City,
                        Tags = e.Tags,
                        TwitterAccount = e.TwitterAccount,
                        TimeZoneOffset = e.TimeZoneOffset,
                        RoomNumber = e.RoomNumber,
                        Date = e.Date,
                        StartTime = e.StartTime,
                        EndTime = e.EndTime,
                        Likes = e.Likes,
                        Logo = e.Logo,
                        Latitude = e.Latitude,
                        Longitude = e.Longitude,
                        Organizer = new RegisteredUser()
                        {
                            Name = e.Organizer.Name,
                            RegisteredUserId = e.Organizer.RegisteredUserId
                        },
                        Sessions = completeInfo ? e.Sessions : null,
                        AttendeesCount = e.RegisteredUsers.Count(),
                    })
                    .ToList();

            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.IEventDefinitionRepository"/>
        /// </summary>
        /// <param name="eventDefinitionId"><see cref="MyEvents.Data.IEventDefinitionRepository"/></param>
        /// <returns><see cref="MyEvents.Data.IEventDefinitionRepository"/></returns>
        public EventDefinition GetById(int eventDefinitionId)
        {
            using (var context = new MyEventsContext())
            {
                return
                    context.EventDefinitions
                    .Include("Organizer")
                    .Include("RegisteredUsers")
                    .Include("Sessions.SessionRegisteredUsers")
                    .Where(q => q.EventDefinitionId == eventDefinitionId)
                    .ToList()
                    .Select(e => new EventDefinition()
                    {
                        EventDefinitionId = e.EventDefinitionId,
                        OrganizerId = e.OrganizerId,
                        Name = e.Name,
                        Description = e.Description,
                        Address = e.Address,
                        ZipCode = e.ZipCode,
                        City = e.City,
                        Tags = e.Tags,
                        TwitterAccount = e.TwitterAccount,
                        TimeZoneOffset = e.TimeZoneOffset,
                        RoomNumber = e.RoomNumber,
                        Date = e.Date,
                        StartTime = e.StartTime,
                        EndTime = e.EndTime,
                        Likes = e.Likes,
                        Logo = e.Logo,
                        Latitude = e.Latitude,
                        Longitude = e.Longitude,
                        Organizer = new RegisteredUser()
                        {
                            Name = e.Organizer.Name,
                            RegisteredUserId = e.Organizer.RegisteredUserId
                        },
                        Sessions = e.Sessions,
                        AttendeesCount = e.RegisteredUsers.Count(),
                    })
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.IEventDefinitionRepository"/>
        /// </summary>
        /// <param name="registeredUserId"><see cref="MyEvents.Data.IEventDefinitionRepository"/></param>
        /// <param name="eventDefinitionId"><see cref="MyEvents.Data.IEventDefinitionRepository"/></param>
        /// <returns><see cref="MyEvents.Data.IEventDefinitionRepository"/></returns>
        public EventDefinition GetByIdWithUserInfo(int registeredUserId, int eventDefinitionId)
        {
            using (var context = new MyEventsContext())
            {
                return
                    context.EventDefinitions
                    .Include("Organizer")
                    .Include("RegisteredUsers")
                    .Where(q => q.EventDefinitionId == eventDefinitionId)
                    .ToList()
                    .Select(e => new EventDefinition()
                    {
                        EventDefinitionId = e.EventDefinitionId,
                        OrganizerId = e.OrganizerId,
                        Name = e.Name,
                        Description = e.Description,
                        Address = e.Address,
                        ZipCode = e.ZipCode,
                        City = e.City,
                        Tags = e.Tags,
                        TwitterAccount = e.TwitterAccount,
                        TimeZoneOffset = e.TimeZoneOffset,
                        RoomNumber = e.RoomNumber,
                        Date = e.Date,
                        StartTime = e.StartTime,
                        EndTime = e.EndTime,
                        Likes = e.Likes,
                        Logo = e.Logo,
                        Latitude = e.Latitude,
                        Longitude = e.Longitude,
                        Organizer = new RegisteredUser()
                        {
                            Name = e.Organizer.Name,
                            RegisteredUserId = e.Organizer.RegisteredUserId
                        },
                        AttendeesCount = e.RegisteredUsers.Count(),
                        Registered = e.RegisteredUsers.Any(rs => rs.RegisteredUserId == registeredUserId)
                    })
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.IEventDefinitionRepository"/>
        /// </summary>
        /// <param name="eventDefinition"><see cref="MyEvents.Data.IEventDefinitionRepository"/></param>
        /// <returns><see cref="MyEvents.Data.IEventDefinitionRepository"/></returns>
        public int Add(EventDefinition eventDefinition)
        {
            using (var context = new MyEventsContext())
            {
                CheckAndFixTwitterAccount(eventDefinition);
                context.EventDefinitions.Add(eventDefinition);
                context.SaveChanges();
                return eventDefinition.EventDefinitionId;
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.IEventDefinitionRepository"/>
        /// </summary>
        /// <param name="eventDefinition"><see cref="MyEvents.Data.IEventDefinitionRepository"/></param>
        public void Update(EventDefinition eventDefinition)
        {
            using (var context = new MyEventsContext())
            {
                var eventDefinitionToUpdate = context.EventDefinitions
                    .FirstOrDefault(q => q.EventDefinitionId == eventDefinition.EventDefinitionId);

                eventDefinitionToUpdate.EventDefinitionId = eventDefinition.EventDefinitionId;
                eventDefinitionToUpdate.OrganizerId = eventDefinition.OrganizerId;
                eventDefinitionToUpdate.Name = eventDefinition.Name;
                eventDefinitionToUpdate.Description = eventDefinition.Description;
                eventDefinitionToUpdate.Address = eventDefinition.Address;
                eventDefinitionToUpdate.ZipCode = eventDefinition.ZipCode;
                eventDefinitionToUpdate.City = eventDefinition.City;
                eventDefinitionToUpdate.Tags = eventDefinition.Tags;
                eventDefinitionToUpdate.TwitterAccount = eventDefinition.TwitterAccount;
                eventDefinitionToUpdate.TimeZoneOffset = eventDefinition.TimeZoneOffset;
                eventDefinitionToUpdate.RoomNumber = eventDefinition.RoomNumber;
                eventDefinitionToUpdate.Date = eventDefinition.Date;

                // Be sure that the starttime and endtime is in the same day of the event
                eventDefinitionToUpdate.StartTime
                    = new DateTime(eventDefinition.Date.Year, eventDefinition.Date.Month, eventDefinition.Date.Day, eventDefinition.StartTime.Hour, eventDefinition.StartTime.Minute, 0);

                eventDefinitionToUpdate.EndTime
                    = new DateTime(eventDefinition.Date.Year, eventDefinition.Date.Month, eventDefinition.Date.Day, eventDefinition.EndTime.Hour, eventDefinition.EndTime.Minute, 0);

                eventDefinitionToUpdate.Likes = eventDefinition.Likes;
                eventDefinitionToUpdate.Logo = eventDefinition.Logo;
                eventDefinitionToUpdate.Latitude = eventDefinition.Latitude;
                eventDefinitionToUpdate.Longitude = eventDefinition.Longitude; 

                CheckAndFixTwitterAccount(eventDefinitionToUpdate);
                context.SaveChanges();

                // After update eventdefinition we have to clean the room number of the sessions to be sure that all room numbers exist
                CleanRooms(eventDefinition.EventDefinitionId, eventDefinition.RoomNumber);
            }
        }
        
        /// <summary>
        /// <see cref="MyEvents.Data.IEventDefinitionRepository"/>
        /// </summary>
        /// <param name="eventDefinitionId"><see cref="MyEvents.Data.IEventDefinitionRepository"/></param>
        public void Delete(int eventDefinitionId)
        {
            using (var context = new MyEventsContext())
            {
                var eventDefinition = context.EventDefinitions.FirstOrDefault(q => q.EventDefinitionId == eventDefinitionId);
                if (eventDefinition != null)
                {
                    context.EventDefinitions.Remove(eventDefinition);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.IEventDefinitionRepository"/>
        /// </summary>
        /// <param name="eventDefinitionId"><see cref="MyEvents.Data.IEventDefinitionRepository"/></param>
        /// <returns><see cref="MyEvents.Data.IEventDefinitionRepository"/></returns>
        public byte[] GetEventLogo(int eventDefinitionId)
        {
            using (var context = new MyEventsContext())
            {
                return context.EventDefinitions.Where(e => e.EventDefinitionId == eventDefinitionId).Select(e => e.Logo).FirstOrDefault();
            }
        }

        
        /// <summary>
        /// <see cref="MyEvents.Data.IEventDefinitionRepository"/>
        /// </summary>
        /// <param name="organizerId"><see cref="MyEvents.Data.IEventDefinitionRepository"/></param>
        /// <returns><see cref="MyEvents.Data.IEventDefinitionRepository"/></returns>
        public IList<Tag> GetTopTags(int organizerId)
        {
            using (var context = new MyEventsContext())
            {
                return context.EventDefinitions
                    .Where(e => e.OrganizerId == organizerId)
                    .ToList()
                    .SelectMany(e => e.Tags.Split(','))
                    .GroupBy(e => e.Trim())
                    .Select(g => new Tag() { Name = g.Key, Value = g.Count() })
                    .OrderByDescending(e => e.Value)
                    .Take(5)
                    .ToList();
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.IEventDefinitionRepository"/>
        /// </summary>
        /// <param name="organizerId"><see cref="MyEvents.Data.IEventDefinitionRepository"/></param>
        /// <returns><see cref="MyEvents.Data.IEventDefinitionRepository"/></returns>
        public IList<Speaker> GetTopSpeakers(int organizerId)
        {
            using (var context = new MyEventsContext())
            {
                var result =  context.Sessions
                    .Include("EventDefinition")
                    .Include("SessionRegisteredUsers")
                    .Where(s => s.EventDefinition.OrganizerId == organizerId)
                    .Select(ru => new Speaker()
                    {
                        Name = ru.Speaker,
                        Score =
                            ru.SessionRegisteredUsers.Any(sru => sru.Rated)
                            ? ru.SessionRegisteredUsers.Where(sru => sru.Rated).Average(sru => sru.Score) : 0
                    })
                    .GroupBy(s => s.Name)
                    .Select(g => new Speaker() { Name = g.Key, Score = g.Average(s => s.Score) })
                    .OrderByDescending(ru => ru.Score)
                    .Take(5)
                    .ToList();

                return result;
                   
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.ISessionRepository"/>
        /// </summary>
        /// <param name="eventDefinitionId"><see cref="MyEvents.Data.ISessionRepository"/></param>
        /// <returns><see cref="MyEvents.Data.ISessionRepository"/></returns>
        public byte[] GetRoomImage(int eventDefinitionId)
        {
            using (var context = new MyEventsContext())
            {
                return context.EventDefinitions
                    .Where(e => e.EventDefinitionId == eventDefinitionId)
                    .Single()
                    .MapImage;
            }
        }


        /// <summary>
        /// <see cref="MyEvents.Data.ISessionRepository"/>
        /// </summary>
        /// <param name="eventDefinitionId"><see cref="MyEvents.Data.ISessionRepository"/></param>
        /// <returns><see cref="MyEvents.Data.ISessionRepository"/></returns>
        public IList<RoomPoint> GetAllRoomPoints(int eventDefinitionId)
        {
            using (var context = new MyEventsContext())
            {
                var roomPoints = context.RoomPoints
                    .Where(e => e.EventDefinitionId == eventDefinitionId)
                    .OrderBy(e => e.RoomNumber)
                    .ToList();

                return roomPoints;
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.ISessionRepository"/>
        /// </summary>
        /// <param name="sessionId"><see cref="MyEvents.Data.ISessionRepository"/></param>
        /// <returns><see cref="MyEvents.Data.ISessionRepository"/></returns>
        public IList<RoomPoint> GetRoomPoints(int sessionId)
        {
            IList<RoomPoint> roomPoints = new List<RoomPoint>();
            using (var context = new MyEventsContext())
            {
                var session = context.Sessions.FirstOrDefault(s => s.SessionId == sessionId);
                if (session != null && session.RoomNumber > 0)
                {
                    roomPoints = context.RoomPoints
                        .Where(e => e.EventDefinitionId == session.EventDefinitionId && e.RoomNumber == session.RoomNumber)
                        .OrderBy(e => e.RoomNumber)
                        .ToList();
                }

                return roomPoints;
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.ISessionRepository"/>
        /// </summary>
        /// <param name="eventDefinition"><see cref="MyEvents.Data.ISessionRepository"/></param>
        public void UpdateRoomImage(EventDefinition eventDefinition)
        {
            using (var context = new MyEventsContext())
            {
                var eventDefinitionToUpdate = context.EventDefinitions
                    .FirstOrDefault(q => q.EventDefinitionId == eventDefinition.EventDefinitionId);

                eventDefinitionToUpdate.EventDefinitionId = eventDefinition.EventDefinitionId;
                eventDefinitionToUpdate.MapImage = eventDefinition.MapImage;

                context.SaveChanges();

            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.ISessionRepository"/>
        /// </summary>
        /// <param name="roomPoints"><see cref="MyEvents.Data.ISessionRepository"/></param>
        public void AddRoomPoints(IEnumerable<RoomPoint> roomPoints)
        {
            using (var context = new MyEventsContext())
            {
                DeleteRoomPoints(roomPoints.First().EventDefinitionId, roomPoints.First().RoomNumber);

                foreach (var roomPoint in roomPoints)
                {
                    context.RoomPoints.Add(roomPoint);
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.ISessionRepository"/>
        /// </summary>
        /// <param name="eventDefinitionId"><see cref="MyEvents.Data.ISessionRepository"/></param>
        /// <param name="roomNumber"><see cref="MyEvents.Data.ISessionRepository"/></param>
        public void DeleteRoomPoints(int eventDefinitionId, int roomNumber)
        {
            using (var context = new MyEventsContext())
            {
                var roomPoints = context.RoomPoints
                    .Where(q => q.EventDefinitionId == eventDefinitionId && q.RoomNumber == roomNumber)
                    .ToList();
                foreach (var roomPoint in roomPoints)
                    context.RoomPoints.Remove(roomPoint);

                context.SaveChanges();
            }
        }

        /// <summary>
        /// After update eventdefinition we have to clean the room number of the sessions to be sure that all room numbers exist
        /// </summary>
        /// <param name="eventDefinitionId">eventDefinitionId</param>
        /// <param name="roomNumber">Actual room number</param>
        private void CleanRooms(int eventDefinitionId, int roomNumber)
        {
            using (var context = new MyEventsContext())
            {
                // Clean Sessions
                var sessions = context.Sessions
                    .Where(q => q.EventDefinitionId == eventDefinitionId && q.RoomNumber > roomNumber).ToList();
                foreach (var session in sessions)
                    session.RoomNumber = 0;
                    

                // Delete room points of delete rooms
                var roomPoints = context.RoomPoints.Where(q => q.EventDefinitionId == eventDefinitionId && q.RoomNumber > roomNumber).ToList();
                foreach (var roomPoint in roomPoints)
                    context.RoomPoints.Remove(roomPoint);

                context.SaveChanges();
            }
        }

        private void CheckAndFixTwitterAccount(EventDefinition eventDefinition)
        {
            if (!eventDefinition.TwitterAccount.StartsWith("@"))
                eventDefinition.TwitterAccount = string.Format("@{0}", eventDefinition.TwitterAccount);
        }
    }
}
