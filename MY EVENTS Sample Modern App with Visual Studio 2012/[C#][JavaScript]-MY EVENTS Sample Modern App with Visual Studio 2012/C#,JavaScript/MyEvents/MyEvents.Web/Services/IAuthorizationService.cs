using MyEvents.Model;
using MyEvents.Web.Authentication;

namespace MyEvents.Web.Services
{
    /// <summary>
    /// Authorization service interface.
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Validates the event authorization.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="eventDefinitionid">The event definitionid.</param>
        void ValidateEventAuthorization(MyEventsIdentity identity, int eventDefinitionid);

        /// <summary>
        /// Validates the event authorization.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="eventDefinition">The event definition.</param>
        /// <exception cref="System.UnauthorizedAccessException"></exception>
        void ValidateEventAuthorization(MyEventsIdentity identity, EventDefinition eventDefinition);

        /// <summary>
        /// Validates the session authorization.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="sessionId">The session id.</param>
        void ValidateSessionAuthorization(MyEventsIdentity identity, int sessionId);

        /// <summary>
        /// Validates the session authorization.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="session">The session.</param>
        /// <exception cref="System.UnauthorizedAccessException"></exception>
        void ValidateSessionAuthorization(MyEventsIdentity identity, Session session);
    }
}