namespace MyCompany.Expenses.Client.WP.Services.Location
{
    using MyCompany.Expenses.Client.WP.Model;
    using System;
    using System.Collections.Generic;
    using System.Device.Location;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Service contract for location aware tasks.
    /// </summary>
    public interface ILocationService
    {
        /// <summary>
        /// Get the current user position.
        /// </summary>
        /// <returns></returns>
        Task<GeoCoordinate> GetCurrentPosition();

        /// <summary>
        /// Start position tracking every x meters even when the app is in background.
        /// </summary>
        void StartBackgroundPositionTracking();

        /// <summary>
        /// Stop position tracking.
        /// </summary>
        void StopBackgroundPositionTracking();

        /// <summary>
        /// Get a city name from given coordinates.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        Task<string> GetCityFromLocation(double latitude, double longitude);

        /// <summary>
        /// This event is raised every time the position changed.
        /// </summary>
        event EventHandler<EventArgs<GeoCoordinate>> CurrentPositionChanged;
    }
}
