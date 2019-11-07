using Microsoft.AspNet.SignalR.Client;
using MyShuttle.Client.Core.Settings;
using MyShuttle.Client.W10.UniversalApp.ViewModels;
using MyShuttle.Client.W10.UniversalApp.Views.Base;
using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;

namespace MyShuttle.Client.W10.UniversalApp.Views
{
    public sealed partial class VehicleDetailPage : WindowsBasePage
    {

        private HubConnection _hubConnection;
        private IHubProxy _proxy;

        public VehicleDetailPage()
        {
            this.InitializeComponent();
            InitializeNotifications();
        }

        private async void InitializeNotifications()
        {
            _hubConnection = new HubConnection(CommonAppSettings.SignalRUrl);
            _proxy = _hubConnection.CreateHubProxy("MyShuttleHub");
            await _hubConnection.Start();
        }

        private async void MapControl_MapTapped(MapControl sender, MapInputEventArgs args)
        {
            var userGeopoint = new Geopoint(new BasicGeoposition
            {
                Latitude = args.Location.Position.Latitude,
                Longitude = args.Location.Position.Longitude
            });

            var vm = this.DataContext as VehicleDetailViewModel;

            vm.ChangeCurrentPositionCommand.Execute(userGeopoint);

            // To avoid massive notifications events.
            if (_hubConnection.State == ConnectionState.Reconnecting)
                await Task.Delay(TimeSpan.FromSeconds(2));

            await _proxy.Invoke("UpdateEmployeePosition", CommonAppSettings.FixedEmployeeId, args.Location.Position.Latitude, args.Location.Position.Longitude);
        }
    }
}
