using Windows.UI.ApplicationSettings;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace InstanceFactory.MessageBoxSample.Common
{
  /// <summary>
  /// Base class of all Pages to be displayed as part of the settings pane.
  /// </summary>
  /// <remarks>
  /// see: Implement Settings Popup Pages for Windows Store Apps (http://blog.instance-factory.com/?p=501)
  /// </remarks>
  public abstract class SettingsPageBase : Page
  {
    #region Protected Methods

    /// <summary>
    /// This is the click handler for the back button on the Flyout.
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="args">Event arguments.</param>
    protected virtual void OnGoBack
      (
      object sender,
      RoutedEventArgs args
      )
    {
      // First close our Popup.
      Popup parent = this.Parent as Popup;

      if (parent != null)
      {
        parent.IsOpen = false;
      }

      // If the app is not snapped, then the back button shows the Settings pane again.
      if (ApplicationView.Value != ApplicationViewState.Snapped)
      {
        SettingsPane.Show();
      }
    }

    #endregion Protected Methods
  }
}