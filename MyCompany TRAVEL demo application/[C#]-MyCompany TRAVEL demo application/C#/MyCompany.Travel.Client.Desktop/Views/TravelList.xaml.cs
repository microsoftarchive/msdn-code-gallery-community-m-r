namespace MyCompany.Travel.Client.Desktop.Views
{
    using GalaSoft.MvvmLight.Command;
    using MyCompany.Travel.Client.Desktop.Helpers;
    using MyCompany.Travel.Client.Desktop.ViewModel;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for TravelList.xaml
    /// </summary>
    public partial class TravelList : UserControl
    {
        /// <summary>
        /// Travel List constructor
        /// </summary>
        public TravelList()
        {
            InitializeComponent();
            InitializeData();
        }

        private async void InitializeData()
        {
            var vm = (TravelListViewModel)this.DataContext;
            await vm.InitializeData();
        }

        private void TextBoxKeyboard_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            TabtipHelper tabtipHelper = TabtipHelper.Instance;
            tabtipHelper.TryKillTabtipProcess();
        }
    }
}
