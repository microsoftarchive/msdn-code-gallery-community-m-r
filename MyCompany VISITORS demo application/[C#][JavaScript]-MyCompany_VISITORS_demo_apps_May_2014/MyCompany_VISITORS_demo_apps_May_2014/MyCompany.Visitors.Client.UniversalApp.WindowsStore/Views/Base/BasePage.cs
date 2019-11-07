namespace MyCompany.Visitors.Client.UniversalApp.WindowsStore.Views.Base
{
    using MyCompany.Visitors.Client.UniversalApp.Settings;
    using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Base page for all views.
    /// </summary>
    public class BasePage : Page
    {
        private const string SNAPPED_VIEW = "Snapped";
        private const string FULL_VIEW = "FullScreenLandscape";
        private const string MEDIUM_SNAPPED_VIEW = "MediumSnapped";

        /// <summary>
        /// Default constructor
        /// </summary>
        public BasePage()
        {
            Window.Current.SizeChanged += Current_SizeChanged;
            if (Window.Current.Bounds.Width <= AppSettings.SNAPPED_WIDTH)
                VisualStateManager.GoToState(this, SNAPPED_VIEW, true);
            else if (Window.Current.Bounds.Width <= AppSettings.MEDIUM_SNAPPED_WIDTH)
                VisualStateManager.GoToState(this, MEDIUM_SNAPPED_VIEW, true);
            else
                VisualStateManager.GoToState(this, FULL_VIEW, true);
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            if (Window.Current.Bounds.Width <= AppSettings.SNAPPED_WIDTH)
                VisualStateManager.GoToState(this, SNAPPED_VIEW, true);
            else if (Window.Current.Bounds.Width <= AppSettings.MEDIUM_SNAPPED_WIDTH)
                VisualStateManager.GoToState(this, MEDIUM_SNAPPED_VIEW, true);
            else
                VisualStateManager.GoToState(this, FULL_VIEW, true);
        }


        /// <summary>
        /// On navigated to method.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            if (Window.Current.Bounds.Width <= AppSettings.SNAPPED_WIDTH)
                VisualStateManager.GoToState(this, SNAPPED_VIEW, true);
            else if (Window.Current.Bounds.Width <= AppSettings.MEDIUM_SNAPPED_WIDTH)
                VisualStateManager.GoToState(this, MEDIUM_SNAPPED_VIEW, true);
            else
                VisualStateManager.GoToState(this, FULL_VIEW, true);

            base.OnNavigatedTo(e);
        }
    }
}
