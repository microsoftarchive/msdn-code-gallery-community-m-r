using Cirrious.MvvmCross.WindowsCommon.Views;
using Windows.UI.Xaml;

namespace MyShuttle.Client.UniversalApp.Views.Base
{
    public class WindowsBasePage : MvxWindowsPage
    {
        public WindowsBasePage()
        {
            
            //this.Loaded += (sender, e) =>
            //{
            //    Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            //};

            //this.Unloaded += (sender, e) =>
            //{
            //    Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            //};
        }

        protected virtual void GoBack(object sender, RoutedEventArgs e)
        {
            // Use the navigation frame to return to the previous page
            if (this.Frame != null && this.Frame.CanGoBack) this.Frame.GoBack();
        }

    }
}
