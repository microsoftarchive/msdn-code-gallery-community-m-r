using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.ViewModels;
using Windows.UI.Xaml.Controls;

namespace MyShuttle.Client.UniversalApp.Views
{
    public sealed partial class Settings : SettingsFlyout
    {
        public Settings()
        {
            this.InitializeComponent();
        }
        public Settings(IMvxViewModel vm)
        {
            
            this.InitializeComponent();
            this.DataContext = vm;
        }
    }
}
