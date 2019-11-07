using System;
using System.Collections.Generic;
using System.Net;

namespace MyEvents.Api.Client
{
    /// <summary>
    /// Interfaz to access to the Event Definition Controller exposed by MyEvents.API
    /// </summary>
    public interface IEventDefinitionService
    {
        /// <summary>
        /// Get Events Count
        /// </summary>
        /// <param name="callback">CallBack func to get event count</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult GetEventDefinitionCountAsync(Action<int> callback);

        /// <summary>
        /// Get All Events in Async Mode. 
        /// </summary>
        /// <param name="pageSize">Number of results to get</param>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="callback">CallBack func to get list of event definitions</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult GetAllEventDefinitionsAsync(int pageSize, int pageIndex, Action<IList<EventDefinition>> callback);


        /// <summary>
        /// Get last number of event definitions
        /// </summary>
        /// <param name="number">Number of event defintions to get</param>
        /// <param name="callback">CallBack func to get list of event definitions</param>
        /// <returns></returns>
        IAsyncResult GetLastEventDefinitionsAsync(int number, Action<IList<EventDefinition>> callback);

        /// <summary>
        /// Get the event that happens today ordered by name
        /// </summary>
        /// <param name="callback">CallBack func to get list of event definitions</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult GetCurrentEventDefinitionsAsync(Action<IList<EventDefinition>> callback);

        /// <summary>
        /// Get Events count filtered by OrganizerId and title
        /// </summary>
        /// <param name="organizerId">Organizer Id</param>
        /// <param name="filter">filter applied to search</param>
        /// <param name="callback">CallBack func to get event count</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult GetEventDefinitionCountByOrganizerIdAsync(int organizerId, string filter, Action<int> callback);

        /// <summary>
        /// Get All Events filtered by OrganizerId and title
        /// </summary>
        /// <param name="organizerId">Organizer Id</param>
        /// <param name="filter">filter applied to search</param>
        /// <param name="pageSize">Number of results to get</param>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="callback">CallBack func to get list of event definitions</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult GetEventDefinitionByOrganizerIdAsync(int organizerId, string filter, int pageSize, int pageIndex, Action<IList<EventDefinition>> callback);

        /// <summary>
        /// Get Event Definition By Id
        /// </summary>
        /// <param name="eventDefinitionId">eventDefinition Id</param>
        /// <param name="callback">CallBack func to get EventDefinition</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult GetEventDefinitionByIdAsync(int eventDefinitionId, Action<EventDefinition> callback);

        /// <summary>
        /// Add new event definition
        /// </summary>
        /// <param name="eventDefinition">Event definition</param>
        /// <param name="callback">CallBack func to get new Id</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult AddEventDefinitionAsync(EventDefinition eventDefinition, Action<int> callback);

        /// <summary>
        /// Update event definition
        /// </summary>
        /// <param name="eventDefinition">Event definition</param>
        /// <param name="callback">CallBack func to get action result</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult UpdateEventDefinitionAsync(EventDefinition eventDefinition, Action<HttpStatusCode> callback);

        /// <summary>
        /// Delete Event
        /// </summary>
        /// <param name="eventDefinitionId">eventDefinition</param>
        /// <param name="callback">CallBack func to get action result</param>
        /// <returns>IAsyncResult</returns>
        IAsyncResult DeleteEventDefinitionAsync(int eventDefinitionId, Action<HttpStatusCode> callback);

    }
}
