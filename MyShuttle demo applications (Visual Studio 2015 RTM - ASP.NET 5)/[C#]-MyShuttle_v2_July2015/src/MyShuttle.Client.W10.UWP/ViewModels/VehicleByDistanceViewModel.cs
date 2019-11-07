using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.ViewModels;
using System.Windows.Input;

namespace MyShuttle.Client.W10.UniversalApp.ViewModels
{
    public class VehicleByDistanceViewModel : MvxViewModel
    {
        public IVehiclesInMapViewModel VehiclesByDistanceViewModel { get; private set; }
        public ICommand NavigateToVehicleDetailsCommand { get; set; }


        public VehicleByDistanceViewModel(
            IVehiclesInMapViewModel vehiclesByDistanceViewModel)
        {
            VehiclesByDistanceViewModel = vehiclesByDistanceViewModel;

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
