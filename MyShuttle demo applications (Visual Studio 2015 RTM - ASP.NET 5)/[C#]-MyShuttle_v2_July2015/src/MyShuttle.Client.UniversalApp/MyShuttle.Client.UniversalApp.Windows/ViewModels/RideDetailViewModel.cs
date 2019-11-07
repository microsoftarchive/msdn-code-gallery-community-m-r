using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.ServiceAgents.Interfaces;
using MyShuttle.Client.UniversalApp.ConnectedServices;
using System;
using System.Windows.Input;

namespace MyShuttle.Client.UniversalApp.ViewModels
{
    public class RideDetailViewModel : Core.ViewModels.RideDetailViewModel
    {
        private ICommand _downloadInvoiceCommand;
        public RideDetailViewModel(IMyShuttleClient myShuttleClient, IMvxMessenger messenger)
            : base(myShuttleClient, messenger)
	    {
            InitializeCommands();
	    }

        public ICommand DownloadInvoiceCommand
        {
            get { return this._downloadInvoiceCommand; }
        }

        private void InitializeCommands()
        {
            _downloadInvoiceCommand = new MvxCommand(DownloadInvoice);
        }

        private async  void DownloadInvoice()
        {
            this.IsLoadingRide = true;
            var _fileOperations = new InvoiceService();

            var file = await _fileOperations.GetFile(this.Ride.EmployeeId);
            if (file != null)
            {
                var launcher = Windows.System.Launcher.LaunchFileAsync(file);
            }

            this.IsLoadingRide = false;
        }

    }
}
