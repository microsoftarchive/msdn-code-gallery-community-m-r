using System;
using System.Collections.Generic;
using System.Net;

namespace MyEvents.Api.Client
{
    /// <summary>
    /// Class to access to the Registered User Controller exposed by MyEvents.API
    /// </summary>
    public interface IRegisteredUserService
    {
        /// <summary>
        /// Get RegisteredUser Information
        /// </summary>
        /// <param name="facebookId">facebookId</param>
        /// <param name="callback">CallBack func to RegisteredUser</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult GetRegisteredUserAsync(string facebookId, Action<RegisteredUser> callback);

        /// <summary>
        /// Get All RegisteredUsers By EventId. 
        /// </summary>
        /// <param name="id">Event Definition Id</param>
        /// <param name="callback">CallBack func to get list of registered users</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult GetAllRegisteredUsersByEventIdAsync(int id, Action<IList<RegisteredUser>> callback);

        /// <summary>
        /// Get All RegisteredUsers By SessionId. Re
        /// </summary>
        /// <param name="id">Session Id</param>
        /// <param name="callback">CallBack func to get list of registered users</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult GetAllRegisteredUsersBySessionIdAsync(int id, Action<IList<RegisteredUser>> callback);

        /// <summary>
        /// Get All events for the currennt RegisteredUserId
        /// </summary>
        /// <param name="registeredUserId"></param>
        /// <param name="callback">CallBack func to get list of event definitions</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult GetRegisteredUserEventDefinitionsAsync(int registeredUserId, Action<IList<EventDefinition>> callback);

        /// <summary>
        /// Add RegisteredUser
        /// </summary>
        /// <param name="registeredUser">registeredUser information</param>
        /// <param name="callback">CallBack func to get new id</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult AddRegisteredUserCreateIfNotExistsAsync(RegisteredUser registeredUser, Action<int> callback);

        /// <summary>
        /// Asociate RegisteredUser with event
        /// </summary>
        /// <param name="registeredUserId">RegisteredUser Id</param>
        /// <param name="eventDefinitionId">Event Id</param>
        /// <param name="callback">CallBack func to get action result</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult AddRegisteredUserToEventAsync(int registeredUserId, int eventDefinitionId, Action<HttpStatusCode> callback);

        /// <summary>
        /// Delete RegisteredUser from event
        /// </summary>
        /// <param name="registeredUserId">RegisteredUser Id</param>
        /// <param name="eventDefinitionId">Event Id</param>
        /// <param name="callback">CallBack func to get action result</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult DeleteRegisteredUserFromEventAsync(int registeredUserId, int eventDefinitionId, Action<HttpStatusCode> callback);

        /// <summary>
        /// Asociate RegisteredUser with session
        /// </summary>
        /// <param name="registeredUserId">RegisteredUser Id</param>
        /// <param name="sessionId">Session Id</param>
        /// <param name="callback">CallBack func to get action result</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult AddRegisteredUserToSessionAsync(int registeredUserId, int sessionId, Action<HttpStatusCode> callback);

        /// <summary>
        /// Asociate RegisteredUser with session
        /// </summary>
        /// <param name="registeredUserId">RegisteredUser Id</param>
        /// <param name="sessionId">Session Id</param>
        /// <param name="score">Score</param>
        /// <param name="callback">CallBack func to get action result</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult AddRegisteredUserScoreAsync(int registeredUserId, int sessionId, double score, Action<HttpStatusCode> callback);


        /// <summary>
        /// Delete RegisteredUser from session
        /// </summary>
        /// <param name="registeredUserId">RegisteredUser Id</param>
        /// <param name="sessionId">Session Id</param>
        /// <param name="callback">CallBack func to get action result</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult DeleteRegisteredUserFromSessionAsync(int registeredUserId, int sessionId, Action<HttpStatusCode> callback);

        /// <summary>
        /// Get Fake User to used it when the clients are in offline mode.
        /// </summary>
        /// <param name="callback">CallBack func to get RegisteredUser</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult GetFakeRegisteredUserAsync(Action<RegisteredUser> callback);
    }
}
