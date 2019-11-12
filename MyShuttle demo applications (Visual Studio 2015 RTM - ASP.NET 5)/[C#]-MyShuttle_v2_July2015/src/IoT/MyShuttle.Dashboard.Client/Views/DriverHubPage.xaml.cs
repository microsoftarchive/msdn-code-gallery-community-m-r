namespace MyShuttle.Dashboard.Client.Views
{
    using MyShuttle.Dashboard.Client.Helpers;
    using MyShuttle.Dashboard.Client.ViewModels;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Media.Animation;

    public sealed partial class DriverHubPage
    {
        DriverHubPageViewModel vm;
        public DriverHubPage()
        {
            InitializeComponent();
            this.SizeChanged += DriverHubPage_SizeChanged;
            this.Loaded += DriverHubPage_Loaded;
            this.DataContextChanged += DriverHubPage_DataContextChanged;
        }

        private void DriverHubPage_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            vm = DataContext as DriverHubPageViewModel;
            vm.RatingsDataLoaded += Vm_RatingsDataLoaded;
        }

        private void Vm_RatingsDataLoaded(object sender, System.EventArgs e)
        {
            var grid = ControlTreeHelper.FindNameInVisualTree<Grid>(RatingsHubSection, "RatingsGrid");
            var storyboard = ((Storyboard)grid.Resources["RatingsLoadStoryBoard"]);
            storyboard.Begin();
        }

        private void DriverHubPage_Loaded(object sender, RoutedEventArgs e)
        {
            DriverSectionHub.Width = WindowHelper.IsSmallResolution() ? 770 : 480;
        }

        private void DriverHubPage_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            var vm = DataContext as DriverHubPageViewModel;
            if (vm!=null) vm.Refresh();

            var low = ControlTreeHelper.FindNameInVisualTree<StackPanel>(DriverSectionHub, "LowViewState");
            var normal = ControlTreeHelper.FindNameInVisualTree<StackPanel>(DriverSectionHub, "NormalViewState");

            var isLowRes = WindowHelper.IsSmallResolution();

            DriverHubPage_Loaded(this, null);
            low.Visibility = isLowRes ? Visibility.Visible : Visibility.Collapsed;
            normal.Visibility = isLowRes ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
