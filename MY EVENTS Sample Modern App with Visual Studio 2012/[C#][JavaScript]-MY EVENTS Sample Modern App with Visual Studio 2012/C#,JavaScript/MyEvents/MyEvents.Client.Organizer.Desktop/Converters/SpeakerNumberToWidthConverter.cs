using System;
using System.Windows.Data;

namespace MyEvents.Client.Organizer.Desktop.Converters
{
    /// <summary>
    /// Converts the width of its item in the Top Speakers list depending on its position
    /// </summary>
    public class SpeakerNumberToWidthConverter: IValueConverter
    {
        /// <summary>
        /// Converts from  position to a width
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int position = (int)value;
            return 80 - position*4;
        }

        /// <summary>
        /// Converts from  position to a width
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
