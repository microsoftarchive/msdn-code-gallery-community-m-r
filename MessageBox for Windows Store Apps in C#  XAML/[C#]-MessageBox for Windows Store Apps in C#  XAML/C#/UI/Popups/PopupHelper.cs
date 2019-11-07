using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace InstanceFactory.MessageBoxSample.UI.Popups
{
  /// <summary>
  /// Helper class for <see cref="Popup"/>.
  /// </summary>
  public static class PopupHelper
  {
    #region Public Static Methods

    /// <summary>
    /// Creates a full screen popup with a child.
    /// </summary>
    /// <param name="child">The child of the full screen popup.</param>
    /// <returns>The fullscrenn popup.</returns>
    public static Popup CreateFullScreenWithChild
      (
      UserControl child
      )
    {
      Popup popup = new Popup()
      {
        Height = Window.Current.Bounds.Height,
        IsLightDismissEnabled = false,
        Width = Window.Current.Bounds.Width
      };

      // Set the child's size
      child.Height = popup.Height;
      child.Width = popup.Width;

      // Add the child to the popup.
      popup.Child = child;

      // Set popup's position
      popup.SetValue(Canvas.LeftProperty, 0);
      popup.SetValue(Canvas.TopProperty, 0);

      // Open it
      popup.IsOpen = true;

      // Set the focus to the child
      child.Focus(FocusState.Programmatic);

      return (popup);
    }

    #endregion Public Static Methods
  }
}
