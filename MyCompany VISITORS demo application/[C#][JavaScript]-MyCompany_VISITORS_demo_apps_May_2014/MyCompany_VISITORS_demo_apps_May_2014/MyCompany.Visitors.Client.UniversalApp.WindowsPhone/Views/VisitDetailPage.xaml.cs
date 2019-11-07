using MyCompany.Visitors.Client.UniversalApp.ViewModel;
using MyCompany.Visitors.Client.UniversalApp.WindowsPhone.Helpers;
using MyCompany.Visitors.Client.UniversalApp.WindowsPhone.Views.Base;
using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.Media.Capture;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MyCompany.Visitors.Client.UniversalApp.WindowsPhone.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VisitDetailPage : BasePage
    {
        private const string VISIT = "Visit_";        

        public VisitDetailPage()
        {
            this.InitializeComponent();            
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.New)
            {
                var vm = (VMVisitDetailPage)this.DataContext;
                vm.Visit = null;
                int visitId = 0;
                var idToParse = e.Parameter.ToString();

                vm.NavHistoryWasEmpty = NavigationHistoryWasEmpty();

                // If comes from secondary tile.
                if (idToParse.Contains(VISIT))
                {
                    idToParse = e.Parameter.ToString().Substring(6);
                }

                int.TryParse(idToParse, out visitId);
                await vm.InitializeData(visitId);
            }
        }

        private bool NavigationHistoryWasEmpty()
        {
            string navigationHistory = Frame.GetNavigationState();
            var history = new List<string>(navigationHistory.Split(','));

            return (int.Parse(history[2]) == 0);
        }

        private void PinVisitButton_Click(object sender, RoutedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            GeneralTransform buttonTransform = element.TransformToVisual(null);
            Point point = buttonTransform.TransformPoint(new Point());
            var rect = new Rect(point, new Size(element.ActualWidth, element.ActualHeight));

            var vm = (VMVisitDetailPage)this.DataContext;
            vm.PinVisitCommand.Execute(rect);
        }

        private void ChangeImageButton_Click(object sender, RoutedEventArgs e)
        {
            this.CaptureControl.Visibility = Visibility.Visible;
        }     
        
    }
}
