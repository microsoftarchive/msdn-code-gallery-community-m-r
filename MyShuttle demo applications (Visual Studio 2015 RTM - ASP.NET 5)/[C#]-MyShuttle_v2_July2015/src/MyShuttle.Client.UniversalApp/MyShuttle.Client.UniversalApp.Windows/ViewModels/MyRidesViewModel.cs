using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Services;
using MyShuttle.Client.Core.ServiceAgents.Interfaces;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace MyShuttle.Client.UniversalApp.ViewModels
{
    public class MyRidesViewModel : Core.ViewModels.MyRidesViewModel
    {
        public MyRidesViewModel(
            IMyShuttleClient myShuttleClient,
            IApplicationSettingServiceSingleton applicationSettingService,
            IMvxMessenger messenger)
            : base(myShuttleClient, applicationSettingService, messenger)
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            this._navigateToRideDetailsCommand = new MvxCommand<int>(NavigateToRideDetailsW8);
        }

        private void NavigateToRideDetailsW8(int currentRideId)
        {
            ShowViewModel<RideDetailViewModel>(new { currentRideId = currentRideId });
        }
    }

}
