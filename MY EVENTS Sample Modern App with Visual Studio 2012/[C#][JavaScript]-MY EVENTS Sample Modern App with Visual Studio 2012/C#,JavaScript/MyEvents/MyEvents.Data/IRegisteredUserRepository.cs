using System.Collections.Generic;
using MyEvents.Model;

namespace MyEvents.Data
{
    /// <summary>
    /// Repository to access to RegisteredUser entities
    /// </summary>
    public interface IRegisteredUserRepository
    {
        /// <summary>
        /// Get RegisteredUser Information
        /// </summary>
        /// <param name="facebookId">facebookId</param>
        /// <returns>RegisteredUser</returns>
        RegisteredUser Get(string facebookId);

        /// <summary>
        /// Get RegisteredUser Information
        /// </summary>
        /// <param name="registeredUserId">UserId</param>
        /// <returns>RegisteredUser</returns>
        RegisteredUser GetById(int registeredUserId);

        /// <summary>
        /// Get All RegisteredUsers By EventId
        /// </summary>
        /// <param name="eventDefinitionId">Event Id</param>
        /// <returns>List of RegisteredUsers</returns>
        IList<RegisteredUser> GetAllByEventId(int eventDefinitionId);

        /// <summary>
        /// Get if user is already registered
        /// </summary>
        /// <param name="eventDefinitionId">event id</param>
        /// <param name="registeredUserId">registered user id</param>
        /// <returns></returns>
        bool GetIfUserIsRegistered(int eventDefinitionId, int registeredUserId);

        /// <summary>
        /// Get All RegisteredUsers By SessionId
        /// </summary>
        /// <param name="sessionId">Session Id</param>
        /// <returns>List of RegisteredUsers</returns>
        IList<RegisteredUser> GetAllBySessionId(int sessionId);

        /// <summary>
        /// Get All events for the currennt RegisteredUserId
        /// </summary>
        /// <param name="registeredUserId"></param>
        /// <returns></returns>
        IList<EventDefinition> GetEventDefinitions(int registeredUserId);

        /// <summary>
        /// Get All sessions for the currennt RegisteredUserId
        /// </summary>
        /// <param name="eventDefinitionId"></param>
        /// <param name="registeredUserId"></param>
        /// <returns></returns>
        IList<Session> GetSessions(int eventDefinitionId, int registeredUserId);

        /// <summary>
        /// Add RegisteredUser
        /// </summary>
        /// <param name="registeredUser">registeredUser</param>
        /// <returns>registeredUser Id</returns>
        int Add(RegisteredUser registeredUser);

        /// <summary>
        ///  Asociate RegisteredUser with event
        /// </summary>
        /// <param name="registeredUserId">RegisteredUser Id</param>
        /// <param name="eventDefinitionId">eventDefinitionId</param>
        void AddRegisteredUserToEvent(int registeredUserId, int eventDefinitionId);

        /// <summary>
        /// Delete RegisteredUser from event
        /// </summary>
        /// <param name="registeredUserId">RegisteredUser Id</param>
        /// <param name="eventDefinitionId">eventDefinitionId</param>
        void DeleteRegisteredUserFromEvent(int registeredUserId, int eventDefinitionId);

        /// <summary>
        /// Asociate RegisteredUser with session
        /// </summary>
        /// <param name="registeredUserId">RegisteredUser Id</param>
        /// <param name="sessionId">Session Id</param>
        void AddRegisteredUserToSession(int registeredUserId, int sessionId);

        /// <summary>
        /// Delete RegisteredUser from session
        /// </summary>
        /// <param name="registeredUserId">RegisteredUser Id</param>
        /// <param name="sessionId">Session Id</param>
        void DeleteRegisteredUserFromSession(int registeredUserId, int sessionId);

        /// <summary>
        /// Update the score of the session
        /// </summary>
        /// <param name="registeredUserId">RegisteredUser Id</param>
        /// <param name="sessionId">Session Id</param>
        /// <param name="score">Score</param>
        void AddRegisteredUserScore(int registeredUserId, int sessionId, double score);

    }
}
