using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using MyEvents.Api.Authentication;
using MyEvents.Api.Mappers;
using MyEvents.Api.Models;
using MyEvents.Data;
using MyEvents.Model;

namespace MyEvents.Api.Controllers
{
    /// <summary>
    /// EventDefinitions Controller
    /// </summary>
    public class EventDefinitionsController : ApiController
    {
        private readonly IEventDefinitionRepository _eventDefinitionRepository = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventDefinitionRepository">IEventDefinitionRepository dependency</param>
        public EventDefinitionsController(IEventDefinitionRepository eventDefinitionRepository)
        {
            if (eventDefinitionRepository ==  null)
                throw new ArgumentNullException("eventDefinitionRepository");

            _eventDefinitionRepository = eventDefinitionRepository;
        }

        /// <summary>
        /// Get Events Count
        /// </summary>
        /// <returns>Event Count</returns>
        public int GetEventDefinitionCount()
        {
            return _eventDefinitionRepository.GetCount();
        }

        /// <summary>
        /// Get All Events
        /// </summary>
        /// <param name="pageSize">Number of results to get</param>
        /// <param name="pageIndex">Page Index</param>
        /// <returns>List of EventDefinitions</returns>
        public IList<EventDefinition> GetAllEventDefinitions(int pageSize, int pageIndex)
        {
            return _eventDefinitionRepository.GetAllWithUserInfo(this.GetRegisteredUserId(), pageSize, pageIndex);
        }

        /// <summary>
        /// Get the last X events
        /// </summary>
        /// <param name="number">Number of results to get</param>
        /// <returns>List of EventDefinitions</returns>
        public IList<EventDefinition> GetLastEventDefinitions(int number)
        {
            return _eventDefinitionRepository.GetLastWithUserInfo(this.GetRegisteredUserId(), number);
        }

        /// <summary>
        /// Get the event that happens today ordered by name
        /// </summary>
        /// <returns>EventDefinition List with Id and Name</returns>
        public IList<EventDefinition> GetCurrentEventDefinitions()
        {
            return _eventDefinitionRepository.GetCurrent();
        }

        /// <summary>
        /// Get Events count filtered by OrganizerId and title
        /// </summary>
        /// <param name="organizerId">Organizer Id</param>
        /// <param name="filter">filter applied to search</param>
        /// <returns>Event Count</returns>
        public int GetEventDefinitionCountByOrganizerId(int organizerId, string filter)
        {
            return _eventDefinitionRepository.GetCountByOrganizerId(organizerId, filter);
        }

        /// <summary>
        /// Get All Events filtered by OrganizerId and title
        /// </summary>
        /// <param name="organizerId">Organizer Id</param>
        /// <param name="filter">filter applied to search</param>
        /// <param name="pageSize">Number of results to get</param>
        /// <param name="pageIndex">Page Index</param>
        /// <returns>List of EventDefinitions</returns>
        public IList<EventDefinition> GetEventDefinitionByOrganizerId(int organizerId, string filter, int pageSize, int pageIndex)
        {
            return _eventDefinitionRepository.GetByOrganizerId(organizerId, filter, pageSize, pageIndex, false);
        }

        /// <summary>
        /// Get Event Definition By Id
        /// </summary>
        /// <param name="id">eventDefinition Id</param>
        /// <returns></returns>
        public EventDefinition GetEventDefinitionById(int id)
        {
            return _eventDefinitionRepository.GetByIdWithUserInfo(this.GetRegisteredUserId(), id);
        }

        /// <summary>
        /// Add new event definition
        /// </summary>
        /// <param name="eventDefinition">Event definition</param>
        /// <returns>eventDefinitionId</returns>
        [MyEvents.Api.Authentication.AuthorizeAttribute]
        public int Post(EventDefinition eventDefinition)
        {
            if (eventDefinition == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));

            var token = MyEventsToken.GetTokenFromHeader();
            if (token.RegisteredUserId != eventDefinition.OrganizerId)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));

            return _eventDefinitionRepository.Add(eventDefinition);
        }

        /// <summary>
        /// Update event definition
        /// </summary>
        /// <param name="eventDefinition">Event definition</param>
        [MyEvents.Api.Authentication.AuthorizeAttribute]
        public void Put(EventDefinition eventDefinition)
        {
            if (eventDefinition == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));

            var token = MyEventsToken.GetTokenFromHeader();
            if (token.RegisteredUserId != eventDefinition.OrganizerId)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));

            _eventDefinitionRepository.Update(eventDefinition);
        }

        /// <summary>
        /// Delete Event
        /// </summary>
        /// <param name="id">eventDefinition Id</param>
        [MyEvents.Api.Authentication.AuthorizeAttribute]
        public void Delete(int id)
        {
            var token = MyEventsToken.GetTokenFromHeader();

            ValidateEventAuthorization(id);

            _eventDefinitionRepository.Delete(id);
        }

        /// <summary>
        /// Get Schedule Information
        /// </summary>
        /// <param name="eventDefinitionId"></param>
        /// <returns></returns>
        public Schedule GetScheduleInformation(int eventDefinitionId)
        {
            var eventDefinition = _eventDefinitionRepository.GetById(eventDefinitionId);
            var mapper = new EventDefinitionToScheduleViewModelMapper();
            var scheduleInformation = mapper.Map(this.GetRegisteredUserId(), eventDefinition);
            return scheduleInformation;
        }

        private void ValidateEventAuthorization(int eventDefinitionid)
        {
            var token = MyEventsToken.GetTokenFromHeader();
            var eventDefinition = _eventDefinitionRepository.GetById(eventDefinitionid);
            if (token.RegisteredUserId != eventDefinition.OrganizerId)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
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
