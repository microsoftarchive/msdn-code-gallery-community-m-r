using System;
using System.Collections.Generic;
using System.Net;

namespace MyEvents.Api.Client
{
    /// <summary>
    /// Class to access to the Session Controller exposed by MyEvents.API
    /// </summary>
    public interface ISessionService
    {
        /// <summary>
        /// Get All Sessions
        /// </summary>
        /// <param name="eventDefinitionId">Event Definition Id</param>
        /// <param name="callback">CallBack func to get list of sessions</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult GetAllSessionsAsync(int eventDefinitionId, Action<IList<Session>> callback);

        /// <summary>
        /// Get Session
        /// </summary>
        /// <param name="sessionId">SessionId</param>
        /// <param name="callback">CallBack func to get session</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult GetSessionAsync(int sessionId, Action<Session> callback);

        /// <summary>
        /// Add new session
        /// </summary>
        /// <param name="session">Session information</param>
        /// <param name="callback">CallBack func to get session Id</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult AddSessionAsync(Session session, Action<int> callback);

        /// <summary>
        /// Update Session information
        /// </summary>
        /// <param name="session">Session information</param>
        /// <param name="callback">CallBack func to get action result</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult UpdateSessionAsync(Session session, Action<HttpStatusCode> callback);
        
        /// <summary>
        /// Delete Session
        /// </summary>
        /// <param name="sessionId">Session Id</param>
        /// <param name="callback">CallBack func to get action result</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult DeleteSessionAsync(int sessionId, Action<HttpStatusCode> callback);

        /// <summary>
        /// Update Session StartTime and Duration
        /// </summary>
        /// <param name="sessionId">SessionId</param>
        /// <param name="startTime">StartTime in UTC format</param>
        /// <param name="duration">Duration in minutes</param>
        /// <param name="callback">CallBack func to get action result</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult UpdateSessionPeriodAsync(int sessionId, string startTime, int duration, Action<HttpStatusCode> callback);
    }
}
