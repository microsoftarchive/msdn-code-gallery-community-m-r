using Cirrious.MvvmCross.Plugins.Messenger;
using MyShuttle.Client.Core.DocumentResponse;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Services;
using MyShuttle.Client.Core.ServiceAgents.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyShuttle.Client.Core.ViewModels
{
    public class VehiclesByPriceViewModel : VehiclesViewModelBase, IVehiclesByPriceViewModel
    {
        public VehiclesByPriceViewModel(
            IMyShuttleClient myShuttleClient,
            ILocationServiceSingleton locationService,
            IApplicationSettingServiceSingleton applicationSettingService,
            IMvxMessenger messenger)
            : base(myShuttleClient, locationService, applicationSettingService, messenger)
        {
        }

        protected override async Task LoadFilteredVehiclesAsync()
        {
            FilteredVehicles = new ObservableCollection<Vehicle>(await MyShuttleClient.VehiclesService.GetByPriceAsync(CurrentLocation.Latitude, CurrentLocation.Longitude, ApplicationSettingService.TopListItemsCount));
        }
    }
}
