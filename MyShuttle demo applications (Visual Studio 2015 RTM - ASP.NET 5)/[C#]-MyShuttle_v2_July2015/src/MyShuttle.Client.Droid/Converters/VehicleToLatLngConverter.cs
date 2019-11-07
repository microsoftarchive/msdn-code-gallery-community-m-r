using System;
using Cirrious.CrossCore.Converters;
using Android.Gms.Maps.Model;
using MyShuttle.Client.Core.DocumentResponse;

namespace MyShuttle.Client.Droid.Converters
{
    public class VehicleToLatLngConverter : MvxValueConverter<Vehicle, LatLng>
    {
        protected override LatLng Convert(Vehicle value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new LatLng(value.Latitude, value.Longitude);
        }
    }
}