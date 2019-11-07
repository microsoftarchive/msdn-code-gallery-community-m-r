using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.ViewModels.InterfacesForDependencyInjection;
using System.Windows.Input;

namespace MyShuttle.Client.W10.UniversalApp.ViewModels
{
    public class MyRidesViewModel : MvxViewModel
    {
        public IMyRidesViewModel MyRidesVM { get; private set; }
        public ICommand NavigateToRideDetailsCommand { get; set; }


        public MyRidesViewModel(
            IMyRidesViewModel myRidesViewModel)
        {
            MyRidesVM = myRidesViewModel;

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            NavigateToRideDetailsCommand = new MvxCommand<int>(NavigateToRideDetails);
        }

        private void NavigateToRideDetails(int currentRideId)
        {
            ShowViewModel<RideDetailViewModel>(new { currentRideId = currentRideId });
        }
    }
}
