using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.ViewModels;
using System.Windows.Input;

namespace MyShuttle.Client.W10.UniversalApp.ViewModels
{
    public class VehicleByPriceViewModel : MvxViewModel
    {
        public IVehiclesByPriceViewModel VehiclesByPriceViewModel { get; private set; }
        public ICommand NavigateToVehicleDetailsCommand { get; set; }


        public VehicleByPriceViewModel(
            IVehiclesByPriceViewModel vehiclesByPriceViewModel)
        {
            VehiclesByPriceViewModel = vehiclesByPriceViewModel;

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            NavigateToVehicleDetailsCommand = new MvxCommand<int>(NavigateToRideDetails);
        }

        private void NavigateToRideDetails(int currentVehicleId)
        {
            ShowViewModel<VehicleDetailViewModel>(new { currentVehicleId = currentVehicleId });
        }
    }
}
