using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MyShuttle.Client.UniversalApp.Converters
{
    /// <summary>
    /// Converter from Boolean to Visibility.
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool val = System.Convert.ToBoolean(value);
            if (val)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var val = (Visibility)value;
            if (val == Visibility.Visible)
            {
                return true; ;
            }
            return false;
        }
    }
}
