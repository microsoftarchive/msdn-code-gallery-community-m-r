namespace MyCompany.Visitors.Client.UniversalApp.Converters
{
    using System;
    using System.Globalization;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Converter from DateTime to current culture Short Time.
    /// </summary>
    public class DateTimeToFormattedTimeConverter : IValueConverter
    {
        /// <summary>
        /// Converts a DateTime value to a current culture Short Time value.
        /// </summary>
        /// <param name="value">Datetime value</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value is DateTime)
            {
                var dateTime = System.Convert.ToDateTime(value).ToLocalTime();
                var c = CultureInfo.CurrentCulture;
                return dateTime.ToString(c.DateTimeFormat.ShortTimePattern);
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
