using System;

namespace MyShuttle.Client.Core.Model
{
    public class LocationCalculatedEventArgs : EventArgs
    {
        public Location CalculatedLocation { get; private set; }
        
        public LocationCalculatedEventArgs(Location location)
        {
            CalculatedLocation = location;
        }
    }
}
