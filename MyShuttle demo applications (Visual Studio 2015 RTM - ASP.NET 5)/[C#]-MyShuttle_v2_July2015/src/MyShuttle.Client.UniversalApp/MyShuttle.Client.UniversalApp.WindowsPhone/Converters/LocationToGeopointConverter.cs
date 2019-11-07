using System;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Data;
using MyShuttle.Client.Core.Model;

namespace MyShuttle.Client.UniversalApp.Converters
{
    public class LocationToGeopointConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var locationToConvert = (Location)value;

            return new Geopoint(new BasicGeoposition
            {
                Latitude = locationToConvert.Latitude,
                Longitude = locationToConvert.Longitude
            });
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
