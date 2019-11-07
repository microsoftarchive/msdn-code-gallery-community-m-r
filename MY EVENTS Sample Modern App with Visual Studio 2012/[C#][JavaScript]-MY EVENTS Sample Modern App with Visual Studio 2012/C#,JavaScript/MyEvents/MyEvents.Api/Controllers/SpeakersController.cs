using System;
using System.Collections.Generic;
using System.Web.Http;
using MyEvents.Data;
using MyEvents.Model;

namespace MyEvents.Api.Controllers
{
    /// <summary>
    /// Controller to expose methods that returns information that client reports need
    /// </summary>
    public class SpeakersController : ApiController
    {
        private readonly IEventDefinitionRepository _eventDefinitionRepository = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventDefinitionRepository">IEventDefinitionRepository dependency</param>
        public SpeakersController(IEventDefinitionRepository eventDefinitionRepository)
        {
            if (eventDefinitionRepository ==  null)
                throw new ArgumentNullException("eventDefinitionRepository");

            _eventDefinitionRepository = eventDefinitionRepository;
        }

        /// <summary>
        /// Get Top speaker in all the events that are in My Events Platform
        /// </summary>
        /// <param name="id">Id of the organizer to get reports</param>
        /// <returns>List of speakers</returns>
        public IList<Speaker> GetTopSpeakers(int id)
        {
            return _eventDefinitionRepository.GetTopSpeakers(id);
        }
    }
}