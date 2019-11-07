using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.DocumentResponse;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Services;
using MyShuttle.Client.Core.ServiceAgents.Interfaces;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace MyShuttle.Client.UniversalApp.ViewModels
{
    public class CompanyRidesViewModel : MyShuttle.Client.Core.ViewModels.CompanyRidesViewModel
    {

        public CompanyRidesViewModel(IMyShuttleClient myShuttleClient, 
            IApplicationSettingServiceSingleton applicationSettingService,
            IMvxMessenger messenger)
            : base(myShuttleClient, applicationSettingService,messenger)
	    {
            InitializeCommands();
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

    }
}
