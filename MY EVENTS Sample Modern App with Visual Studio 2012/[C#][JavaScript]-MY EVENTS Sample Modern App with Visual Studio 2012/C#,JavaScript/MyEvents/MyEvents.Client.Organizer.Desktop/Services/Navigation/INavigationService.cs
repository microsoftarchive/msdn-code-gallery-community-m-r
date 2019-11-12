using MyEvents.Api.Client;

namespace MyEvents.Client.Organizer.Desktop.Services.Navigation
{
    /// <summary>
    /// Navigation service contract
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Navigate to application main view.
        /// </summary>
        void NavigateToMain();

        /// <summary>
        /// Navigate to manage my events view.
        /// </summary>
        void NavigateToMyEvents();

        /// <summary>
        /// Navigate to an event details.
        /// </summary>
        /// <param name="eventDefinition">Selected event</param>
        void NavigateToEventDetails(EventDefinition eventDefinition);

        /// <summary>
        /// Navigate to an event schedule.
        /// </summary>
        /// <param name="eventDefinition">Selected event</param>
        void NavigateToEventSchedule(EventDefinition eventDefinition);

        /// <summary>
        /// Navigate to an event map definition.
        /// </summary>
        /// <param name="eventDefinition">Selected event</param>
        void NavigateToManageEventMap(EventDefinition eventDefinition);

        /// <summary>
        /// Navigate to an event session management.
        /// </summary>
        /// <param name="eventDefinition">Selected event</param>
        void NavigateToManageSessions(EventDefinition eventDefinition);

        /// <summary>
        /// Navigate to an event session management.
        /// </summary>
        /// <param name="eventDefinition">Selected event</param>
        void NavigateBackToSessionList(EventDefinition eventDefinition);

        /// <summary>
        /// Navigate to an event registered users.
        /// </summary>
        /// <param name="eventDefinition">Selected event</param>
        void NavigateToRegisteredUsers(EventDefinition eventDefinition);

        /// <summary>
        /// Navigate to last view.
        /// </summary>
        void NavigateBack();

       /// <summary>
       /// Navigate to edit the session details
       /// </summary>
       /// <param name="selectedSession"></param>
        void NavigateToEditSessionDetail(Session selectedSession);


        /// <summary>
        /// Navigate to event details in read only mode
        /// </summary>
        /// <param name="selectedSession"></param>
        void NavigateToEventDetailsReadOnly(Session selectedSession);

        /// <summary>
        /// Navigate to upload materials for the session view
        /// </summary>
        /// <param name="selectedSession"></param>
        void NavigateToUploadMaterials(Session selectedSession);

        /// <summary>
        /// Navigate to upload materials for the session view
        /// </summary>       
        void NavigateToSettings();
    }
}
