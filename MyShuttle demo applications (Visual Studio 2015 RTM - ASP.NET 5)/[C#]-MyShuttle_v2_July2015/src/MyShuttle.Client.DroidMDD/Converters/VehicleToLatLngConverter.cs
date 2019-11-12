using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cirrious.CrossCore.Converters;
using MyShuttle.Client.Core.Model;
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