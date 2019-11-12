using InstanceFactory.MessageBoxSample.Common;
using InstanceFactory.MessageBoxSample.SettingsPages;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;

namespace InstanceFactory.MessageBoxSample
{
  /// <summary>
  /// Provides application-specific behavior to supplement the default Application class.
  /// </summary>
  sealed partial class App : Application
  {
    #region Private Const Date Member

    /// <summary>
    /// Keeps the width of the settings popup.
    /// </summary>
    /// <remarks>
    /// UI guidelines specify this should be 346 or 646 depending on your needs.
    /// </remarks>
    private const int SettingsPopupWidth = 346;

    #endregion Private Const Date Member


    #region Private Properties

    /// <summary>
    /// Gets / sets the popup used to display the settings pages.
    /// </summary>
    /// <remarks>
    /// When declared locally in OpenSettingsPage, the app crashes with an unhandled exception (event an UnhandledException handler is declared),
    /// when the user clicks outside of the settings page.
    /// </remarks>
    private Popup SettingsPopup { get; set; }

    #endregion Private Properties


    #region Public Constructors

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
      this.InitializeComponent();
    }

    #endregion Public Constructors


    #region Private Methods

    /// <summary>
    /// Invoked when settings commands are requested.
    /// </summary>
    /// <param name="settingsPane">The current <see cref="SettingsPane"/></param>
    /// <param name="args">Event arguments.</param>
    private void OnSettingsCommandsRequested
      (
      SettingsPane settingsPane,
      SettingsPaneCommandsRequestedEventArgs args
      )
    {
      args.Request.ApplicationCommands.Add(new SettingsCommand("about", "About", OnSettingsInvoked));
    }

    /// <summary>
    /// Invoked when a settings command is selected.
    /// </summary>
    /// <param name="command">The current UI command.</param>
    private void OnSettingsInvoked
      (
      IUICommand command
      )
    {
      // Get the command id.
      string commandId = command.Id as string;

      // Has to be set, otherwise return.
      if (commandId == null)
      {
        return;
      }

      switch (commandId)
      {
        case "about":
          OpenSettingsPage<AboutPage>();
          break;
        default:
          break;
      }
    }

    /// <summary>
    /// Opens a settings page.
    /// </summary>
    /// <typeparam name="TPage">The type of settings page to be opened.</typeparam>
    /// <remarks>
    /// see: Implement Settings Popup Pages for Windows Store Apps (http://blog.instance-factory.com/?p=501)
    /// </remarks>
    private void OpenSettingsPage<TPage>()
      where TPage : SettingsPageBase, new()
    {
      SettingsPopup = new Popup();

      SettingsPopup.IsLightDismissEnabled = true;
      SettingsPopup.Width = App.SettingsPopupWidth;
      SettingsPopup.Height = Window.Current.Bounds.Height;

      // Add the proper animation for the panel.
      SettingsPopup.ChildTransitions = new TransitionCollection();
      SettingsPopup.ChildTransitions.Add(new PaneThemeTransition()
      {
        Edge = (SettingsPane.Edge == SettingsEdgeLocation.Right) ?
               EdgeTransitionLocation.Right :
               EdgeTransitionLocation.Left
      });

      // Create a SettingsFlyout the same dimenssions as the Popup.
      TPage settingsFlyout = new TPage();
      settingsFlyout.Width = SettingsPopup.Width;
      settingsFlyout.Height = SettingsPopup.Height;

      // Place the SettingsFlyout inside our Popup window.
      SettingsPopup.Child = settingsFlyout;

      // define the location of our Popup.
      SettingsPopup.SetValue(Canvas.LeftProperty,
        SettingsPane.Edge == SettingsEdgeLocation.Right
        ? (Window.Current.Bounds.Width - SettingsPopup.Width)
          : 0);
      SettingsPopup.SetValue(Canvas.TopProperty, 0);
      SettingsPopup.IsOpen = true;
    }

    #endregion Private Methods


    #region Protected Methods


    /// <summary>
    /// Invoked when the application is launched normally by the end user.  Other entry points
    /// will be used when the application is launched to open a specific file, to display
    /// search results, and so forth.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
      Frame rootFrame = Window.Current.Content as Frame;

      // Do not repeat app initialization when the Window already has content,
      // just ensure that the window is active
      if (rootFrame == null)
      {
        // Create a Frame to act as the navigation context and navigate to the first page
        rootFrame = new Frame();

        if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
        {
          //TODO: Load state from previously suspended application
        }

        // Place the frame in the current Window
        Window.Current.Content = rootFrame;
      }

      if (rootFrame.Content == null)
      {
        // When the navigation stack isn't restored navigate to the first page,
        // configuring the new page by passing required information as a navigation
        // parameter
        if (!rootFrame.Navigate(typeof(MainPage), args.Arguments))
        {
          throw new Exception("Failed to create initial page");
        }
      }
      // Ensure the current window is active
      Window.Current.Activate();

      // Register Settings command request handler
      SettingsPane.GetForCurrentView().CommandsRequested += OnSettingsCommandsRequested;
    }

    #endregion Protected Methods
  }
}
