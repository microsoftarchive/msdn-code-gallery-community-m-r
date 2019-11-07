using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using MyShuttle.Device.Enums;

namespace MyShuttle.Device.Converters
{
    public class SessionStatusToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (SessionStatus)value;
            var param = (SessionStatus)(int.Parse(parameter.ToString()));

            return (val == param) ? Visibility.Visible : Visibility.Hidden;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
