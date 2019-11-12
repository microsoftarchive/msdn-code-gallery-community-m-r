using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace MyShuttle.Client.UniversalApp.Converters
{
    /// <summary>
    /// Converter from Boolean to Visibility.
    /// </summary>
    public class ItemClickedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var args = value as ItemClickEventArgs;

            if (args != null)
                return args.ClickedItem;

            return null;
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
