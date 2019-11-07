using Chance.MvvmCross.Plugins.UserInteraction;
using Cirrious.MvvmCross.Plugins.PhoneCall;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Services;
using MyShuttle.Client.Core.ServiceAgents.Interfaces;
using MyShuttle.Client.W10.UniversalApp.Model;
using System.Collections.ObjectModel;
using Windows.ApplicationModel;
using Windows.Devices.Geolocation;

namespace MyShuttle.Client.W10.UniversalApp.ViewModels
{
    public class VehicleDetailViewModel : Core.ViewModels.VehicleDetailViewModel
    {
        public VehicleDetailViewModel(IMyShuttleClient myShuttleClient,
            IMvxPhoneCallTask phoneCallTask,
            IUserInteraction userInteraction,
            ILocationServiceSingleton locationService,
            IApplicationSettingServiceSingleton applicationSettingService) : base(myShuttleClient,
                phoneCallTask, userInteraction, locationService, applicationSettingService)
        {

            if (DesignMode.DesignModeEnabled)
            {
                CurrentVehicle = new Core.DocumentResponse.Vehicle
                {
                    Make = "A",
                    Model = "B",
                };
            }

        }
       

        public override void Start()
        {
            InitializeMaps();

            base.Start();
        }

        private void InitializeMaps()
        {
            MapCenter = new Geopoint(new BasicGeoposition()
            {
                Latitude = CurrentLocation.Latitude,
                Longitude = CurrentLocation.Longitude
            });

            Locations = new ObservableCollection<Placemark>
            {
                new Placemark { Geopoint = MapCenter }
            };
        }

        private Geopoint _mapCenter;

        public Geopoint MapCenter
        {
            get
            {
                return _mapCenter;
            }
            set
            {
                _mapCenter = value;
                RaisePropertyChanged(() => MapCenter);
            }
        }

        private ObservableCollection<Placemark> _locations;

        public ObservableCollection<Placemark> Locations
        {
            get
            {
                return _locations;
            }
            set
            {
                _locations = value;
                RaisePropertyChanged(() => Locations);
            }
        }
    }
}
