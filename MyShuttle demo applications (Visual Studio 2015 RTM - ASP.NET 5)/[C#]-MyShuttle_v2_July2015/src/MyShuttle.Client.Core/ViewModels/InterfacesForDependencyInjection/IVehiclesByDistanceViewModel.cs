using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.DocumentResponse;
using System.Collections.ObjectModel;

namespace MyShuttle.Client.Core.ViewModels
{
    public interface IVehiclesByDistanceViewModel : IMvxViewModel, IMvxNotifyPropertyChanged
    {
        ObservableCollection<Vehicle> FilteredVehicles { get; set; }
    }
}
