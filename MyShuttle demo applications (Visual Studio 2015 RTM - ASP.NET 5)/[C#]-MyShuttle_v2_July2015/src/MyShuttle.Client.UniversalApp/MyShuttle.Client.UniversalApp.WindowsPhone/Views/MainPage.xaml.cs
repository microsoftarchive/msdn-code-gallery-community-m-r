using Windows.UI.Xaml.Controls;
using MyShuttle.Client.UniversalApp.Views.Base;
using MyShuttle.Client.UniversalApp.Views.Partials;
using Windows.UI.Xaml;

namespace MyShuttle.Client.UniversalApp.Views
{
    public sealed partial class Main : WindowsPhoneBasePage
    {
        public Main()
        {
            this.InitializeComponent();
        }

        private void VehiclesInMapWrapper_OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            var grid = (Grid)sender;

            if (grid != null && grid.DataContext != null)
            {
                var map = MapFactory.GetMap(grid.DataContext);
                grid.Children.Clear();
                grid.Children.Add(map);
            }
        }

        private void VehiclesInMapWrapper_OnUnloaded(object sender, RoutedEventArgs e)
        {
            var grid = (Grid)sender;
            grid.Children.Clear();
        }
    }
}
