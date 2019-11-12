using System.Collections.Generic;
using System.Linq;
using MyEvents.Model;

namespace MyEvents.Data
{
    /// <summary>
    /// <see cref="MyEvents.Data.ISessionRepository"/>
    /// </summary>
    public class SessionRepository : ISessionRepository
    {
        /// <summary>
        /// <see cref="MyEvents.Data.ISessionRepository"/>
        /// </summary>
        /// <param name="eventDefinitionId"><see cref="MyEvents.Data.ISessionRepository"/></param>
        /// <returns><see cref="MyEvents.Data.ISessionRepository"/></returns>
        public IList<Session> GetAll(int eventDefinitionId)
        {
            return GetAllWithUserInfo(0, eventDefinitionId);
        }

        /// <summary>
        /// <see cref="MyEvents.Data.ISessionRepository"/>
        /// </summary>
        /// <param name="registeredUserId"><see cref="MyEvents.Data.ISessionRepository"/></param>
        /// <param name="eventDefinitionId"><see cref="MyEvents.Data.ISessionRepository"/></param>
        /// <returns><see cref="MyEvents.Data.ISessionRepository"/></returns>
        public IList<Session> GetAllWithUserInfo(int registeredUserId, int eventDefinitionId)
        {
            using (var context = new MyEventsContext())
            {
                return context.Sessions.Include("SessionRegisteredUsers")
                    .Where(q => q.EventDefinitionId == eventDefinitionId)
                    .ToList()
                    .Select(q => new Session()
                    {
                        SessionId = q.SessionId, 
                        Title = q.Title,
                        Description = q.Description,
                        Speaker = q.Speaker, 
                        Biography = q.Biography,
                        StartTime = q.StartTime,
                        Duration = q.Duration, 
                        RoomNumber = q.RoomNumber, 
                        AttendeesCount = q.SessionRegisteredUsers.Count(), 
                        TwitterAccount = q.TwitterAccount,
                        TimeZoneOffset = q.TimeZoneOffset,
                        EventDefinitionId = q.EventDefinitionId,
                        IsFavorite = q.SessionRegisteredUsers.Any(sr => sr.RegisteredUserId == registeredUserId),
                        UserScore = q.SessionRegisteredUsers.Any(sr => sr.RegisteredUserId == registeredUserId) ?
                                q.SessionRegisteredUsers.First(sr => sr.RegisteredUserId == registeredUserId).Score : 0,
                        Score = q.SessionRegisteredUsers.Any(sr => sr.Rated) ?
                                q.SessionRegisteredUsers.Where(sr => sr.Rated).Average(sr => sr.Score) : 0
                    }).ToList();
            }
        }

         /// <summary>
        /// <see cref="MyEvents.Data.ISessionRepository"/>
        /// </summary>
        /// <param name="sessionId"><see cref="MyEvents.Data.ISessionRepository"/></param>
        /// <returns><see cref="MyEvents.Data.ISessionRepository"/></returns>
        public Session Get(int sessionId)
        {
            return GetWithUserInfo(0, sessionId);
        }

        /// <summary>
        /// <see cref="MyEvents.Data.ISessionRepository"/>
        /// </summary>
        /// <param name="registeredUserId"><see cref="MyEvents.Data.ISessionRepository"/></param>
        /// <param name="sessionId"><see cref="MyEvents.Data.ISessionRepository"/></param>
        /// <returns><see cref="MyEvents.Data.ISessionRepository"/></returns>
        public Session GetWithUserInfo(int registeredUserId, int sessionId)
        {
            using (var context = new MyEventsContext())
            {
                return context.Sessions.Include("SessionRegisteredUsers")
                    .Include("Materials")
                    .Where(q => q.SessionId == sessionId)
                    .ToList()
                    .Select(q => new Session()
                    {
                        SessionId = q.SessionId,
                        Title = q.Title,
                        Description = q.Description,
                        Speaker = q.Speaker,
                        Biography = q.Biography,
                        StartTime = q.StartTime,
                        Duration = q.Duration,
                        RoomNumber = q.RoomNumber,
                        AttendeesCount = q.SessionRegisteredUsers.Count(),
                        TimeZoneOffset = q.TimeZoneOffset,
                        TwitterAccount = q.TwitterAccount,
                        EventDefinitionId = q.EventDefinitionId,
                        Comments = q.Comments,
                        SessionRegisteredUsers = q.SessionRegisteredUsers,
                        IsFavorite = q.SessionRegisteredUsers.Any(sr => sr.RegisteredUserId == registeredUserId),
                        UserScore = q.SessionRegisteredUsers.Any(sr => sr.RegisteredUserId == registeredUserId) ? 
                                        q.SessionRegisteredUsers.First(sr => sr.RegisteredUserId == registeredUserId).Score : 0,
                        Score = q.SessionRegisteredUsers.Any(sr => sr.Rated) ?
                                q.SessionRegisteredUsers.Where(sr => sr.Rated).Average(sr => sr.Score) : 0,
                        Materials = q.Materials.Select(m => new Material()
                                                                 {
                                                                     MaterialId = m.MaterialId,
                                                                     Name = m.Name
                                                                 }).ToList()
                    })
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.ISessionRepository"/>
        /// </summary>
        /// <param name="session"><see cref="MyEvents.Data.ISessionRepository"/></param>
        /// <returns><see cref="MyEvents.Data.ISessionRepository"/></returns>
        public int Add(Session session)
        {
            using (var context = new MyEventsContext())
            {
                CheckAndFixTwitterAccount(session);
                context.Sessions.Add(session);
                context.SaveChanges();
                return session.SessionId;
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.ISessionRepository"/>
        /// </summary>
        /// <param name="session"><see cref="MyEvents.Data.ISessionRepository"/></param>
        public void Update(Session session)
        {
            CheckAndFixTwitterAccount(session);

            using (var context = new MyEventsContext())
            {
                var sessionToUpdate = context.Sessions.FirstOrDefault(q => q.SessionId == session.SessionId);

                context.Entry<Session>(sessionToUpdate)
                   .CurrentValues
                   .SetValues(session);

                context.SaveChanges();
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.ISessionRepository"/>
        /// </summary>
        /// <param name="sessionId"><see cref="MyEvents.Data.ISessionRepository"/></param>
        public void Delete(int sessionId)
        {
            using (var context = new MyEventsContext())
            {
                var session = context.Sessions.FirstOrDefault(q => q.SessionId == sessionId);
                if (session != null)
                {
                    context.Sessions.Remove(session);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// <see cref="MyEvents.Data.ISessionRepository"/>
        /// </summary>
        /// <param name="sessionId"><see cref="MyEvents.Data.ISessionRepository"/></param>
        /// <returns><see cref="MyEvents.Data.ISessionRepository"/></returns>
        public int GetOrganizerId(int sessionId)
        {
            int id = 0;
            using (var context = new MyEventsContext())
            {
                var session = context.Sessions.Include("EventDefinition")
                    .FirstOrDefault(s => s.SessionId == sessionId);
                if (session != null)
                {
                    id = session.EventDefinition.OrganizerId;
                }
            }
            return id;
        }

        private void CheckAndFixTwitterAccount(Session session)
        {
            if (!session.TwitterAccount.StartsWith("@"))
                session.TwitterAccount = string.Format("@{0}", session.TwitterAccount);
        }

    }
}
