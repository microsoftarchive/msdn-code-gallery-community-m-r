using MyEvents.Api.Client;
using MyEvents.Client.Organizer.View;

namespace MyEvents.Client.Organizer.Services.Navigation
{
    /// <summary>
    /// INavigationService implementation.
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
        /// Navigate from Main page to event details.
        /// </summary>
        public void NavigateToEventDetails(EventDefinition eventDefinition)
        {
            App.RootFrame.Navigate(typeof(VEventDetail), eventDefinition);
        }

        /// <summary>
        /// Navigate from event details page to session details.
        /// </summary>
        public void NavigateToSessionDetails(Session session)
        {
            App.RootFrame.Navigate(typeof(VSessionDetail), session);
        }

        /// <summary>
        /// Navigate to main page.
        /// </summary>
        public void NavigateToMainPage()
        {
            App.RootFrame.Navigate(typeof(MainPage));
        }

        /// <summary>
        /// Navigate to splash screen to reset user.
        /// </summary>
        public void NavigateToSplashScreen()
        {
            App.RootFrame.Navigate(typeof(SplashScreen));
        }

        /// <summary>
        /// Navigate to previous visited page.
        /// </summary>
        public void NavigateBack()
        {
            if (App.RootFrame.CanGoBack)
                App.RootFrame.GoBack();
        }
    }
}
