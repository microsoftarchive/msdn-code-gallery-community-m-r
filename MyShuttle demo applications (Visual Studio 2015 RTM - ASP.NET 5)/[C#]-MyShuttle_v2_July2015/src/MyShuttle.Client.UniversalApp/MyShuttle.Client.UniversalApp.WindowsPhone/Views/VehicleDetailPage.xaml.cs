using Windows.UI.Xaml;
using MyShuttle.Client.UniversalApp.Views.Base;

namespace MyShuttle.Client.UniversalApp.Views
{
    public sealed partial class VehicleDetailPage : WindowsPhoneBasePage
    {
        public VehicleDetailPage()
        {
            this.InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ChangePositionPage));
        }
    }
}