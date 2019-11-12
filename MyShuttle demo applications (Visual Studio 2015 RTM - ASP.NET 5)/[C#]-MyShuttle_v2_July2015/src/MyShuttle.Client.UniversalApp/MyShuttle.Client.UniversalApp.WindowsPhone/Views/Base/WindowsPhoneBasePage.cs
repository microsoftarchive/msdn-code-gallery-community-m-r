using Cirrious.MvvmCross.WindowsCommon.Views;
using MyShuttle.Client.Core.ViewModels.Behavoirs;

namespace MyShuttle.Client.UniversalApp.Views.Base
{
    public class WindowsPhoneBasePage : MvxWindowsPage
    {
        public WindowsPhoneBasePage()
        {
            this.Loaded += (sender, e) =>
            {
                Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            };

            this.Unloaded += (sender, e) =>
            {
                Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            };
        }

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            var currentViewModel = this.DataContext as ICanGoBackViewModel;
            if (currentViewModel != null)
            {
                e.Handled = true;
                currentViewModel.GoBackCommand.Execute(null);
            }
        }
    }
}
