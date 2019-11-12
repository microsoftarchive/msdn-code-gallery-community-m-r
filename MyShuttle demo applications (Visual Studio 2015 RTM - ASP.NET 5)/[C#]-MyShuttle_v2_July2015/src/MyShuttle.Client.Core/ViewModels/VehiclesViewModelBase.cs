using System.Linq;
using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.DocumentResponse;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Services;
using MyShuttle.Client.Core.Model;
using MyShuttle.Client.Core.ServiceAgents.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.Messenger;
using MyShuttle.Client.Core.Messages;

namespace MyShuttle.Client.Core.ViewModels
{
    public abstract class VehiclesViewModelBase : MvxViewModel
    {
        private readonly ILocationServiceSingleton _locationService;
        private readonly IMvxMessenger _messenger;
        private readonly MvxSubscriptionToken _token;

        private bool _isLoadingFilteredVehicles;
        private ObservableCollection<Vehicle> _filteredVehicles;

        private ICommand _navigateToVehicleDetailsCommand;
        private ICommand _navigateToVehicleDetailsAlternativeCommand;

        public IApplicationSettingServiceSingleton ApplicationSettingService { get; private set; }

        protected IMyShuttleClient MyShuttleClient { get; private set; }

        public bool IsLoadingFilteredVehicles
        {
            get
            {
                return _isLoadingFilteredVehicles;
            }
            set
            {
                _isLoadingFilteredVehicles = value;
                RaisePropertyChanged(() => IsLoadingFilteredVehicles);
            }
        }

        public ObservableCollection<Vehicle> FilteredVehicles
        {
            get
            {
                return _filteredVehicles;
            }
            set
            {
                _filteredVehicles = value;
                RaisePropertyChanged(() => FilteredVehicles);
            }
        }

        public Location CurrentLocation { get; private set; }

        public ICommand NavigateToVehicleDetailsCommand
        {
            get { return _navigateToVehicleDetailsCommand; }
        }

        public ICommand NavigateToVehicleDetailsAlternativeCommand
        {
            get { return _navigateToVehicleDetailsAlternativeCommand; }
        }

        protected VehiclesViewModelBase(
            IMyShuttleClient myShuttleClient,
            ILocationServiceSingleton locationService,
            IApplicationSettingServiceSingleton applicationSettingService,
            IMvxMessenger messenger)
        {
            if (locationService == null)
            {
                throw new ArgumentNullException("locationService");
            }

            if (myShuttleClient == null)
            {
                throw new ArgumentNullException("myShuttleClient");
            }

            if (applicationSettingService == null)
            {
                throw new ArgumentNullException("applicationSettingService");
            }

            if (messenger == null)
            {
                throw new ArgumentNullException("messenger");
            }

            MyShuttleClient = myShuttleClient;
            ApplicationSettingService = applicationSettingService;
            _locationService = locationService;
            _messenger = messenger;

            _token = _messenger.Subscribe<ReloadDataMessage>(_ => InitializeActions());

            InitializeCommands();
            InitializeActions();
        }

        protected async virtual Task LoadFilteredVehiclesAsync()
        {
            try
            {
                FilteredVehicles = new ObservableCollection<Vehicle>(await MyShuttleClient.VehiclesService.GetByDistanceAsync(CurrentLocation.Latitude, CurrentLocation.Longitude, ApplicationSettingService.TopListItemsCount));
            }
            catch 
            {
                FilteredVehicles = new ObservableCollection<Vehicle>();
            }
        }

        private async void InitializeActions()
        {
            IsLoadingFilteredVehicles = true;

            // Run & forget
            CurrentLocation = await _locationService.CalculatePositionAsync();
            await LoadFilteredVehiclesAsync();

            IsLoadingFilteredVehicles = false;
        }

        private void InitializeCommands()
        {
            _navigateToVehicleDetailsCommand = new MvxCommand<int>(NavigateToVehicleDetails);
            // This command is needed for MVVMCross-iOS due to source table *must* send the whole Vehicle object
            _navigateToVehicleDetailsAlternativeCommand = new MvxCommand<Vehicle>(vehicle => this.NavigateToVehicleDetails(vehicle.VehicleId));
        }

        private void NavigateToVehicleDetails(int vehicleId)
        {
            ShowViewModel<VehicleDetailViewModel>(new { currentVehicleId = vehicleId });
        }
    }
}
