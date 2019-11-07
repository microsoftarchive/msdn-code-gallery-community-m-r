using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using MyShuttle.Dashboard.Client.Helpers;
using MyShuttle.Dashboard.Client.ViewModels;

namespace MyShuttle.Dashboard.Client.Views
{
    public sealed partial class MainHubPage
    {
        private MainHubPageViewModel vm;
        public MainHubPage()
        {
            InitializeComponent();
            this.DataContextChanged += MainHubPage_DataContextChanged;
            this.SizeChanged += MainHubPage_SizeChanged;
        }

        private void MainHubPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            vm.Refresh();

            VehiclesHubSection_OnLoaded(this, null);
            FrameworkElement_OnLoaded(this, null);
        }

        private void MainHubPage_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            vm = DataContext as MainHubPageViewModel;
            vm.ChartLoaded += Vm_ChartLoaded;
        }

        private void Vm_ChartLoaded(object sender, System.EventArgs e)
        {
            var stackpanel = ControlTreeHelper.FindVisualChild<StackPanel>(ServicesHubSection);
            var storyboard = ((Storyboard)stackpanel.Resources["Cha1Storyboard"]);
            storyboard.Begin();
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            var frmkelement = (FrameworkElement)sender;

            var chart1 = ControlTreeHelper.FindNameInVisualTree<Viewbox>(ServicesHubSection, "Chart1Viewbox");
            var chart2 = ControlTreeHelper.FindNameInVisualTree<Viewbox>(ServicesHubSection, "Chart2Viewbox");
            if (chart1 != null) chart1.Width = WindowHelper.IsSmallResolution() ? 400 : 500;
            if (chart2 != null) chart2.Width = chart1.Width;
        }

        private void VehiclesHubSection_OnLoaded(object sender, RoutedEventArgs e)
        {
            var vdgv = ControlTreeHelper.FindNameInVisualTree<GridView>(VehiclesHubSection, "vehicleDataGridView");
            if (vdgv != null) vdgv.Height = WindowHelper.IsSmallResolution() ? 270 : 530;
        }
    }
}
