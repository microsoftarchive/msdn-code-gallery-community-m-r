using System.Collections.ObjectModel;
using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.DocumentResponse;
using MyShuttle.Client.UniversalApp.Model;

namespace MyShuttle.Client.Core.ViewModels.InterfacesForDependencyInjection
{
    public interface ICompanyRidesViewModel : IMvxViewModel
    {
        bool IsLoadingLastCompanyRides { get; }

        ObservableCollection<GroupRide> LastCompanyRidesGrouped { get; set; }
    }
}
