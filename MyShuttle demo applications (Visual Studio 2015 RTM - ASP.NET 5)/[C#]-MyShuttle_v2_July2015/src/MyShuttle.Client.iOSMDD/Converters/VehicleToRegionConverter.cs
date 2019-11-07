using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.CrossCore.Converters;
using MonoTouch.MapKit;
using MonoTouch.CoreLocation;
using MyShuttle.Client.Core.ViewModels;
using MyShuttle.Client.Core.DocumentResponse;
using MyShuttle.Client.Core.Model;

namespace MyShuttle.Client.iOS.Converters
{
    public class VehicleToRegionConverter : MvxValueConverter<Vehicle, MKCoordinateRegion>
    {
        protected override MKCoordinateRegion Convert(Vehicle value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return new MKCoordinateRegion();
            }

            var location = new CLLocationCoordinate2D(value.Latitude, value.Longitude);

            var returnRegion = new MKCoordinateRegion 
            { 
                Center = location,
                Span = new MKCoordinateSpan(MapSettings.LatitudeLongitudeDelta, MapSettings.LatitudeLongitudeDelta) 
            };

            return returnRegion;
        }
    }
}