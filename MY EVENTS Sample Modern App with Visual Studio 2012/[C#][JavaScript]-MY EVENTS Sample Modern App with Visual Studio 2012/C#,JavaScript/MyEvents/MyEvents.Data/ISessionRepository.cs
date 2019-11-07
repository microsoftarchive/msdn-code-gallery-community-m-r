using System.Collections.Generic;
using MyEvents.Model;

namespace MyEvents.Data
{
    /// <summary>
    /// Repository to access to Session entities
    /// </summary>
    public interface ISessionRepository
    {
        /// <summary>
        /// Get All Sessions
        /// </summary>
        /// <param name="eventDefinitionId">Event Definition Id</param>
        /// <returns>List of Sessions</returns>
        IList<Session> GetAll(int eventDefinitionId);

        /// <summary>
        /// Get All Sessions
        /// This method uses the userId to get more information, for example, if the user is registered as attendee
        /// </summary>
        /// <param name="registeredUserId">Id of the user that do the call</param>
        /// <param name="eventDefinitionId">Event Definition Id</param>
        /// <returns>List of Sessions</returns>
        IList<Session> GetAllWithUserInfo(int registeredUserId, int eventDefinitionId);

        /// <summary>
        /// Get Session
        /// </summary>
        /// <param name="sessionId">SessionId</param>
        /// <returns>Session</returns>
        Session Get(int sessionId);

        /// <summary>
        /// Get Session
        /// This method uses the userId to get more information, for example, if the user is registered as attendee
        /// </summary>
        /// <param name="registeredUserId">Id of the user that do the call</param>
        /// <param name="sessionId">SessionId</param>
        /// <returns>Session</returns>
        Session GetWithUserInfo(int registeredUserId, int sessionId);

        /// <summary>
        /// Add new session
        /// </summary>
        /// <param name="session">Session information</param>
        /// <returns>sessionId</returns>
        int Add(Session session);

        /// <summary>
        /// Update Session information
        /// </summary>
        /// <param name="session">Session information</param>
        void Update(Session session);

        /// <summary>
        /// Delete Session
        /// </summary>
        /// <param name="sessionId">Session Id</param>
        void Delete(int sessionId);

        /// <summary>
        /// Get the organizerId of the event
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        int GetOrganizerId(int sessionId);

    }
}
