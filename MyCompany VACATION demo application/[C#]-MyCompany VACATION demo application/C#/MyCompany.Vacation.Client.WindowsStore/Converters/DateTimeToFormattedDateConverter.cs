using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace MyCompany.Vacation.Client.WindowsStore.Converters
{
    /// <summary>
    /// Converter from DateTime to current culture Short Date.
    /// </summary>
    public class DateTimeToFormattedDateConverter : IValueConverter
    {
        /// <summary>
        /// Converts a DateTime value to a current culture Short Date value.
        /// </summary>
        /// <param name="value">DateTime value</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime)
            {
                var dateTime = System.Convert.ToDateTime(value).ToLocalTime();
                var c = CultureInfo.CurrentCulture;
                return dateTime.ToString("dd, MMM");
            }
            return null;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
