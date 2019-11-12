using System;
using System.Globalization;
using System.Windows.Data;


namespace MyShuttle.Device.Converters
{
    public class BoolToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var sessionStarted = (bool)value;

            var userImageUri = new Uri("/MyShuttle.Device;component/Assets/user_photo.png",
                UriKind.RelativeOrAbsolute);

            var unknowUserImageUri = new Uri("/MyShuttle.Device;component/Assets/user_unknow.png",
                UriKind.RelativeOrAbsolute);

            return sessionStarted ? userImageUri : unknowUserImageUri;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
