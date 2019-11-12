using System;
using System.Windows.Data;
using MyEvents.Client.Organizer.Desktop.ViewModel;

namespace MyEvents.Client.Organizer.Desktop.Converters
{
    /// <summary>
    /// Convert time to position offset.
    /// </summary>
    public class TimeToPositionConverter : IValueConverter
    {
        /// <summary>
        /// time to offset
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                ViewModelLocator loc = (ViewModelLocator)App.Current.Resources["Locator"];
                DateTime startTime = (DateTime)value;
                DateTime eventStartTime = loc.EventSchedule.Event.StartTime;

                double difference = startTime.Subtract(eventStartTime).TotalMinutes;
                return (difference * 2);

            }

            return 0;
        }

        /// <summary>
        /// offset to time
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
