using MyShuttle.Client.Core.DocumentResponse;
using MyShuttle.Client.Core.ServiceAgents.Interfaces;
using MyShuttle.Client.Desktop.Infrastructure;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyShuttle.Client.Desktop.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private bool _isLoadingVehicles;
        private ObservableCollection<Vehicle> _vehicles;

        protected IMyShuttleClient MyShuttleClient { get; private set; }

        public RelayCommand RefreshCommand { get; set; }

        public DetailsViewModel Details { get; set; }
        
        public ObservableCollection<Vehicle> Vehicles
        {
            get
            {
                return _vehicles;
            }
            set
            {
                _vehicles = value;
                RaisePropertyChanged();
            }
        }

        public bool IsLoadingVehicles
        {
            get
            {
                return _isLoadingVehicles;
            }
            set
            {
                _isLoadingVehicles = value;
                RaisePropertyChanged();
                RefreshCommand.RaiseCanExecutedChanged();
            }
        }

        public MainViewModel(IMyShuttleClient myShuttleClient, DetailsViewModel details)
        {
            MyShuttleClient = myShuttleClient;
            Details = details;
            RefreshCommand = new RelayCommand(Refresh, CanRefresh);
        }

        private async void Refresh()
        {
            await LoadVehiclesAsync();
        }

        private bool CanRefresh()
        {
            return !IsLoadingVehicles;
        }

        protected async virtual Task LoadVehiclesAsync()
        {
            IsLoadingVehicles = true;
            Vehicles = null;

            var vehiclesCount = await MyShuttleClient.VehiclesService.GetCountAsync(string.Empty);
            Vehicles = new ObservableCollection<Vehicle>(Enumerable.Range(0, vehiclesCount).Select(x => new Vehicle()));

            if (vehiclesCount > 0)
            {
                var vehicles = await MyShuttleClient.VehiclesService.GetAsync(string.Empty, vehiclesCount, 0);
                Vehicles = new ObservableCollection<Vehicle>(vehicles);
                Details.SelectedVehicle = Vehicles.FirstOrDefault();
            }

            IsLoadingVehicles = false;
        }

        public override async void Load()
        {
            await LoadVehiclesAsync();
        }

        public override void Update()
        {

        }
    }
}
