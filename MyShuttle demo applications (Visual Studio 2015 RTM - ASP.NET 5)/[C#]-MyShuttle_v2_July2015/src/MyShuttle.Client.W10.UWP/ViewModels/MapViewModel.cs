using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Services;
using MyShuttle.Client.Core.ViewModels;
using System.Windows.Input;

namespace MyShuttle.Client.W10.UniversalApp.ViewModels
{
    public class MapViewModel : MvxViewModel
    {
        public IVehiclesInMapViewModel VehiclesByDistanceViewModel { get; private set; }
        public ICommand NavigateToVehicleDetailsCommand { get; set; }

        public string MapToken => _applicationSettingService.UniversalAppBingMapsToken;
        private readonly ILocationServiceSingleton _locationService;
        private readonly IApplicationSettingServiceSingleton _applicationSettingService;

        public MapViewModel(
            ILocationServiceSingleton locationService, 
            IApplicationSettingServiceSingleton applicationSettingService,
            IVehiclesInMapViewModel vehiclesByDistanceViewModel)
        {
            _locationService = locationService;
            _applicationSettingService = applicationSettingService;
            VehiclesByDistanceViewModel = vehiclesByDistanceViewModel;

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            NavigateToVehicleDetailsCommand = new MvxCommand<int>(NavigateToVehicleDetails);
        }

        private void NavigateToVehicleDetails(int currentVehicleId)
        {
            ShowViewModel<VehicleDetailViewModel>(new { currentVehicleId = currentVehicleId });
        }

    }
}
