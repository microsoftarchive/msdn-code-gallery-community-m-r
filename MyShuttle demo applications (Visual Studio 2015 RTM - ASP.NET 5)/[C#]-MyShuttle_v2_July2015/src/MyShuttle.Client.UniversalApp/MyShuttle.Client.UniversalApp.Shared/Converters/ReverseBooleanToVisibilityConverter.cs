using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MyShuttle.Client.UniversalApp.Converters
{
    public class ReverseBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && !((bool)value))
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }
        
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}