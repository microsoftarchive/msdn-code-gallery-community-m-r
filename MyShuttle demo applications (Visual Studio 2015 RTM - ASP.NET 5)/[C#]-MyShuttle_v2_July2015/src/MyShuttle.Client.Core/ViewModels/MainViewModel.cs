using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.ViewModels.InterfacesForDependencyInjection;
using System.Windows.Input;

namespace MyShuttle.Client.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private ICommand _navigateSettingsCommand;

        public IVehiclesByDistanceViewModel VehiclesByDistanceViewModel { get; private set; }

        public IVehiclesByPriceViewModel VehiclesByPriceViewModel { get; private set; }

        public IVehiclesInMapViewModel VehiclesInMapViewModel { get; private set; }

        public IMyRidesViewModel MyRidesViewModel { get; private set; }

        public ICompanyRidesViewModel CompanyRidesViewModel { get; private set; }


        // Constructors
        public MainViewModel(
            IVehiclesByDistanceViewModel vehiclesByDistanceViewModel,
            IVehiclesByPriceViewModel vehiclesByPriceViewModel,
            IVehiclesInMapViewModel vehiclesInMapViewModel,
            IMyRidesViewModel myRidesViewModel,
            ICompanyRidesViewModel companyRidesViewModel
            )
        {
            this.VehiclesByDistanceViewModel = vehiclesByDistanceViewModel;
            this.VehiclesByPriceViewModel = vehiclesByPriceViewModel;
            this.VehiclesInMapViewModel = vehiclesInMapViewModel;
            this.MyRidesViewModel = myRidesViewModel;
            this.CompanyRidesViewModel = companyRidesViewModel;
            
            InitializeCommands();
        }

        // Commands
        public ICommand NavigateToSettingsCommand
        {
            get { return _navigateSettingsCommand; }
        }

        private void InitializeCommands()
        {
            _navigateSettingsCommand = new MvxCommand(NavigateToSettings);
        }

        private void NavigateToSettings()
        {
            ShowViewModel<SettingsViewModel>();
        }
    }
}
