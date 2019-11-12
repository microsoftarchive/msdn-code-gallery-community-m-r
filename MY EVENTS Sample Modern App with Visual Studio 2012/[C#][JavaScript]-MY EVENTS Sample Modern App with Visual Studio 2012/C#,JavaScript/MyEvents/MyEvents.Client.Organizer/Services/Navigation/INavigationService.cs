using MyEvents.Api.Client;

namespace MyEvents.Client.Organizer.Services.Navigation
{
    /// <summary>
    /// Interface for application page navigation.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Navigate from Main page to event details.
        /// </summary>
        void NavigateToEventDetails(EventDefinition eventDefinition);

        /// <summary>
        /// Navigate from event details page to session details.
        /// </summary>
        void NavigateToSessionDetails(Session session);

        /// <summary>
        /// Navigate to main page.
        /// </summary>
        void NavigateToMainPage();

        /// <summary>
        /// Navigate to previous visited page.
        /// </summary>
        void NavigateBack();

        /// <summary>
        /// Navigate to splash screen to reset user.
        /// </summary>
        void NavigateToSplashScreen();
    }
}
