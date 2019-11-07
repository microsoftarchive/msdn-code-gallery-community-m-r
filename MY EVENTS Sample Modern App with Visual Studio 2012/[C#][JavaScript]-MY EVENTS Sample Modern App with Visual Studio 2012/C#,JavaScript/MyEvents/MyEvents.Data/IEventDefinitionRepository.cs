using System.Collections.Generic;
using MyEvents.Model;

namespace MyEvents.Data
{
    /// <summary>
    /// Repository to access to Event Definition entities
    /// </summary>
    public interface IEventDefinitionRepository
    {
        /// <summary>
        /// Get Events Count
        /// </summary>
        /// <returns>Event Count</returns>
        int GetCount();

        /// <summary>
        /// Get All Events
        /// </summary>
        /// <param name="pageSize">Number of results to get</param>
        /// <param name="pageIndex">Page Index</param>
        /// <returns>List of EventDefinitions</returns>
        IList<EventDefinition> GetAll(int pageSize, int pageIndex);

        /// <summary>
        /// Get All Events.
        /// This method uses the userId to get more information, for example, if the user is registered as attendee
        /// </summary>
        /// <param name="registeredUserId">Id of the user that do the call</param>
        /// <param name="pageSize">Number of results to get</param>
        /// <param name="pageIndex">Page Index</param>
        /// <returns>List of EventDefinitions</returns>
        IList<EventDefinition> GetAllWithUserInfo(int registeredUserId, int pageSize, int pageIndex);

        /// <summary>
        /// Get the last X events
        /// </summary>
        /// <param name="number">Number of results to get</param>
        /// <returns>List of EventDefinitions</returns>
        IList<EventDefinition> GetLast(int number);


        /// <summary>
        /// Get the last "number" events
        /// This method uses the userId to get more information, for example, if the user is registered as attendee
        /// </summary>
        /// <param name="registeredUserId">Id of the user that do the call</param>
        /// <param name="number">Number of results to get</param>
        /// <returns>List of EventDefinitions</returns>
        IList<EventDefinition> GetLastWithUserInfo(int registeredUserId, int number);

        /// <summary>
        /// Get the event that happens today ordered by name
        /// </summary>
        /// <returns>EventDefinition List with Id and Name</returns>
        IList<EventDefinition> GetCurrent();

        /// <summary>
        /// Get Events count filtered by OrganizerId and title
        /// </summary>
        /// <param name="organizerId">Organizer Id</param>
        /// <param name="filter">filter applied to search</param>
        /// <returns>Event Count</returns>
        int GetCountByOrganizerId(int organizerId, string filter);

        /// <summary>
        /// Get All Events filtered by OrganizerId and title
        /// </summary>
        /// <param name="organizerId">Organizer Id</param>
        /// <param name="filter">filter applied to search</param>
        /// <param name="pageSize">Number of results to get</param>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="completeInfo">true is the method must return complete information about the event</param>
        /// <returns>List of EventDefinitions</returns>
        IList<EventDefinition> GetByOrganizerId(int organizerId, string filter, int pageSize, int pageIndex, bool completeInfo);

        /// <summary>
        /// Get Event Definition By Id
        /// </summary>
        /// <param name="eventDefinitionId">eventDefinition Id</param>
        /// <returns></returns>
        EventDefinition GetById(int eventDefinitionId);


        /// <summary>
        /// Get Event Definition By Id
        /// This method uses the userId to get more information, for example, if the user is registered as attendee
        /// </summary>
        /// <param name="registeredUserId">Id of the user that do the call</param>
        /// <param name="eventDefinitionId">eventDefinition Id</param>
        /// <returns></returns>
        EventDefinition GetByIdWithUserInfo(int registeredUserId, int eventDefinitionId);

        /// <summary>
        /// Get the event logo
        /// </summary>
        /// <param name="eventDefinitionId">eventDefinition Id</param>
        /// <returns></returns>
        byte[] GetEventLogo(int eventDefinitionId);

        /// <summary>
        /// Add new event definition
        /// </summary>
        /// <param name="eventDefinition">Event definition</param>
        /// <returns>eventDefinitionId</returns>
        int Add(EventDefinition eventDefinition);

        /// <summary>
        /// Update event definition
        /// </summary>
        /// <param name="eventDefinition">Event definition</param>
        void Update(EventDefinition eventDefinition);

        /// <summary>
        /// Delete Event
        /// </summary>
        /// <param name="eventDefinitionId">eventDefinition to delete</param>
        void Delete(int eventDefinitionId);

        /// <summary>
        /// Get Top tags used in all the events that are in My Events Platform
        /// </summary>
        /// <param name="organizerId">Organizer Id</param>
        /// <returns></returns>
        IList<Tag> GetTopTags(int organizerId);
        
        /// <summary>
        /// Get Top speaker in all the events that are in My Events Platform
        /// </summary>
        /// <param name="organizerId">Organizer Id</param>
        /// <returns></returns>
        IList<Speaker> GetTopSpeakers(int organizerId);

        /// <summary>
        /// Get Room Image
        /// </summary>
        /// <param name="eventDefinitionId">eventDefinitionId</param>
        /// <returns>Image</returns>
        byte[] GetRoomImage(int eventDefinitionId);

        /// <summary>
        /// Get Room Points of ALL the sessions
        /// </summary>
        /// <param name="eventDefinitionId">eventDefinitionId</param>
        /// <returns>List of RoomPoint</returns>
        IList<RoomPoint> GetAllRoomPoints(int eventDefinitionId);

        /// <summary>
        /// Get Room Points of the session
        /// </summary>
        /// <param name="sessionId">SessionId</param>
        /// <returns>List of RoomPoint</returns>
        IList<RoomPoint> GetRoomPoints(int sessionId);

        /// <summary>
        /// Update Room Image
        /// </summary>
        /// <param name="eventDefinition">eventDefinition. </param>
        void UpdateRoomImage(EventDefinition eventDefinition);


        /// <summary>
        /// Add room points. Boefe adding new points old ones are deleted
        /// </summary>
        /// <param name="roomPoints">Points of the room</param>
        void AddRoomPoints(IEnumerable<RoomPoint> roomPoints);

        /// <summary>
        /// Delete room points
        /// </summary>
        /// <param name="eventDefinitionId">eventDefinitionId></param>
        /// <param name="roomNumber">roomNumber</param>
        void DeleteRoomPoints(int eventDefinitionId, int roomNumber);
    }
}
