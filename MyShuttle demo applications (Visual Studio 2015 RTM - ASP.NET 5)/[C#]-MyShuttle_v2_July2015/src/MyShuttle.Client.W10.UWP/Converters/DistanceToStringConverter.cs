namespace MyShuttle.Client.W10.UniversalApp.Converters
{
    using System;
    using Windows.UI.Xaml.Data;

    public class DistanceToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var vehicleStatus = (double)value;

            return string.Format("{0:0.00}", value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new System.NotImplementedException();
        }
    }
}
