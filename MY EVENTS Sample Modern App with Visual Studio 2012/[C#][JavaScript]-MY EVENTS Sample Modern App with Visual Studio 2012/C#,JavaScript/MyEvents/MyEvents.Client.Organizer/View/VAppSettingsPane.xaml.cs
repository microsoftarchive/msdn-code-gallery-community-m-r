using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MyEvents.Client.Organizer.View
{
    /// <summary>
    /// Panel to show settings
    /// </summary>
    public sealed partial class VAppSettingsPane : UserControl
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public VAppSettingsPane()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Back button clicked: close the popup.
        /// </summary>
        private void SettingsBack_Clicked(object sender, RoutedEventArgs e)
        {
            Popup popup = this.Parent as Popup;
            if (popup != null)
            {
                popup.IsOpen = false;
            }
        }
    }
}
