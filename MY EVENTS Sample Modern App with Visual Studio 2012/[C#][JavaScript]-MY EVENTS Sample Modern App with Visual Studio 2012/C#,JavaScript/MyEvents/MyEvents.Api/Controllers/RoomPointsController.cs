using System;
using System.Collections.Generic;
using System.Linq;
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
    /// RoomPoints Controller
    /// </summary>
    public class RoomPointsController : ApiController
    {
        private readonly IEventDefinitionRepository _eventDefinitionRepository = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventDefinitionRepository">IEventDefinitionRepository dependency</param>
        public RoomPointsController(IEventDefinitionRepository eventDefinitionRepository)
        {
            if (eventDefinitionRepository == null)
                throw new ArgumentNullException("eventDefinitionRepository");

            _eventDefinitionRepository = eventDefinitionRepository;
        }

        /// <summary>
        /// Get Room Image
        /// </summary>
        /// <param name="eventDefinitionId">eventDefinitionId</param>
        /// <returns>Image</returns>
        public byte[] GetRoomImage(int eventDefinitionId)
        {
            return _eventDefinitionRepository.GetRoomImage(eventDefinitionId);
        }


        /// <summary>
        /// Get Room Points of ALL event session
        /// </summary>
        /// <param name="id">eventDefinitionId</param>
        /// <returns>List of RoomPoint</returns>
        public IEnumerable<RoomPoint> GetAllRoomPoints(int id)
        {
            return _eventDefinitionRepository.GetAllRoomPoints(id);
        }


        /// <summary>
        /// Get Room Points of the session
        /// </summary>
        /// <param name="sessionId">SessionId</param>
        /// <returns>List of RoomPoint</returns>
        public IEnumerable<RoomPoint> GetRoomPoints(int sessionId)
        {
            return _eventDefinitionRepository.GetRoomPoints(sessionId);
        }

        /// <summary>
        /// Update Room Image
        /// </summary>
        /// <param name="eventDefinition">eventDefinition. Id + MapImage</param>
        [MyEvents.Api.Authentication.AuthorizeAttribute]
        public void PutRoomImage(EventDefinition eventDefinition)
        {
            ValidateEventAuthorization(eventDefinition.EventDefinitionId);

            _eventDefinitionRepository.UpdateRoomImage(eventDefinition);
        }


        /// <summary>
        /// Add room points. Boefe adding new points old ones are deleted
        /// </summary>
        /// <param name="roomPoints">Points of the room</param>
        [MyEvents.Api.Authentication.AuthorizeAttribute]
        public void PostRoomPoints(IEnumerable<RoomPoint> roomPoints)
        {
            foreach (var eventId in roomPoints.Select(r => r.EventDefinitionId))
                ValidateEventAuthorization(eventId);

            _eventDefinitionRepository.AddRoomPoints(roomPoints);
        }

        /// <summary>
        /// Delete room points
        /// </summary>
        /// <param name="eventDefinitionId">eventDefinitionId></param>
        /// <param name="roomNumber">roomNumber</param>
        [MyEvents.Api.Authentication.AuthorizeAttribute]
        public void DeleteRoomPoints(int eventDefinitionId, int roomNumber)
        {
            ValidateEventAuthorization(eventDefinitionId);

            _eventDefinitionRepository.DeleteRoomPoints(eventDefinitionId, roomNumber);
        }


        private void ValidateEventAuthorization(int eventDefinitionid)
        {
            var token = MyEventsToken.GetTokenFromHeader();
            var eventDefinition = _eventDefinitionRepository.GetById(eventDefinitionid);
            if (token.RegisteredUserId != eventDefinition.OrganizerId)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
        }
    }
}