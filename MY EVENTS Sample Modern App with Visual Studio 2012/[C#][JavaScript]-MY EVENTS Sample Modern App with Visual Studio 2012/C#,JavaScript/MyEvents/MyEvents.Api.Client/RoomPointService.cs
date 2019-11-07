using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using MyEvents.Api.Client.Web;

namespace MyEvents.Api.Client
{

    /// <summary>
    /// <see cref="MyEvents.Api.Client.IRoomPointService"/>
    /// </summary>
    internal class RoomPointService : BaseRequest, IRoomPointService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        /// <param name="authenticationToken">Authentication Token</param>
        public RoomPointService(string urlPrefix, string authenticationToken)
            : base(urlPrefix, authenticationToken)
        {

        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IRoomPointService"/>
        /// </summary>
        /// <param name="eventDefinitionId"><see cref="MyEvents.Api.Client.IRoomPointService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IRoomPointService"/></param>
        /// <returns></returns>
        public IAsyncResult GetRoomImageAsync(int eventDefinitionId, Action<byte[]> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                    , "{0}api/roompoints?eventDefinitionId={1}", _urlPrefix, eventDefinitionId);

            return base.DoGet(url, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IRoomPointService"/>
        /// </summary>
        /// <param name="eventDefinitionId"><see cref="MyEvents.Api.Client.IRoomPointService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IRoomPointService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IRoomPointService"/></returns>
        public IAsyncResult GetAllRoomPointsAsync(int eventDefinitionId, Action<IList<RoomPoint>> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                    , "{0}api/roompoints/{1}", _urlPrefix, eventDefinitionId);

            return base.DoGet(url, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IRoomPointService"/>
        /// </summary>
        /// <param name="sessionId"><see cref="MyEvents.Api.Client.IRoomPointService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IRoomPointService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IRoomPointService"/></returns>
        public IAsyncResult GetRoomPointsAsync(int sessionId, Action<IList<RoomPoint>> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                    , "{0}api/roompoints?sessionId={1}", _urlPrefix, sessionId);

            return base.DoGet(url, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IRoomPointService"/>
        /// </summary>
        /// <param name="eventDefinitionId"><see cref="MyEvents.Api.Client.IRoomPointService"/></param>
        /// <param name="image"><see cref="MyEvents.Api.Client.IRoomPointService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IRoomPointService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IRoomPointService"/></returns>
        public IAsyncResult UpdateRoomImageAsync(int eventDefinitionId, byte[] image, Action<HttpStatusCode> callback)
        {
             string url = String.Format(CultureInfo.InvariantCulture
                    , "{0}api/roompoints", _urlPrefix);

             Client.EventDefinition eventDefinition = new EventDefinition()
             {
                 EventDefinitionId = eventDefinitionId,
                 MapImage = image
             };

             return base.DoPut(url, eventDefinition, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IRoomPointService"/>
        /// </summary>
        /// <param name="roomPoints"><see cref="MyEvents.Api.Client.IRoomPointService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IRoomPointService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IRoomPointService"/></returns>
        public IAsyncResult AddRoomPointsAsync(IEnumerable<RoomPoint> roomPoints, Action<HttpStatusCode> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                    , "{0}api/roompoints", _urlPrefix);

            return base.DoPost(url, roomPoints, callback);
        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IRoomPointService"/>
        /// </summary>
        /// <param name="eventDefinitionId"><see cref="MyEvents.Api.Client.IRoomPointService"/></param>
        /// <param name="roomNumber"><see cref="MyEvents.Api.Client.IRoomPointService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IRoomPointService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IRoomPointService"/></returns>
        public IAsyncResult DeleteRoomPointsAsync(int eventDefinitionId, int roomNumber, Action<HttpStatusCode> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                    , "{0}api/roompoints?eventDefinitionId={1}&roomNumber={2}", _urlPrefix, eventDefinitionId, roomNumber);

            return base.DoDelete(url, eventDefinitionId, callback);
        }
    }
}
