using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.DocumentResponse;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Services;
using MyShuttle.Client.Core.ServiceAgents.Interfaces;
using System;
using System.Collections.ObjectModel;
using MyShuttle.Client.Core.ViewModels.InterfacesForDependencyInjection;
using MyShuttle.Client.Core.ViewModels;
using System.Collections.Generic;
using MyShuttle.Client.UniversalApp.Model;
using Cirrious.MvvmCross.Plugins.Messenger;
using MyShuttle.Client.Core.Messages;

namespace MyShuttle.Client.Core.ViewModels
{
    public class CompanyRidesViewModel : MvxViewModel, ICompanyRidesViewModel
    {
        private readonly IMyShuttleClient _myShuttleClient;
        private readonly IApplicationSettingServiceSingleton _applicationSettingService;
        private readonly IMvxMessenger _messenger;
        private readonly MvxSubscriptionToken _token;

        private bool _isLoadingLastCompanyRides;
        private ObservableCollection<GroupRide> _lastCompanyRidesGrouped;
        private ObservableCollection<Ride> _companyRides; 

        public ICommand _navigateToRideDetailsCommand;
        public ICommand _navigateToRideDetailsAlternativeCommand;

        public bool IsLoadingLastCompanyRides
        {
            get
            {
                return _isLoadingLastCompanyRides;
            }
            private set
            {
                _isLoadingLastCompanyRides = value;
                RaisePropertyChanged(() => IsLoadingLastCompanyRides);
            }
        }

        public ObservableCollection<GroupRide> LastCompanyRidesGrouped
        {
            get
            {
                return _lastCompanyRidesGrouped;
            }
            set
            {
                _lastCompanyRidesGrouped = value;
                RaisePropertyChanged(() => LastCompanyRidesGrouped);
            }
        }

        public ObservableCollection<Ride> CompanyRides
        {
            get { return _companyRides; }
            set
            {
                _companyRides = value;
                RaisePropertyChanged(() => CompanyRides);
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

        public CompanyRidesViewModel(IMyShuttleClient myShuttleClient, 
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

        private void InitializeActions()
        {
            IsLoadingLastCompanyRides = true;
            LoadLastCompanyRidesAsync();
        }

        private void InitializeCommands()
        {
            _navigateToRideDetailsCommand = new MvxCommand<int>(NavigateToRideDetails);
            _navigateToRideDetailsAlternativeCommand = new MvxCommand<Ride>(ride =>
                this.NavigateToRideDetails(ride.RideId));
        }

        private void NavigateToRideDetails(int rideId)
        {
            ShowViewModel<RideDetailViewModel>(new { currentRideId = rideId });
        }

        private async void LoadLastCompanyRidesAsync()
        {
            CompanyRides = new ObservableCollection<Ride>(await _myShuttleClient.RidesService.GetCompanyRidesAsync(_applicationSettingService.TopListItemsCount));
            var groupRides = new GroupRide(CompanyRides);
            this.LastCompanyRidesGrouped = new ObservableCollection<GroupRide> { groupRides };     
            
            IsLoadingLastCompanyRides = false;
        }
    }
}
