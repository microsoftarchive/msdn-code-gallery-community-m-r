using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Desktop.Views;

namespace MyEvents.Client.Organizer.Desktop.Services.Navigation
{
    /// <summary>
    /// Navigation service implementation.
    /// </summary>
    public class NavigationService : INavigationService
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public NavigationService()
        {

        }

        /// <summary>
        /// Navigate to application main view.
        /// </summary>
        public void NavigateToMain()
        {
            NavigationController.Instance.NavigateTo(new VMainDesktop());
        }

        /// <summary>
        /// Navigate to manage my events view.
        /// </summary>
        public void NavigateToMyEvents()
        {
            NavigationController.Instance.NavigateTo(new VManageEvents());
        }

        /// <summary>
        /// Navigate to an event details.
        /// </summary>
        /// <param name="eventDefinition">Selected event</param>
        public void NavigateToEventDetails(EventDefinition eventDefinition)
        {
            NavigationController.Instance.NavigateTo(new VEventDetails(eventDefinition));
        }

        /// <summary>
        /// Navigate to an event schedule.
        /// </summary>
        /// <param name="eventDefinition">Selected event</param>
        public void NavigateToEventSchedule(EventDefinition eventDefinition)
        {
            NavigationController.Instance.NavigateTo(new VEventSchedule(eventDefinition));
        }

        /// <summary>
        /// Navigate to an event map definition.
        /// </summary>
        /// <param name="eventDefinition">Selected event</param>
        public void NavigateToManageEventMap(EventDefinition eventDefinition)
        {
            NavigationController.Instance.NavigateTo(new VManageMap(eventDefinition));
        }

        /// <summary>
        /// Navigate to an event session management.
        /// </summary>
        /// <param name="eventDefinition">Selected event</param>
        public void NavigateToManageSessions(EventDefinition eventDefinition)
        {
            NavigationController.Instance.NavigateTo(new VManageSessions(eventDefinition));
        }

        /// <summary>
        /// Navigate to an event registered users.
        /// </summary>
        /// <param name="eventDefinition">Selected event</param>
        public void NavigateToRegisteredUsers(EventDefinition eventDefinition)
        {
            NavigationController.Instance.NavigateTo(new VRegisteredUsers(eventDefinition));
        }

        /// <summary>
        /// Navigate to last view.
        /// </summary>
        public void NavigateBack()
        {
            NavigationController.Instance.NavigateBack();
        }

        /// <summary>
        /// Navigate to an event session management.
        /// </summary>
        /// <param name="eventDefinition">Selected event</param>
        public void NavigateBackToSessionList(EventDefinition eventDefinition)
        {
            NavigationController.Instance.NavigateWithoutBackTo(new VManageSessions(eventDefinition));
        }

        /// <summary>
        /// Navigate to a session detail for edition
        /// </summary>
        /// <param name="selectedSession"></param>
        public void NavigateToEditSessionDetail(Session selectedSession)
        {
            NavigationController.Instance.NavigateTo(new VEditSessionDetails(selectedSession));
        }

        /// <summary>
        /// Navigate to event details in read only mode
        /// </summary>
        /// <param name="selectedSession"></param>
        public void NavigateToEventDetailsReadOnly(Session selectedSession)
        {
            NavigationController.Instance.NavigateTo(new VEditSessionDetailsReadOnly(selectedSession));
        }

        /// <summary>
        /// Navigate to upload materials for the session view
        /// </summary>
        /// <param name="selectedSession"></param>
        public void NavigateToUploadMaterials(Session selectedSession)
        {
            NavigationController.Instance.NavigateTo(new VMaterialUpload(selectedSession));
        }

        /// <summary>
        /// Navigate to upload materials for the session view
        /// </summary>     
        public void NavigateToSettings()
        {
            NavigationController.Instance.NavigateWithoutBackTo(new VSettings());
        }
    }
}
