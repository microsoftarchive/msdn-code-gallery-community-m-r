using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using MyShuttle.Device.Enums;

namespace MyShuttle.Device.Converters
{
    public class SessionStatusToColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (SessionStatus)value;
            var myresourcedictionary = new ResourceDictionary
            {
                Source = new Uri("/MyShuttle.Device;component/Resources/ResourceDictionary.xaml", UriKind.RelativeOrAbsolute)
            };

            var colorBrush = myresourcedictionary["OffColorBrush"];

            switch (val)
            {
                case SessionStatus.GoodDriver:
                    colorBrush =  myresourcedictionary["OnColorBrush"];
                    break;

                case SessionStatus.BadDriver:
                    colorBrush = myresourcedictionary["BadColorBrush"];
                    break;

                case SessionStatus.CarCrash:
                    colorBrush = myresourcedictionary["AlarmColorBrush"];
                    break;
            }

            return colorBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
