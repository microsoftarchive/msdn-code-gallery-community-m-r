namespace MyCompany.Visitors.Client.UniversalApp.WindowsStore.Views
{
    using MyCompany.Visitors.Client.UniversalApp.WindowsStore.ViewModel;
    using MyCompany.Visitors.Client.UniversalApp.WindowsStore.Views.Base;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchEmployeePage : BasePage
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SearchEmployeePage()
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
            base.OnNavigatedTo(e);
            var vm = (VMSearchEmployeePage)this.DataContext;
            if (e.NavigationMode == NavigationMode.New)
            {
                await vm.InitializeData();
            }
            //vm.InitializeSearch();
        }

        /// <summary>
        /// On navigated from method.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            var vm = (VMSearchEmployeePage)this.DataContext;
            //vm.ResetSearch();
        }

        private void SearchBox_QueryChanged(Windows.UI.Xaml.Controls.SearchBox sender, Windows.UI.Xaml.Controls.SearchBoxQueryChangedEventArgs args)
        {

        }
    }
}
