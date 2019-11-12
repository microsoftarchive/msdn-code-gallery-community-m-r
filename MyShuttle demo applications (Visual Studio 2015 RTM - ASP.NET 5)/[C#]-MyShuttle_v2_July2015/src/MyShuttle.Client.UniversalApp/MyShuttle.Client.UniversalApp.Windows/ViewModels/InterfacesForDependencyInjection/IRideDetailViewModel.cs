using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.DocumentResponse;

namespace MyShuttle.Client.UniversalApp.ViewModels.InterfacesForDependencyInjection
{
    public interface IRideDetailViewModel : IMvxViewModel
    {
        bool IsLoadingRide { get; }

        Ride Ride { get; set; }
    }
}
