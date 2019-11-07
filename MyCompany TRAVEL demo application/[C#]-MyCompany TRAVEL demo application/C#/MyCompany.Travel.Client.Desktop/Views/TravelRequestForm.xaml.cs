namespace MyCompany.Travel.Client.Desktop.Views
{
    using GalaSoft.MvvmLight.Command;
    using MyCompany.Travel.Client.Desktop.ViewModel;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for TravelRequestForm.xaml
    /// </summary>
    public partial class TravelRequestForm : UserControl
    {
        /// <summary>
        /// Travel Request form
        /// </summary>
        public TravelRequestForm(TravelRequest travelRequest)
        {
            InitializeComponent();
            var vm = (TravelRequestFormViewModel)this.DataContext;
            var task = vm.InitializeData(travelRequest);
        }
    }
}
