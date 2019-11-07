using Chance.MvvmCross.Plugins.UserInteraction;
using Cirrious.MvvmCross.Plugins.PhoneCall;
using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Services;
using MyShuttle.Client.Core.ServiceAgents.Interfaces;
using System.Windows.Input;
using Windows.Devices.Geolocation;

namespace MyShuttle.Client.W10.UniversalApp.ViewModels
{
    public class VehicleDetailViewModel : Core.ViewModels.VehicleDetailViewModel
    {
        private bool isVehicleRequested;

        public VehicleDetailViewModel(IMyShuttleClient myShuttleClient,
            IMvxPhoneCallTask phoneCallTask,
            IUserInteraction userInteraction,
            ILocationServiceSingleton locationService,
            IApplicationSettingServiceSingleton applicationSettingService) : base(myShuttleClient,
                phoneCallTask, userInteraction, locationService, applicationSettingService)
        {
            this.InitializeCommands();
        }

        private ICommand _changeCurrentPositionCommand;

        public ICommand ChangeCurrentPositionCommand
        {
            get { return this._changeCurrentPositionCommand; }
        }

        private void InitializeCommands()
        {
            this._changeCurrentPositionCommand = new MvxCommand<Geopoint>(this.ChangeCurrentPosition);
        }

        private void ChangeCurrentPosition(Geopoint geoPoint)
        {
            if (!isVehicleRequested) return;

            CurrentLocation = new Core.Model.Location { Latitude = geoPoint.Position.Latitude, Longitude = geoPoint.Position.Longitude };
        }

        protected override void OnVehicleRequested()
        {
            isVehicleRequested = true;
        }
    }
}
