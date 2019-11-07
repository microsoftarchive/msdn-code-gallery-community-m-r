namespace MyShuttle.Client.W10.UniversalApp.Converters
{
    using System;
    using Windows.UI.Xaml.Data;

    public class DateTimeToTimeConverter :  IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((DateTime)value).ToString("t").ToLower();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
