using System;
using Windows.UI.Xaml.Data;

namespace MyShuttle.Client.UniversalApp.Converters
{
    public class MinutesToHumanizedTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var timespan = new TimeSpan(0, (int)value, 0);
            return String.Format("{0}h{1}m",timespan.Hours,timespan.Minutes);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
