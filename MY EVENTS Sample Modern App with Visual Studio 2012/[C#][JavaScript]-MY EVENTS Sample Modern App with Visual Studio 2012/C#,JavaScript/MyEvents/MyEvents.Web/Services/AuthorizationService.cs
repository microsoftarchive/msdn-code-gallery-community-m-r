using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyEvents.Model;
using MyEvents.Web.Authentication;
using MyEvents.Data;

namespace MyEvents.Web.Services
{
    /// <summary>
    /// Authorization service.
    /// </summary>
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IEventDefinitionRepository _eventDefinitionRepository;
        private readonly ISessionRepository _sessionRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationService" /> class.
        /// </summary>
        /// <param name="eventDefinitionRepository">The event definition repository.</param>
        /// <param name="sessionRepository">The session repository.</param>
        public AuthorizationService(IEventDefinitionRepository eventDefinitionRepository, ISessionRepository sessionRepository)
        {
            _eventDefinitionRepository = eventDefinitionRepository;
            _sessionRepository = sessionRepository;
        }

        /// <summary>
        /// Validates the event authorization.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="eventDefinitionid">The event definitionid.</param>
        public void ValidateEventAuthorization(MyEventsIdentity identity, int eventDefinitionid)
        {
            var eventDefinition = _eventDefinitionRepository.GetById(eventDefinitionid);
            ValidateEventAuthorization(identity, eventDefinition);
        }

        /// <summary>
        /// Validates the event authorization.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="eventDefinition">The event definition.</param>
        /// <exception cref="System.UnauthorizedAccessException"></exception>
        public void ValidateEventAuthorization(MyEventsIdentity identity, EventDefinition eventDefinition)
        {
            if (identity.UserId != eventDefinition.OrganizerId)
                throw new UnauthorizedAccessException();
        }

        /// <summary>
        /// Validates the session authorization.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="sessionId">The session id.</param>
        public void ValidateSessionAuthorization(MyEventsIdentity identity, int sessionId)
        {
            var session = _sessionRepository.Get(sessionId);
            ValidateEventAuthorization(identity, session.EventDefinitionId);
        }

        /// <summary>
        /// Validates the session authorization.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="session">The session.</param>
        /// <exception cref="System.UnauthorizedAccessException"></exception>
        public void ValidateSessionAuthorization(MyEventsIdentity identity, Session session)
        {
            var eventDefinition = _eventDefinitionRepository.GetById(session.EventDefinitionId);
            if (identity.UserId != eventDefinition.OrganizerId)
                throw new UnauthorizedAccessException();
        }
    }
}