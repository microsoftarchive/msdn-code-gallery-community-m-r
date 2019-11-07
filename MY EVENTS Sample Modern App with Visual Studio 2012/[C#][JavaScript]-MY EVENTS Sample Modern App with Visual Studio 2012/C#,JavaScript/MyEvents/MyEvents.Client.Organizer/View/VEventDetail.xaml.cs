using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MyEvents.Client.Organizer.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VEventDetail : Page
    {
        public VEventDetail()
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += Current_SizeChanged;
            VisualStateManager.GoToState(this, ApplicationView.Value.ToString(), true);
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
            var vm = this.DataContext as EventDetailViewModel;
            vm.Load();
            vm.InitializeData(e.Parameter as EventDefinition);
        }

        /// <summary>
        /// Called just before a page is no longer the active page in a frame.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            var vm = this.DataContext as EventDetailViewModel;
            vm.UnLoad();
        }

        private void SessionsPinButton_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            GeneralTransform buttonTransform = element.TransformToVisual(null);
            Point point = buttonTransform.TransformPoint(new Point());
            Rect rect = new Rect(point, new Size(element.ActualWidth, element.ActualHeight));

            (this.DataContext as EventDetailViewModel).PinToStartCommand.Execute(rect);
        }
    }
}
