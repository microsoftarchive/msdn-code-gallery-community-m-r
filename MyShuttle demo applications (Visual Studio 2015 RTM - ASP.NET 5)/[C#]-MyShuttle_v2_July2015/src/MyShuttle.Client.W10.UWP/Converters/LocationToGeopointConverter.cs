namespace MyShuttle.Client.W10.UniversalApp.Converters
{
    using System;
    using Windows.Devices.Geolocation;
    using Windows.UI.Xaml.Data;
    using Core.Model;

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
