using System;
using System.Collections.Generic;
using System.Net;

namespace MyEvents.Api.Client
{
    /// <summary>
    /// Interfaz to access to the RoomPoint Controller exposed by MyEvents.API
    /// </summary>
    public interface IRoomPointService
    {
        /// <summary>
        /// Get Room Image
        /// </summary>
        /// <param name="eventDefinitionId">eventDefinitionId</param>
        /// <param name="callback">CallBack func to get room image</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult GetRoomImageAsync(int eventDefinitionId, Action<byte[]> callback);

        /// <summary>
        /// Get Room Points of ALL the sessions
        /// </summary>
        /// <param name="eventDefinitionId">eventDefinitionId</param>
        /// <param name="callback">CallBack func to get room points</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult GetAllRoomPointsAsync(int eventDefinitionId, Action<IList<RoomPoint>> callback);

        /// <summary>
        /// Get Room Points of the session
        /// </summary>
        /// <param name="sessionId">sessionId</param>
        /// <param name="callback">CallBack func to get room points</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult GetRoomPointsAsync(int sessionId, Action<IList<RoomPoint>> callback);

        /// <summary>
        /// Update Room Image
        /// </summary>
        /// <param name="eventDefinitionId">eventDefinitionId</param>
        /// <param name="image">Image</param>
        /// <param name="callback">CallBack func to get action result</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult UpdateRoomImageAsync(int eventDefinitionId, byte[] image, Action<HttpStatusCode> callback);

        /// <summary>
        /// Add room points. Before adding new points old ones are deleted
        /// </summary>
        /// <param name="roomPoints">Points to add</param>
        /// <param name="callback">CallBack func to get action result</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult AddRoomPointsAsync(IEnumerable<RoomPoint> roomPoints, Action<HttpStatusCode> callback);

        /// <summary>
        /// Delete room points
        /// </summary>
        /// <param name="eventDefinitionId">eventDefinitionId</param>
        /// <param name="roomNumber">roomNumber</param>
        /// <param name="callback">CallBack func to get action result</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult DeleteRoomPointsAsync(int eventDefinitionId, int roomNumber, Action<HttpStatusCode> callback);
    }
}
