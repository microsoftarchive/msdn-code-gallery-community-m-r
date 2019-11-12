using MyCompany.Visitors.Client.UniversalApp.ViewModel;
using MyCompany.Visitors.Client.UniversalApp.WindowsPhone.Views;
using MyCompany.Visitors.Client.UniversalApp.WindowsPhone.Views.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace MyCompany.Visitors.Client.UniversalApp.WindowsPhone.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : BasePage 
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        void MasterHub_SectionHeaderClick(object sender, Windows.UI.Xaml.Controls.HubSectionHeaderClickEventArgs e)
        {
            var vm = (VMMainPage)this.DataContext;

            if (e.Section.Header.ToString() == vm.TodayVisitsHeader)
            {
                vm.NavigateToVisitsListingCommand.Execute(1);
            }
            else if (e.Section.Header.ToString() == vm.OtherVisitsHeader)
            {
                vm.NavigateToVisitsListingCommand.Execute(2);
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.New)
            {
                var vm = (VMMainPage)this.DataContext;
                await vm.InitializeData();
            }
        }
      
    }
}
