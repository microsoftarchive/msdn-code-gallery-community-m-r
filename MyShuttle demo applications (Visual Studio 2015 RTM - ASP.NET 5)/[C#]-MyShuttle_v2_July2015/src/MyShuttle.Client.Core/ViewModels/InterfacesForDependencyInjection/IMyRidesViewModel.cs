using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.DocumentResponse;
using System.Collections.ObjectModel;

namespace MyShuttle.Client.Core.ViewModels.InterfacesForDependencyInjection
{
        public interface IMyRidesViewModel : IMvxViewModel, IMvxNotifyPropertyChanged
    {
            bool IsLoadingMyLastRides { get; }

            ObservableCollection<Ride> MyLastRides { get; set; }
        }

}
