using System;
using System.Collections.Generic;
using System.Linq;
using MyEvents.Model;

namespace MyEvents.Data
{
    /// <summary>
    /// <see cref="MyEvents.Data.IRegisteredUserRepository"/>
    /// </summary>
    public class RegisteredUserRepository : IRegisteredUserRepository
    {
        /// <summary>
        /// <see cref="MyEvents.Data.IRegisteredUserRepository"/>
        /// </summary>
        /// <param name="facebookId"><see cref="MyEvents.Data.IRegisteredUserRepository"/></param>
        /// <returns><see cref="MyEvents.Data.IRegisteredUserRepository"/></returns>
        public RegisteredUser Get(string facebookId)
        {
            using (var context = new MyEventsContext())
            {
                return context.RegisteredUsers.FirstOrDefault(q => q.FacebookId.Equals(facebookId, StringComparison.OrdinalIgnoreCase));
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.IRegisteredUserRepository"/>
        /// </summary>
        /// <param name="registeredUserId"><see cref="MyEvents.Data.IRegisteredUserRepository"/></param>
        /// <returns><see cref="MyEvents.Data.IRegisteredUserRepository"/></returns>
        public RegisteredUser GetById(int registeredUserId)
        {
            using (var context = new MyEventsContext())
            {
                return context.RegisteredUsers.FirstOrDefault(q => q.RegisteredUserId == registeredUserId);
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.IRegisteredUserRepository"/>
        /// </summary>
        /// <param name="eventDefinitionId"><see cref="MyEvents.Data.IRegisteredUserRepository"/></param>
        /// <returns><see cref="MyEvents.Data.IRegisteredUserRepository"/></returns>
        public IList<RegisteredUser> GetAllByEventId(int eventDefinitionId)
        {
            using (var context = new MyEventsContext())
            {
                return context.RegisteredUsers
                    .Where(q => q.AttendeeEventDefinitions.Any(e => e.EventDefinitionId == eventDefinitionId))
                    .OrderBy(q => q.Name)
                    .ToList();
            }
        }

        /// <summary>
        /// Get if user is already registered
        /// </summary>
        /// <param name="eventDefinitionId"></param>
        /// <param name="registeredUserId"></param>
        /// <returns></returns>
        public bool GetIfUserIsRegistered(int eventDefinitionId, int registeredUserId)
        {
            using (var context = new MyEventsContext())
            {
                var registeredUser = context.RegisteredUsers.Include("AttendeeEventDefinitions").FirstOrDefault(r => r.RegisteredUserId == registeredUserId);
                if (registeredUser != null)
                {
                    return registeredUser.AttendeeEventDefinitions.Select(e => e.EventDefinitionId).Contains(eventDefinitionId);
                }
            }
            return false;
        }

        /// <summary>
        /// <see cref="MyEvents.Data.IRegisteredUserRepository"/>
        /// </summary>
        /// <param name="sessionId"><see cref="MyEvents.Data.IRegisteredUserRepository"/></param>
        /// <returns><see cref="MyEvents.Data.IRegisteredUserRepository"/></returns>
        public IList<RegisteredUser> GetAllBySessionId(int sessionId)
        {
            using (var context = new MyEventsContext())
            {
                return context.RegisteredUsers.Include("SessionRegisteredUsers")
                    .Where(q => q.SessionRegisteredUsers.Any(s => s.SessionId == sessionId))
                    .OrderBy(q => q.Name)
                    .ToList();
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.IRegisteredUserRepository"/>
        /// </summary>
        /// <param name="registeredUserId"><see cref="MyEvents.Data.IRegisteredUserRepository"/></param>
        /// <returns><see cref="MyEvents.Data.IRegisteredUserRepository"/></returns>
        public IList<EventDefinition> GetEventDefinitions(int registeredUserId)
        {
            using (var context = new MyEventsContext())
            {
                return context.RegisteredUsers
                    .Include("AttendeeEventDefinitions")
                    .Where(q => q.RegisteredUserId == registeredUserId)
                    .SelectMany(q => q.AttendeeEventDefinitions)
                    .OrderByDescending(q => q.Date)
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
                             // TODO: Improve this query to avoid this sentence
                             Name = context.RegisteredUsers.First(r => r.RegisteredUserId == e.OrganizerId).Name,
                             RegisteredUserId = e.OrganizerId
                         },
                         Registered = true
                     }).ToList();
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.IRegisteredUserRepository"/>
        /// </summary>
        /// <param name="eventDefinitionId"><see cref="MyEvents.Data.IRegisteredUserRepository"/></param>
        /// <param name="registeredUserId"><see cref="MyEvents.Data.IRegisteredUserRepository"/></param>
        /// <returns><see cref="MyEvents.Data.IRegisteredUserRepository"/></returns>
        public IList<Session> GetSessions(int eventDefinitionId, int registeredUserId)
        {
            using (var context = new MyEventsContext())
            {
                return context.Sessions.Include("SessionRegisteredUsers").Include("Comments")
                                .Where(q =>
                                    q.EventDefinitionId == eventDefinitionId
                                    &&
                                    q.SessionRegisteredUsers.Any(s => s.RegisteredUserId == registeredUserId))
                                .OrderBy(q => q.StartTime)
                                .ToList();
            }
        }

        /// <summary>
        ///  <see cref="MyEvents.Data.IRegisteredUserRepository"/>
        /// </summary>
        /// <param name="registeredUser"> <see cref="MyEvents.Data.IRegisteredUserRepository"/></param>
        public int Add(RegisteredUser registeredUser)
        {
            using (var context = new MyEventsContext())
            {
                var user = Get(registeredUser.FacebookId);
                if (user == null)
                {
                    context.RegisteredUsers.Add(registeredUser);
                    context.SaveChanges();
                    return registeredUser.RegisteredUserId;
                }
                else
                {
                    return user.RegisteredUserId;
                }
            }
        }

        /// <summary>
        ///  <see cref="MyEvents.Data.IRegisteredUserRepository"/>
        /// </summary>
        /// <param name="registeredUserId"> <see cref="MyEvents.Data.IRegisteredUserRepository"/></param>
        /// <param name="eventDefinitionId"> <see cref="MyEvents.Data.IRegisteredUserRepository"/></param>
        public void AddRegisteredUserToEvent(int registeredUserId, int eventDefinitionId)
        {
            using (var context = new MyEventsContext())
            {
                var eventDefinition = context.EventDefinitions.Include("RegisteredUsers").FirstOrDefault(q => q.EventDefinitionId == eventDefinitionId);
                if (eventDefinition != null)
                {
                    if (!eventDefinition.RegisteredUsers.Any(s => s.RegisteredUserId == registeredUserId))
                    {
                        var registeredUser = context.RegisteredUsers.FirstOrDefault(q => q.RegisteredUserId == registeredUserId);
                        if (registeredUser != null)
                        {
                            eventDefinition.RegisteredUsers.Add(registeredUser);
                            context.SaveChanges();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.IRegisteredUserRepository"/>
        /// </summary>
        /// <param name="registeredUserId"> <see cref="MyEvents.Data.IRegisteredUserRepository"/></param>
        /// <param name="eventDefinitionId"> <see cref="MyEvents.Data.IRegisteredUserRepository"/></param>
        public void DeleteRegisteredUserFromEvent(int registeredUserId, int eventDefinitionId)
        {
            using (var context = new MyEventsContext())
            {
                var eventDefinition = context.EventDefinitions.Include("RegisteredUsers").FirstOrDefault(q => q.EventDefinitionId == eventDefinitionId);
                if (eventDefinition != null)
                {
                    var registerUser = eventDefinition.RegisteredUsers.FirstOrDefault(s => s.RegisteredUserId == registeredUserId);
                    if (registerUser != null)
                    {
                        eventDefinition.RegisteredUsers.Remove(registerUser);
                        context.SaveChanges();
                    }
                }
            }
        }

        /// <summary>
        ///  <see cref="MyEvents.Data.IRegisteredUserRepository"/>
        /// </summary>
        /// <param name="registeredUserId"> <see cref="MyEvents.Data.IRegisteredUserRepository"/></param>
        /// <param name="sessionId"> <see cref="MyEvents.Data.IRegisteredUserRepository"/></param>
        public void AddRegisteredUserToSession(int registeredUserId, int sessionId)
        {
            using (var context = new MyEventsContext())
            {
                var registeredUser = context.RegisteredUsers.FirstOrDefault(q => q.RegisteredUserId == registeredUserId);
                var session = context.Sessions.FirstOrDefault(q => q.SessionId == sessionId);

                if (registeredUser != null && session != null)
                {
                    context.SessionRegisteredUsers.Add(new SessionRegisteredUser()
                    {
                        SessionId = sessionId,
                        RegisteredUserId = registeredUserId, 
                        FacebookId = registeredUser.FacebookId
                    });

                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.IRegisteredUserRepository"/>
        /// </summary>
        /// <param name="registeredUserId"> <see cref="MyEvents.Data.IRegisteredUserRepository"/></param>
        /// <param name="sessionId"> <see cref="MyEvents.Data.IRegisteredUserRepository"/></param>
        public void DeleteRegisteredUserFromSession(int registeredUserId, int sessionId)
        {
            using (var context = new MyEventsContext())
            {
                var sessionRegisteredUser = context.SessionRegisteredUsers.FirstOrDefault(q => q.SessionId == sessionId && q.RegisteredUserId == registeredUserId);
                if (sessionRegisteredUser != null)
                {
                    context.SessionRegisteredUsers.Remove(sessionRegisteredUser);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.IRegisteredUserRepository"/>
        /// </summary>
        /// <param name="registeredUserId"><see cref="MyEvents.Data.IRegisteredUserRepository"/></param>
        /// <param name="sessionId"><see cref="MyEvents.Data.IRegisteredUserRepository"/></param>
        /// <param name="score"><see cref="MyEvents.Data.IRegisteredUserRepository"/></param>
        public void AddRegisteredUserScore(int registeredUserId, int sessionId, double score)
        {
            using (var context = new MyEventsContext())
            {
                var sessionRegisteredUser = context.SessionRegisteredUsers.FirstOrDefault(q => q.RegisteredUserId == registeredUserId && q.SessionId == sessionId);
                if (sessionRegisteredUser != null)
                {
                    sessionRegisteredUser.Rated = true;
                    sessionRegisteredUser.Score = score;
                    context.SaveChanges();
                }
            }
        }
    }
}
