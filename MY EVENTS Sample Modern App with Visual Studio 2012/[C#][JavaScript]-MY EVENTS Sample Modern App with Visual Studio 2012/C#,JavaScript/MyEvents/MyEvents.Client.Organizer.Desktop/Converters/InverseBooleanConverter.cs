using System;
using System.Windows.Data;

namespace MyEvents.Client.Organizer.Desktop.Converters
{
    /// <summary>
    /// Returns an inversed boolean
    /// </summary>
    public class InverseBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Convert true to false, false to true.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool val = false;

            if (value != null)
            {
                bool.TryParse(value.ToString(), out val);

                return !val;
            }

            return val;
        }

        /// <summary>
        /// Not used, inverse boolean.
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
