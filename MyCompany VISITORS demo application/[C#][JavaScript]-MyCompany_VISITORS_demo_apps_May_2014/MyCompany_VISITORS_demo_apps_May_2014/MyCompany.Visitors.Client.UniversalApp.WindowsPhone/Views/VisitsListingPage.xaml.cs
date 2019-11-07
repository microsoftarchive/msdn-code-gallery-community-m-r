using MyCompany.Visitors.Client.UniversalApp.ViewModel;
using MyCompany.Visitors.Client.UniversalApp.WindowsPhone.Views.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MyCompany.Visitors.Client.UniversalApp.WindowsPhone.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VisitsListingPage : BasePage
    {
        public VisitsListingPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
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
                bool showOnlyToday = false;
                bool.TryParse(e.Parameter.ToString(), out showOnlyToday);
                VMVisitsListingPage vm = (VMVisitsListingPage)this.DataContext;
                await vm.InitializeData(showOnlyToday);
            }
        }
    }
}
