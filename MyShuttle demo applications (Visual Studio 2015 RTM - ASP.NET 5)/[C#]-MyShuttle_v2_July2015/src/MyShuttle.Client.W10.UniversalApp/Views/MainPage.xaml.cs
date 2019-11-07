using MyShuttle.Client.W10.UniversalApp.ViewModels;
using MyShuttle.Client.W10.UniversalApp.Views.Base;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;

namespace MyShuttle.Client.W10.UniversalApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : WindowsBasePage
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.SizeChanged += MainPage_SizeChanged;
        }
        
        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.SizeChanged -= MainPage_SizeChanged;

            var dc = DataContext as MainPageViewModel;

            var viewState = new Dictionary<double, int>
            {
                { 1660, 5 },
                { 1400, 4 },
                { 1024, 3 },
                {  800, 2 },
                {  540, 1 },
                {    0, 0 }
            }
            .FirstOrDefault(x => x.Key <= ActualWidth)
            .Value;

            dc.CurrentViewState = viewState;

        }
    }
}
