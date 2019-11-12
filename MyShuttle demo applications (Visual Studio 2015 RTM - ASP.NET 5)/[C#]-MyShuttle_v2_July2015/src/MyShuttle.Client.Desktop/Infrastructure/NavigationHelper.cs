using Autofac;
using MyShuttle.Client.Desktop.ViewModels;
using MyShuttle.Client.Desktop.Views;
using System.Windows.Controls;

namespace MyShuttle.Client.Desktop.Infrastructure
{
    public static class NavigationHelper
    {
        public static Frame MainFrame { get; set; }

        public static void NavigateToMainPage()
        {
            if (MainFrame.Content is MainView)
            {
                return;
            }

            if (MainFrame.CanGoBack)
            {
                MainFrame.Navigated += MainViewNavigated;
                MainFrame.GoBack();
                return;
            }

            Navigate<MainView, MainViewModel>();
        }

        private static void MainViewNavigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            MainFrame.Navigated -= MainViewNavigated;
            ((MainViewModel)((MainView)MainFrame.Content).DataContext).Update();
        }

        public static void NavigateToCameraPage()
        {
            if (MainFrame.Content is CameraView)
            {
                return;
            }

            if (MainFrame.CanGoForward)
            {
                MainFrame.Navigated += CameraViewNavigated;
                MainFrame.GoForward();
                return;
            }

            Navigate<CameraView, CameraViewModel>();
        }

        private static void CameraViewNavigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            MainFrame.Navigated -= CameraViewNavigated;
            ((CameraViewModel)((CameraView)MainFrame.Content).DataContext).Update();
        }


        private static void Navigate<V, VM>()
            where V : Page
            where VM : BaseViewModel
        {
            var container = ((App)App.Current).Container;
            var view = container.Resolve<V>();
            var viewModel = container.Resolve<VM>();
            view.DataContext = viewModel;
            MainFrame.Navigate(view);
            viewModel.Load();
            viewModel.Update();
        }
    }
}
