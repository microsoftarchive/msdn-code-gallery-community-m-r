using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.View;
using MyEvents.Client.Organizer.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyEvents.Client.Organizer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Creates a new instance of MainPage control.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();

            Window.Current.SizeChanged += Current_SizeChanged;
            VisualStateManager.GoToState(this, ApplicationView.Value.ToString(), true);

            GrdItems.SelectionChanged += GrdItems_SelectionChanged;
        }

        void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            VisualStateManager.GoToState(this, ApplicationView.Value.ToString(), true);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        /// <summary>
        /// Control the selection of items with right click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((this.DataContext as MainViewModel).isLoading)
                return;
        }
    }
}
