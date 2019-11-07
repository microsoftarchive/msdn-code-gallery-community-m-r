using System;
using Cirrious.CrossCore.Converters;
using MyShuttle.Client.Core.Model;
using Android.Gms.Maps.Model;

namespace MyShuttle.Client.Droid.Converters
{
    public class LocationToLatLngConverter : MvxValueConverter<Location, LatLng>
    {
        protected override LatLng Convert(Location value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new LatLng(value.Latitude, value.Longitude);
        }
    }
}