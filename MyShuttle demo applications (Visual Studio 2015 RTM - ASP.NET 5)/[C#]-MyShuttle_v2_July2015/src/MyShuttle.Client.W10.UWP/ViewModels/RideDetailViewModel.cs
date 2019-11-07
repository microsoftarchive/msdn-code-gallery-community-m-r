using Cirrious.MvvmCross.Plugins.Messenger;
using MyShuttle.Client.Core.ServiceAgents.Interfaces;

namespace MyShuttle.Client.W10.UniversalApp.ViewModels
{
    public class RideDetailViewModel : Core.ViewModels.RideDetailViewModel
    {
        public RideDetailViewModel(IMyShuttleClient myShuttleClient,
            IMvxMessenger messenger) : base(myShuttleClient,
                messenger)
        {

        }
    }
}
