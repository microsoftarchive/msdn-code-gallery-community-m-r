using Cirrious.MvvmCross.Plugins.Messenger;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Services;
using MyShuttle.Client.Core.ServiceAgents.Interfaces;

namespace MyShuttle.Client.Core.ViewModels
{
    public class VehiclesByDistanceViewModel : VehiclesViewModelBase, IVehiclesByDistanceViewModel
    {
        public VehiclesByDistanceViewModel(
            IMyShuttleClient myShuttleClient,
            ILocationServiceSingleton locationService,
            IApplicationSettingServiceSingleton applicationSettingService,
            IMvxMessenger messenger)
            : base(myShuttleClient, locationService, applicationSettingService, messenger)
        {
        }
    }
}
