namespace MyCompany.Expenses.Client.WP.Services.Location
{
    using Microsoft.Phone.Maps.Services;
    using MyCompany.Expenses.Client.WP.Model;
    using System;
    using System.Collections.Generic;
    using System.Device.Location;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.Devices.Geolocation;

    /// <summary>
    /// Service contract implementation for location aware tasks.
    /// </summary>
    public class LocationService : ILocationService
    {
        Geolocator locator;
        /// <summary>
        /// Get the current user position.
        /// </summary>
        /// <returns></returns>
        public async Task<GeoCoordinate> GetCurrentPosition()
        {
            InitializeLocator();

            Geoposition location = await this.locator.GetGeopositionAsync(TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(10));

            if (location != null)
            {
                return new GeoCoordinate(location.Coordinate.Latitude, location.Coordinate.Longitude);
            }

            return new GeoCoordinate(0, 0);
        }

        /// <summary>
        /// Start position tracking every x meters even when the app is in background.
        /// </summary>
        public void StartBackgroundPositionTracking()
        {
            InitializeLocator();
            this.locator.PositionChanged += locator_PositionChanged;
        }

        private void locator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            if (args.Position != null)
            {
                if (CurrentPositionChanged != null)
                    CurrentPositionChanged(this, new EventArgs<GeoCoordinate>(new GeoCoordinate(args.Position.Coordinate.Latitude, args.Position.Coordinate.Longitude)));
            }
        }

        /// <summary>
        /// Stop position tracking.
        /// </summary>
        public void StopBackgroundPositionTracking()
        {
            if (this.locator != null)
            {
                this.locator.PositionChanged -= locator_PositionChanged;
                this.locator = null;
            }
        }

        /// <summary>
        /// Get a city name from given coordinates.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public Task<string> GetCityFromLocation(double latitude, double longitude)
        {
            TaskCompletionSource<string> taskComplete = new TaskCompletionSource<string>();

            ReverseGeocodeQuery query = new ReverseGeocodeQuery()
            {
                GeoCoordinate = new GeoCoordinate(latitude, longitude)
            };

            query.QueryCompleted += (s, e) =>
            {
                if (e.Error == null)
                {
                    taskComplete.SetResult(e.Result.First().Information.Address.City);
                }
            };

            query.QueryAsync();

            return taskComplete.Task;
        }

        private void InitializeLocator()
        {
            if (this.locator == null)
            {
                this.locator = new Geolocator()
                {
                    DesiredAccuracy = PositionAccuracy.High,
                    DesiredAccuracyInMeters = 1,
                    MovementThreshold = 50,
                    ReportInterval = 5000,
                };
            }
        }

        /// <summary>
        /// This event is raised every time the position changed.
        /// </summary>
        public event EventHandler<EventArgs<GeoCoordinate>> CurrentPositionChanged;
    }
}
