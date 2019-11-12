namespace MyCompany.Travel.Client.Desktop.Views
{
    using MyCompany.Travel.Client.Desktop.Services.Security;
    using MyCompany.Travel.Client.Desktop.ViewModel;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// main Window constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
        }

        private async void InitializeData()
        {
            var vm = (MainViewModel)this.DataContext;
            await vm.InitializeData();            
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(SecurityService.AccessToken))
            {
                e.Cancel = true;
                var task = SecurityService.Logout();
            }
        }

        private void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (this.WindowState == System.Windows.WindowState.Maximized)
                    this.WindowState = System.Windows.WindowState.Normal;
                else
                    this.WindowState = System.Windows.WindowState.Maximized;
            }
            this.DragMove();
        }

        private void MinimizeButton(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeButton(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
            }
        }

        private void CloseButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
