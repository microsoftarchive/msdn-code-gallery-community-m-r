using MyShuttle.Client.Desktop.Infrastructure;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyShuttle.Client.Desktop.Controls
{
    public partial class Menu : UserControl
    {
        public static readonly DependencyProperty IsVehiclesNavigationEnabledProperty = DependencyProperty.Register("IsVehiclesNavigationEnabled", typeof(bool), typeof(Menu), new FrameworkPropertyMetadata(true));
        
        public static readonly DependencyProperty IsCameraNavigationEnabledProperty = DependencyProperty.Register("IsCameraNavigationEnabled", typeof(bool), typeof(Menu), new FrameworkPropertyMetadata(true));
        
        public static readonly DependencyProperty IsRefreshEnabledProperty = DependencyProperty.Register("IsRefreshEnabled", typeof(bool), typeof(Menu), new FrameworkPropertyMetadata(true));
        
        public static readonly DependencyProperty RefreshCommandProperty = DependencyProperty.Register("RefreshCommand", typeof(ICommand), typeof(Menu), new FrameworkPropertyMetadata(null));
        
        public static readonly DependencyProperty RefreshCommandParameterProperty = DependencyProperty.Register("RefreshCommandParameter", typeof(object), typeof(Menu), new FrameworkPropertyMetadata(null));

        public Menu()
        {
            InitializeComponent();
        }

        public bool IsVehiclesNavigationEnabled
        {
            get { return (bool)GetValue(IsVehiclesNavigationEnabledProperty); }
            set { SetValue(IsVehiclesNavigationEnabledProperty, value); }
        }

        public bool IsCameraNavigationEnabled
        {
            get { return (bool)GetValue(IsCameraNavigationEnabledProperty); }
            set { SetValue(IsCameraNavigationEnabledProperty, value); }
        }

        public bool IsRefreshEnabled
        {
            get { return (bool)GetValue(IsRefreshEnabledProperty); }
            set { SetValue(IsRefreshEnabledProperty, value); }
        }

        public bool RefreshCommand
        {
            get { return (bool)GetValue(RefreshCommandProperty); }
            set { SetValue(RefreshCommandProperty, value); }
        }

        public bool RefreshCommandParameter
        {
            get { return (bool)GetValue(RefreshCommandParameterProperty); }
            set { SetValue(RefreshCommandParameterProperty, value); }
        }

        private void VehiclesClick(object sender, RoutedEventArgs e)
        {
            NavigationHelper.NavigateToMainPage();
        }

        private void CameraClick(object sender, RoutedEventArgs e)
        {
            NavigationHelper.NavigateToCameraPage();
        }
    }
}
