using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using MyShuttle.Device.Enums;

namespace MyShuttle.Device.Converters
{
    public class DrivingSessionStartedToColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (bool)value;
            var myresourcedictionary = new ResourceDictionary
            {
                Source = new Uri("/MyShuttle.Device;component/Resources/ResourceDictionary.xaml", UriKind.RelativeOrAbsolute)
            };


            var colorBrushKey = val ? "OnColorBrush" : "AlarmColorBrush";
            var colorBrush = myresourcedictionary[colorBrushKey];

            return colorBrush;
        }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}
}
