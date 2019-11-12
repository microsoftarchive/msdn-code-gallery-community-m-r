namespace MyCompany.Visitors.Client.UniversalApp.WindowsStore.Views
{
    using MyCompany.Visitors.Client.UniversalApp.ViewModel;    
    using MyCompany.Visitors.Client.UniversalApp.WindowsStore.Views.Base;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VisitsListingPage : BasePage
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public VisitsListingPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            AppBar.IsOpen = false;
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
