using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.System.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LockScreen
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DisplayRequest _displayRequest;
        public MainPage()
        {
            this.InitializeComponent();
            _displayRequest = new Windows.System.Display.DisplayRequest();
        }

        private void toggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {

            if(toggleSwitch.IsOn)
            {
                _displayRequest.RequestActive();
            }
            else
            {
                _displayRequest.RequestRelease();
            }
        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://blogs.windows.com/buildingapps/2016/05/24/how-to-prevent-screen-locks-in-your-uwp-apps/"));
        }
    }
}
