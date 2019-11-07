using System;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Data;
using MyShuttle.Client.Core.DocumentResponse;

namespace MyShuttle.Client.UniversalApp.Converters
{
    public class VehicleToGeopointConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var vehicleToConvert = (Vehicle)value;

            if (vehicleToConvert == null)
            {
                return null;
            }

            return new Geopoint(new BasicGeoposition
            {
                Latitude = vehicleToConvert.Latitude,
                Longitude = vehicleToConvert.Longitude
            });
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
