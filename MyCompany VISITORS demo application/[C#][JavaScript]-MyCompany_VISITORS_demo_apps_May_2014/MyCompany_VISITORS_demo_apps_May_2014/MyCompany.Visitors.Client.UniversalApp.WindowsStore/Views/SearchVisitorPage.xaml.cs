namespace MyCompany.Visitors.Client.UniversalApp.WindowsStore.Views
{
    using MyCompany.Visitors.Client.UniversalApp.WindowsStore.ViewModel;
    using MyCompany.Visitors.Client.UniversalApp.WindowsStore.Views.Base;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchVisitorPage : BasePage
    {
        /// <summary>
        /// Visitor Id.
        /// </summary>
        public int? VisitorId { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public SearchVisitorPage()
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
            var vm = (VMSearchVisitorPage)this.DataContext;
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
            var vm = (VMSearchVisitorPage)this.DataContext;
            //vm.ResetSearch();
        }
    }
}
