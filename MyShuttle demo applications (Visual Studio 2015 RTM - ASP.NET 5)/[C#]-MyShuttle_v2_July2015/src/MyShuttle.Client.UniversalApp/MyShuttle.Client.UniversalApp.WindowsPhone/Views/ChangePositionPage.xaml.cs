using System.ComponentModel;
using Windows.Devices.Geolocation;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Cirrious.CrossCore;
using Microsoft.AspNet.SignalR.Client;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Services;
using MyShuttle.Client.Core.Model;
using MyShuttle.Client.Core.Settings;
using MyShuttle.Client.UniversalApp.Views.Base;
using System.Diagnostics;
using System;
using System.Threading.Tasks;

namespace MyShuttle.Client.UniversalApp.Views
{
    public sealed partial class ChangePositionPage : Page, INotifyPropertyChanged
    {
        private readonly ILocationServiceSingleton _locationService;
        private readonly IApplicationSettingServiceSingleton _applicationSettingService;
        private Geopoint _userGeopoint;
        private Geopoint _mapCenterLocation;
        private IHubProxy _proxy;
        private HubConnection _hubConnection;

        public ChangePositionPage()
        {
            this.InitializeComponent();

            _locationService = Mvx.GetSingleton<ILocationServiceSingleton>();
            _applicationSettingService = Mvx.GetSingleton<IApplicationSettingServiceSingleton>();

            InitMap();
            InitializeNotifications();

            this.Loaded += (sender, e) =>
            {
                HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            };

            this.Unloaded += (sender, e) =>
            {
                HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            };
        }

        public Geopoint UserGeopoint
        {
            get { return _userGeopoint; }
            set
            {
                _userGeopoint = value;
                this.OnPropertyChanged("UserGeopoint");
            }
        }

        public Geopoint MapCenterLocation
        {
            get { return _mapCenterLocation; }
            set
            {
                _mapCenterLocation = value;
                this.OnPropertyChanged("MapCenterLocation");
            }
        }

        private async void InitializeNotifications()
        {
            _hubConnection = new HubConnection(CommonAppSettings.SignalRUrl);
            _proxy = _hubConnection.CreateHubProxy("MyShuttleHub");
            await _hubConnection.Start();
        }

        private async void InitMap()
        {
            MapControl.MapServiceToken = _applicationSettingService.BingMapsToken;            

            Location location = await _locationService.CalculatePositionAsync();

            var userGeopoint = new Geopoint(new BasicGeoposition
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            });

            UserGeopoint = userGeopoint;
            MapCenterLocation = userGeopoint;
        }

        private async void MapControl_OnMapTapped(MapControl sender, MapInputEventArgs args)
        {
            UserGeopoint = new Geopoint(new BasicGeoposition
            {
                Latitude = args.Location.Position.Latitude,
                Longitude = args.Location.Position.Longitude
            });

            // To avoid massive notifications events.
            if(_hubConnection.State == ConnectionState.Reconnecting)
                await Task.Delay(TimeSpan.FromSeconds(2));

            await _proxy.Invoke("UpdateEmployeePosition", CommonAppSettings.FixedEmployeeId, args.Location.Position.Latitude, args.Location.Position.Longitude);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame == null)
            {
                return;
            }

            if (frame.CanGoBack)
            {
                frame.GoBack();
                e.Handled = true;
            }
        }
    }
}
