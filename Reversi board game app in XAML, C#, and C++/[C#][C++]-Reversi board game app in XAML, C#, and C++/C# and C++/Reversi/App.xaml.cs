using Reversi.Common;
using Reversi.Models;
using Reversi.ViewModels;
using Reversi.Views;
using Reversi.Views.Settings;
using ReversiGameModel;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.ApplicationSettings;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The project template is documented at http://go.microsoft.com/fwlink/?LinkId=234226

namespace Reversi
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App
    {
        private SettingsViewModel _settingsViewModel;

        /// <summary>
        /// A reference to the settings view-model.
        /// </summary>
        public SettingsViewModel SettingsViewModel
        {
            get
            {
                if (_settingsViewModel != null) return _settingsViewModel;
                _settingsViewModel = new SettingsViewModel();
                if (SuspensionManager.SessionState.ContainsKey("GameViewModel"))
                {
                    _settingsViewModel.GameViewModel = SuspensionManager
                        .SessionState["GameViewModel"] as GameViewModel;
                }
                return _settingsViewModel;
            }
        }

        /// <summary>
        /// Initializes the singleton Application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
            Resuming += (sender, e) => Start();
            ApplicationData.Current.DataChanged += (sender, e) => SettingsViewModel.UpdateSettings();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active

            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                //Associate the frame with a SuspensionManager key                                
                SuspensionManager.RegisterFrame(rootFrame, "AppFrame");
                SuspensionManager.KnownTypes.AddRange(new[] { typeof(GameViewModel), typeof(ClockViewModel),
                    typeof(State), typeof(Score), typeof(Space), GameFactory.GetGameType() });

                // Set the default language
                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated || 
                    e.PreviousExecutionState == ApplicationExecutionState.ClosedByUser)
                {
                    // Restore the saved session state only when appropriate
                    try
                    {
                        await SuspensionManager.RestoreAsync();
                    }
                    catch (SuspensionManagerException)
                    {
                        //Something went wrong restoring state.
                        //Assume there is no state and continue
                    }
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }
            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(StartPage));
            }
            else if (e.PreviousExecutionState == ApplicationExecutionState.ClosedByUser)
            {
                // If the app was explicitly closed, restart from the start page. 
                // Otherwise, stay on the current page. 
                while (rootFrame.CanGoBack) rootFrame.GoBack();
            }
            // Ensure the current window is active
            Window.Current.Activate();
            Start();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Adds various event handlers, starts the game clock, and performs the next move if it is automatic. 
        /// </summary>
        private void Start()
        {
            Window.Current.VisibilityChanged += OnWindowVisibilityChanged;
            SettingsPane.GetForCurrentView().CommandsRequested += OnCommandsRequested;

            if ((Window.Current.Content as Frame).Content is GamePage)
                SettingsViewModel.GameViewModel.Clock.Start();

            // If it is the computer's turn in the current game, 
            // let it start thinking. 
            if (SettingsViewModel.GameViewModel != null)
            {
                SettingsViewModel.GameViewModel.Start();
            }
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            if (SettingsViewModel.GameViewModel != null)
            {
                SuspensionManager.SessionState["GameViewModel"] = SettingsViewModel.GameViewModel;
            }
            Window.Current.VisibilityChanged -= OnWindowVisibilityChanged;
            SettingsPane.GetForCurrentView().CommandsRequested -= OnCommandsRequested;

            // Use a deferral to ensure that the async operation completes before the method returns. 
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }

        /// <summary>
        /// Pauses or resumes the game clock when the window visibility changes.
        /// </summary>
        /// <param name="sender">The source of the property change.</param>
        /// <param name="e">Details about the property change.</param>
        private void OnWindowVisibilityChanged(object sender, 
            VisibilityChangedEventArgs e)
        {
            UpdateClock();
        }

        /// <summary>
        /// Pauses or resumes the game clock when the window changes state.
        /// </summary>
        private void UpdateClock()
        {
            if (SettingsViewModel.GameViewModel == null) return;
            if (!Window.Current.Visible)
            {
                SettingsViewModel.GameViewModel.Clock.Stop();
            }
            else if ((Window.Current.Content as Frame).Content is GamePage)
            {
                SettingsViewModel.GameViewModel.Clock.Start();
            }
        }

        // In most cases, it is a good idea to pause the game while the 
        // settings pane is open. You can do this by adding the following code
        // to the top of the OnCommandsRequested method. This code is not 
        // used by Reversi because the display settings are simple, and because
        // changing the settings results in animated changes to the game board 
        // that would not be visible if the game were paused. 
        //if (SettingsViewModel.GameViewModel != null &&
        //    SettingsViewModel.IsClockShowing)
        //    SettingsViewModel.GameViewModel.Clock.Stop();

        /// <summary>
        /// Provides app commands to populate the settings flyout.
        /// </summary>
        /// <param name="sender">The source of the command request.</param>
        /// <param name="args">Details about the command request</param>
        private void OnCommandsRequested(SettingsPane sender,
            SettingsPaneCommandsRequestedEventArgs args)
        {
            args.Request.ApplicationCommands.Add(new SettingsCommand("Display", "Display options", 
                _ => (new DisplaySettings() { DataContext = SettingsViewModel }).Show()));
            args.Request.ApplicationCommands.Add(new SettingsCommand("NewGame", "New game options", 
                _ => (new NewGameSettings() { DataContext = SettingsViewModel }).Show()));
        }

    }
}
