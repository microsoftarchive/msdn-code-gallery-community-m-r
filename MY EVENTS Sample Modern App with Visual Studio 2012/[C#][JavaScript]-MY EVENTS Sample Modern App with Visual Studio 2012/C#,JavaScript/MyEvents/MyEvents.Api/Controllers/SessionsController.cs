using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MyEvents.Api.Authentication;
using MyEvents.Data;
using MyEvents.Model;


namespace MyEvents.Api.Controllers
{
    /// <summary>
    /// Sessions Controller
    /// </summary>
    public class SessionsController : ApiController
    {
        private readonly ISessionRepository _sessionRepository = null;
        private readonly IEventDefinitionRepository _eventDefinitionRepository = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sessionRepository">ISessionRepository dependency</param>
        /// <param name="eventDefinitionRepository">IEventDefinitionRepository dependency</param>
        public SessionsController(ISessionRepository sessionRepository, IEventDefinitionRepository eventDefinitionRepository)
        {
            if (sessionRepository == null)
                throw new ArgumentNullException("sessionRepository");

            if (eventDefinitionRepository == null)
                throw new ArgumentNullException("eventDefinitionRepository");

            _sessionRepository = sessionRepository;
            _eventDefinitionRepository = eventDefinitionRepository;
        }


        /// <summary>
        /// Get Session
        /// </summary>
        /// <param name="id">SessionId</param>
        /// <returns>Session</returns>
        public Session Get(int id)
        {
            var session = _sessionRepository.GetWithUserInfo(this.GetRegisteredUserId(), id);
            return session;
        }

        /// <summary>
        /// Get All Sessions
        /// </summary>
        /// <param name="eventDefinitionId">Event Definition Id</param>
        /// <returns>List of Sessions</returns>
        public IList<Session> GetAll(int eventDefinitionId)
        {
            return _sessionRepository.GetAllWithUserInfo(this.GetRegisteredUserId(), eventDefinitionId);
        }

        /// <summary>
        /// Add new session
        /// </summary>
        /// <param name="session">Session information</param>
        /// <returns>sessionId</returns>
        [MyEvents.Api.Authentication.AuthorizeAttribute]
        public int Post(Session session)
        {
            if (session == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));

            ValidateEventAuthorization(session.EventDefinitionId);

            return _sessionRepository.Add(session);
        }


        /// <summary>
        /// Update Session information
        /// </summary>
        /// <param name="session">Session information</param>
        [MyEvents.Api.Authentication.AuthorizeAttribute]
        public void Put(Session session)
        {
            if (session == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));

            ValidateEventAuthorization(session.EventDefinitionId);

            _sessionRepository.Update(session);
        }

        /// <summary>
        /// Delete Session
        /// </summary>
        /// <param name="id">Session Id</param>
        [MyEvents.Api.Authentication.AuthorizeAttribute]
        public void Delete(int id)
        {
            ValidateSessionAuthorization(id);
            _sessionRepository.Delete(id);
        }


        /// <summary>
        /// Update Session StartTime and Duration
        /// </summary>
        /// <param name="sessionId">SessionId</param>
        /// <param name="startTime">StartTime in UTC format</param>
        /// <param name="duration">Duration in minutes</param>
        [MyEvents.Api.Authentication.AuthorizeAttribute]
        public void PutSessionPeriod(int sessionId, string startTime, int duration)
        {
            DateTime dtStartTime = DateTime.Now;
            if (DateTime.TryParse(startTime, out dtStartTime))
            {
                if (duration <= 0)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));

                Session session = _sessionRepository.Get(sessionId);
                if (session != null)
                {
                    ValidateEventAuthorization(session.EventDefinitionId);

                    session.StartTime = dtStartTime;
                    session.Duration = duration;
                    _sessionRepository.Update(session);
                }
            }
        }


        private void ValidateEventAuthorization(int eventDefinitionid)
        {
            var token = MyEventsToken.GetTokenFromHeader();
            var eventDefinition = _eventDefinitionRepository.GetById(eventDefinitionid);
            if (token.RegisteredUserId != eventDefinition.OrganizerId)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
        }

        private void ValidateSessionAuthorization(int sessionId)
        {
            var session = _sessionRepository.Get(sessionId);
            ValidateEventAuthorization(session.EventDefinitionId);
        }

        private int GetRegisteredUserId()
        {
            var token = MyEventsToken.GetTokenFromHeader();
            if (token != null)
                return token.RegisteredUserId;

            return 0;
        }
    }
}
