using System;
using Windows.UI.Xaml.Data;

namespace MyEvents.Client.Organizer.Converters
{
    /// <summary>
    /// Convert initial event time to horizontal offset.
    /// </summary>
    public class TimeToPositionOffsetConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && parameter != null)
            {
                DateTime startTime = (DateTime)value;
                DateTime eventStartTime = (DateTime)parameter;

                double difference = startTime.Subtract(eventStartTime).TotalMinutes;
                return difference * 4;
            }

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
