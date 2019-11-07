using System;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.DocumentResponse;
using MyShuttle.Client.Core.Model.Enums;
using MyShuttle.Client.Core.ServiceAgents.Interfaces;
using MyShuttle.Client.Core.ViewModels.Base;
using System.Windows.Input;

namespace MyShuttle.Client.Core.ViewModels
{
    public class RideDetailViewModel : NavegableViewModel
    {
        private readonly IMyShuttleClient _myShuttleClient;
        private readonly IMvxMessenger _messenger;
        //private readonly MvxSubscriptionToken _token;
        private RideDetailWorkflow _rideDetailWorkflowstate;

        private bool _isLoadingRide;
        private Ride _ride;

        private ICommand _showResumeCommand;
        private ICommand _showRateCommand;
        private ICommand _saveRideRateCommand;

        public bool IsLoadingRide
        {
            get
            {
                return _isLoadingRide;
            }
            set
            {
                _isLoadingRide = value;
                RaisePropertyChanged(() => IsLoadingRide);
            }
        }

        public Ride Ride
        {
            get
            {
                return _ride;
            }
            set
            {
                _ride = value;
                RaisePropertyChanged(() => Ride);
            }
        }

        public RideDetailWorkflow RideDetailWorkflowState
        {
            get
            {
                return _rideDetailWorkflowstate;
            }
            private set
            {
                _rideDetailWorkflowstate = value;
                this.RaisePropertyChanged(() => RideDetailWorkflowState);
            }
        }

        public ICommand ShowResumeCommand
        {
            get { return this._showResumeCommand; }
        }

        public ICommand ShowRateCommand
        {
            get { return this._showRateCommand; }
        }

        public ICommand SaveRideRateCommand
        {
            get { return this._saveRideRateCommand; }
        }

        public RideDetailViewModel(
            IMyShuttleClient myShuttleClient,
            IMvxMessenger messenger)
        {
            if (myShuttleClient == null)
            {
                throw new ArgumentNullException("myShuttleClient");
            }

            if (messenger == null)
            {
                throw new ArgumentNullException("messenger");
            }

            _myShuttleClient = myShuttleClient;
            _messenger = messenger;

            //_token = _messenger.Subscribe<ReloadDataMessage>(_ => InitializeActions());
            //InitializeActions();

            InitializeCommands();
        }

        public async void Init(int currentRideId)
        {
            IsLoadingRide = true;

            try
            {
                this.Ride = await this._myShuttleClient.RidesService.GetAsync(currentRideId);
            }
            catch
            {
                this.Ride = new Ride();
            }

            IsLoadingRide = false;
        }

        private void InitializeCommands()
        {
            _showResumeCommand = new MvxCommand(this.ShowResume);
            _showRateCommand = new MvxCommand(this.ShowRate);
            _saveRideRateCommand = new MvxCommand(SaveRideRateAsync);
        }

        private void ShowResume()
        {
            ChangeRideDetailWorkflow(RideDetailWorkflow.Resume);
        }

        private void ShowRate()
        {
            ChangeRideDetailWorkflow(RideDetailWorkflow.Rate);
        }
        
        private async void SaveRideRateAsync()
        {
            this.IsLoadingRide = true;

            var newRideToUpdate = CreateNewPlainRide();

            await this._myShuttleClient.RidesService.PutAsync(newRideToUpdate);

            this.IsLoadingRide = false;
        }

        private Ride CreateNewPlainRide()
        {
            var newRideToUpdate = new Ride
            {
                RideId = this.Ride.RideId,
                StartDateTime = this.Ride.StartDateTime,
                EndDateTime = this.Ride.EndDateTime,
                StartLatitude = this.Ride.StartLatitude,
                StartLongitude = this.Ride.StartLongitude,
                EndLatitude = this.Ride.EndLatitude,
                EndLongitude = this.Ride.EndLongitude,
                StartAddress = this.Ride.StartAddress,
                EndAddress = this.Ride.EndAddress,
                Distance = this.Ride.Distance,
                Duration = this.Ride.Duration,
                Cost = this.Ride.Cost,
                Signature = this.Ride.Signature,
                Rating = this.Ride.Rating,
                Comments = this.Ride.Comments,
                VehicleId = this.Ride.VehicleId,
                DriverId = this.Ride.DriverId,
                EmployeeId = this.Ride.EmployeeId,
                Id = this.Ride.Id,
                CarrierId = this.Ride.CarrierId
            };
            return newRideToUpdate;
        }

        private void ChangeRideDetailWorkflow(RideDetailWorkflow rideDetailWorkflowToSet)
        {
            this.RideDetailWorkflowState = rideDetailWorkflowToSet;
        }
    }
}
