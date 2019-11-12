using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.DocumentResponse;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Services;
using MyShuttle.Client.Core.ServiceAgents.Interfaces;
using System;
using System.Collections.ObjectModel;
using MyShuttle.Client.Core.ViewModels.InterfacesForDependencyInjection;
using Cirrious.MvvmCross.Plugins.Messenger;
using MyShuttle.Client.Core.Messages;

namespace MyShuttle.Client.Core.ViewModels
{
    public class MyRidesViewModel : MvxViewModel, IMyRidesViewModel
    {
        private readonly IMyShuttleClient _myShuttleClient;
        private readonly IApplicationSettingServiceSingleton _applicationSettingService;
        private readonly IMvxMessenger _messenger;
        private readonly MvxSubscriptionToken _token;

        private bool _isLoadingMyLastRides;
        private ObservableCollection<Ride> _myLastRides;

        public ICommand _navigateToRideDetailsCommand;
        public ICommand _navigateToRideDetailsAlternativeCommand;

        public bool IsLoadingMyLastRides
        {
            get
            {
                return _isLoadingMyLastRides;
            }
            private set
            {
                _isLoadingMyLastRides = value;
                RaisePropertyChanged(() => IsLoadingMyLastRides);
            }
        }

        public ObservableCollection<Ride> MyLastRides
        {
            get
            {
                return _myLastRides;
            }
            set
            {
                _myLastRides = value;
                RaisePropertyChanged(() => MyLastRides);
            }
        }

        public ICommand NavigateToRideDetailsCommand
        {
            get { return _navigateToRideDetailsCommand; }
        }

        public ICommand NavigateToRideDetailsAlternativeCommand
        {
            get { return _navigateToRideDetailsAlternativeCommand; }
        }

        public MyRidesViewModel(
            IMyShuttleClient myShuttleClient, 
            IApplicationSettingServiceSingleton applicationSettingService,
            IMvxMessenger messenger)
        {
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

            _myShuttleClient = myShuttleClient;
            _applicationSettingService = applicationSettingService;
            _messenger = messenger;

            _token = _messenger.Subscribe<ReloadDataMessage>(_ => InitializeActions());
            
            InitializeActions();

            InitializeCommands();
        }

        private async void InitializeActions()
        {
            IsLoadingMyLastRides = true;

            try
            {
                 this.MyLastRides = new ObservableCollection<Ride>(await _myShuttleClient.RidesService.GetMyRidesAsync(_applicationSettingService.TopListItemsCount));

            }
            catch
            {
                this.MyLastRides = new ObservableCollection<Ride>();
            }
            
            IsLoadingMyLastRides = false;
        }

        private void InitializeCommands()
        {
            _navigateToRideDetailsCommand = new MvxCommand<int>(NavigateToRideDetails);
            _navigateToRideDetailsAlternativeCommand = new MvxCommand<Ride>(ride => this.NavigateToRideDetails(ride.RideId));
        }

        private void NavigateToRideDetails(int currentRideId)
        {
            ShowViewModel<RideDetailViewModel>(new { currentRideId = currentRideId });
        }
    }

}
