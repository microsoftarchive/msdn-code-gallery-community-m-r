using System;
using System.Windows.Data;

namespace MyShuttle.Client.Desktop.Converters
{
    public class ToUpperConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value ?? string.Empty).ToString().ToUpperInvariant();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
