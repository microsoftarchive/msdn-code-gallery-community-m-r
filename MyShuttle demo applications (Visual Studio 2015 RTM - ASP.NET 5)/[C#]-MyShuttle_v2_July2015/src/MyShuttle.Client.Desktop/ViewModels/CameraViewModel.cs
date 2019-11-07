
using MyShuttle.Client.Core.DocumentResponse;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Repositories;
using MyShuttle.Client.Core.ServiceAgents.Interfaces;
using MyShuttle.Client.Desktop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShuttle.Client.Desktop.ViewModels
{
    public class CameraViewModel : BaseViewModel
    {
        private const string CameraViewVehicleIdKey = "CAMERAVIEWVEHICLEID";

        private bool _isDetecting;
        private Random _random;
        private Task<Vehicle> loadVehicleTask;

        protected IMyShuttleClient MyShuttleClient { get; private set; }
        protected IApplicationDataRepository ApplicationDataRepository { get; set; }

        public DetailsViewModel Details { get; set; }

        public bool IsDetecting
        {
            get
            {
                return _isDetecting;
            }
            set
            {
                _isDetecting = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand DetectingCompletedCommand { get; set; }

        public RelayCommand StartDetectingCommand { get; set; }

        public CameraViewModel(IMyShuttleClient myShuttleClient, IApplicationDataRepository applicationDataRepository, DetailsViewModel details)
        {
            _random = new Random((int)DateTime.Now.Ticks);
            MyShuttleClient = myShuttleClient;
            ApplicationDataRepository = applicationDataRepository;
            Details = details;
            DetectingCompletedCommand = new RelayCommand(DetectingCompleted);
            StartDetectingCommand = new RelayCommand(StartDetecting);
        }

        private void StartDetecting()
        {
            IsDetecting = true;
            var vehicleId = ApplicationDataRepository.GetOptionalIntegerFromApplicationData(CameraViewVehicleIdKey, 0);
            loadVehicleTask = MyShuttleClient.VehiclesService.GetAsync(vehicleId.Value);
        }

        private async void DetectingCompleted()
        {
            if (IsDetecting)
            {
                Details.SelectedVehicle = await loadVehicleTask;
            }
        }

        public override void Load()
        {
            IsDetecting = false;
        }

        public override void Update()
        {
            IsDetecting = false;
            Details.SelectedVehicle = null;
        }
    }
}
