namespace MyCompany.Visitors.Client.UniversalApp.WindowsStore.Views
{
    using Windows.UI.Xaml.Navigation;
    using MyCompany.Visitors.Client.UniversalApp.WindowsStore.Views.Base;
    using MyCompany.Visitors.Client.UniversalApp.WindowsStore.ViewModel;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewVisitorPage : BasePage
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NewVisitorPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached. The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var vm = (VMNewVisitorPage)this.DataContext;
            vm.InitializeData();
            vm.AreEnabledButtoms = true;

            bool isFromNfc = false;

            if(e.Parameter != null)
            {
                bool.TryParse(e.Parameter.ToString(), out isFromNfc);
            }

            if (isFromNfc && e.NavigationMode == NavigationMode.New)
            {
                vm.Visitor = App.VisitorReceivedByNFC;
                vm.SetImageToCrop();
            }
            else
            {
                vm.IsVisibleCrop = false;
            }
        }

        /// <summary>
        /// Invoked when user leaves this page.
        /// </summary>
        /// <param name="e">Navigatoin event arguments</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            var vm = (VMNewVisitorPage) this.DataContext;
            vm.RemovePicturesStoraged();
        }
    }
}
