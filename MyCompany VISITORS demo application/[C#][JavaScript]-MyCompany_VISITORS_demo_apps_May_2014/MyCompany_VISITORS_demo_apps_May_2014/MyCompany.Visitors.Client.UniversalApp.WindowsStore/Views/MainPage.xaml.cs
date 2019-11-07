namespace MyCompany.Visitors.Client.UniversalApp.WindowsStore.Views
{
    using MyCompany.Visitors.Client.UniversalApp.ViewModel;
    using MyCompany.Visitors.Client.UniversalApp.WindowsStore.Views.Base;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : BasePage
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
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
            AppBar.IsOpen = false;
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.New)
            {
                var vm = (VMMainPage)this.DataContext;
                await vm.InitializeData();
            }
        }
    }
}
