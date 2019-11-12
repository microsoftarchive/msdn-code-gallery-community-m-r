using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyEvents.Client.Organizer.View;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.Storage;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MyEvents.Client.Organizer.ViewModel;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace MyEvents.Client.Organizer
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        Popup _settingsPopup;
        double _settingsWidth = 346;

        /// <summary>
        /// Exposes the application frame to use for navigation pourposes.
        /// </summary>
        public static Frame RootFrame;

        /// <summary>
        /// Indicates if we have network connectivity.
        /// </summary>
        public static Boolean HasNetworkConnection = true;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            if (NetworkInformation.GetInternetConnectionProfile() == null)
                HasNetworkConnection = false;
            else
                HasNetworkConnection = true;

            NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;
        }

        void OnCommandsRequested(SettingsPane settingsPane, SettingsPaneCommandsRequestedEventArgs args)
        {
            args.Request.ApplicationCommands.Clear();

            SettingsCommand preferences = new SettingsCommand("Prefs", "Preferences", (uiCommand) =>
            {

                Rect windowBounds = Window.Current.Bounds;

                // Create popup
                _settingsPopup = new Popup()
                {
                    IsLightDismissEnabled = true,
                    Width = _settingsWidth,
                    Height = windowBounds.Height
                };
                _settingsPopup.Closed += OnPopupClosed;
                _settingsPopup.SetValue(Canvas.LeftProperty, windowBounds.Width - _settingsWidth);
                _settingsPopup.SetValue(Canvas.TopProperty, 0);

                // Create settings pane
                VAppSettingsPane appSettingsPane = new VAppSettingsPane()
                {
                    Width = _settingsWidth,
                    Height = windowBounds.Height
                };

                // Hook settings pane in popup
                _settingsPopup.Child = appSettingsPane;

                // Display popup
                _settingsPopup.IsOpen = true;

            });
            args.Request.ApplicationCommands.Add(preferences);

            SettingsCommand SignOut = new SettingsCommand("SignOut", "Sign out", (uiCommand) =>
            {
                ApplicationData.Current.LocalSettings.Values["Facebook_AccessToken"] = string.Empty;
                RootFrame.Navigate(typeof(SplashScreen));
            });
            args.Request.ApplicationCommands.Add(SignOut);
        }

        /// <summary>
        /// Settings pane is closed. Broadcast new settings.
        /// </summary>
        private void OnPopupClosed(object sender, object e)
        {
            _settingsPopup.Closed -= OnPopupClosed;

        }

        void NetworkInformation_NetworkStatusChanged(object sender)
        {
            if (NetworkInformation.GetInternetConnectionProfile() == null)
            {
                HasNetworkConnection = false;
            }
            else
            {
                HasNetworkConnection = true;
            }
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            base.OnActivated(args);
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            ApplicationData.Current.LocalSettings.Values["TileId"] = args.TileId;
            ApplicationData.Current.LocalSettings.Values["TileArgs"] = args.Arguments;
            

            // Do not repeat app initialization when already running, just ensure that
            // the window is active
            if (args.PreviousExecutionState == ApplicationExecutionState.Running)
            {
                //Window.Current.Activate();
                //return;
            }

            if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                //TODO: Load state from previously suspended application
            }

            // Create a Frame to act navigation context and navigate to the first page
            RootFrame = new Frame();

            if (!RootFrame.Navigate(typeof(SplashScreen)))
            {
                throw new Exception("Failed to create initial page");
            }

            // Place the frame in the current Window and ensure that it is active
            Window.Current.Content = RootFrame;
            Window.Current.Activate();

            // Receive a notification when the user opens the Win8 setting charm
            SettingsPane.GetForCurrentView().CommandsRequested += OnCommandsRequested;
        }


        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
