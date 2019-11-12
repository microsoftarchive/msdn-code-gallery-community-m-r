using Reversi.Common;
using Reversi.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Reversi.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public GamePage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
            this.SizeChanged += GamePage_SizeChanged;

            // Retrieve the game view model from the App class. 
            DefaultViewModel["GameViewModel"] = (Application.Current as App).SettingsViewModel.GameViewModel;
        }

        void GamePage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width - 80 < e.NewSize.Height)
            {
                VisualStateManager.GoToState(this, "VerticalLayout", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "HorizontalLayout", true);
            }
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        /// <summary>
        /// Starts the game clock upon navigation to the game page.
        /// </summary>
        /// <param name="e">The navigation event data.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
            (DefaultViewModel["GameViewModel"] as GameViewModel).Clock.Start();
        }

        /// <summary>
        /// Pauses the game clock upon navigation away from the game page. 
        /// </summary>
        /// <param name="e">The navigation event data.</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
            (DefaultViewModel["GameViewModel"] as GameViewModel).Clock.Stop();
        }

        /// <summary>
        /// Hides the game page app bar when play or pause is tapped.
        /// </summary>
        /// <param name="sender">The source of the event; ignored.</param>
        /// <param name="e">The event data; ignored.</param>
        /// <remarks>The play and pause commands are typically used once, so it is
        /// appropriate to hide the app bar after each use. The the undo and redo 
        /// commands, however, might be used multiple times in a row, in which case
        /// the app bar should stay visible until the user explicitly dismisses it. 
        private void DismissAppBar(object sender, RoutedEventArgs e)
        {
            GamePageAppBar.IsOpen = false;
        }
    }
}
