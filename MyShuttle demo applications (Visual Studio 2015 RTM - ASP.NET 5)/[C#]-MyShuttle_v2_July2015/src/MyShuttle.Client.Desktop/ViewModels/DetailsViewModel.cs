using MyShuttle.Client.Core.DocumentResponse;
using MyShuttle.Client.Core.ServiceAgents.Interfaces;
using System.Threading.Tasks;

namespace MyShuttle.Client.Desktop.ViewModels
{
    public class DetailsViewModel : ObservableViewModel
    {
        private Vehicle _selectedVehicle;
        private bool _isLoadingDriver;
        private Driver _selectedDriver;

        protected IMyShuttleClient MyShuttleClient { get; private set; }

        public StatisticsViewModel Statistics { get; set; }

        public Vehicle SelectedVehicle
        {
            get
            {
                return _selectedVehicle;
            }
            set
            {
                _selectedVehicle = value;
                RaisePropertyChanged();
                UpdateSelectedDriver(_selectedVehicle);
            }
        }

        public Driver SelectedDriver
        {
            get
            {
                return _selectedDriver;
            }
            set
            {
                _selectedDriver = value;
                RaisePropertyChanged();
            }
        }

        public bool IsLoadingDriver
        {
            get
            {
                return _isLoadingDriver;
            }
            set
            {
                _isLoadingDriver = value;
                RaisePropertyChanged();
            }
        }

        public DetailsViewModel(IMyShuttleClient myShuttleClient, StatisticsViewModel statistics)
        {
            MyShuttleClient = myShuttleClient;
            Statistics = statistics;
        }

        private async void UpdateSelectedDriver(Vehicle selectedVehicle)
        {
            SelectedDriver = null;

            if (selectedVehicle == null)
            {
                IsLoadingDriver = false;
                return;
            }

            if (!IsDriverCompleteLoaded(selectedVehicle.Driver))
            {
                IsLoadingDriver = true;
                selectedVehicle.Driver = await MyShuttleClient.DriversService.GetAsync(selectedVehicle.DriverId);
            }

            if (SelectedVehicle == selectedVehicle)
            {
                SelectedDriver = selectedVehicle.Driver;
                IsLoadingDriver = false;
            }
        }

        private bool IsDriverCompleteLoaded(Driver driver)
        {
            return driver != null && driver.Picture != null;
        }
    }
}
