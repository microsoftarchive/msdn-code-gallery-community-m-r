using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.DocumentResponse;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Services;
using MyShuttle.Client.Core.ServiceAgents.Interfaces;
using Cirrious.MvvmCross.Plugins.Messenger;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace MyShuttle.Client.Core.ViewModels
{
    public class VehiclesInMapViewModel : VehiclesViewModelBase, IVehiclesInMapViewModel
    {
        private Vehicle _selectedVehicle;
        private ObservableCollection<Vehicle> _selectedVehicles;

        private readonly ICommand _switchSelectedVehicleCommand;

        public Vehicle SelectedVehicle
        {
            get { return _selectedVehicle; }
            set
            {
                _selectedVehicle = value;
                RaisePropertyChanged(() => SelectedVehicle);
            }
        }

        public ObservableCollection<Vehicle> SelectedVehicles
        {
            get
            {
                return _selectedVehicles;
            }
            set
            {
                _selectedVehicles = value;
                RaisePropertyChanged(() => SelectedVehicles);
            }
        }

        public ICommand SwitchSelectedVehicleCommand
        {
            get { return _switchSelectedVehicleCommand; }
        }

        public VehiclesInMapViewModel(
            IMyShuttleClient myShuttleClient,
            ILocationServiceSingleton locationService,
            IApplicationSettingServiceSingleton applicationSettingService,
            IMvxMessenger messenger)
            : base(myShuttleClient, locationService, applicationSettingService, messenger)
        {
            _switchSelectedVehicleCommand = new MvxCommand<int>(SwitchSelectedVehicle);
        }

        private void SwitchSelectedVehicle(int vehicleId)
        {
            foreach (var vehicle in this.FilteredVehicles)
            {
                vehicle.IsSelected = false;
            }

            this.SelectedVehicle = this.FilteredVehicles.FirstOrDefault(v => v.VehicleId == vehicleId);
            
            if (this.SelectedVehicle != null)
            {
                this.SelectedVehicle.IsSelected = true;

                // This is a "workaround" made in order to raise the vehicles IsSelected property changed to the view.
                var filteredVehiclesAux = this.FilteredVehicles;
                this.FilteredVehicles = null;
                this.FilteredVehicles = filteredVehiclesAux;
            }

            if (this.SelectedVehicles == null)
            {
                this.SelectedVehicles = new ObservableCollection<Vehicle>();
            }
            this.SelectedVehicles.Clear();
            this.SelectedVehicles.Add(this._selectedVehicle);
        }
    }
}
