namespace MyCompany.Expenses.Client.WP.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;

    /// <summary>
    /// Date to local string converter
    /// </summary>
    public class DateToLocalStringConverter : IValueConverter
    {
        /// <summary>
        /// Convert uppercase string to lowecase.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is DateTime)
            {
                var dateTime = System.Convert.ToDateTime(value).ToLocalTime();
                var c = CultureInfo.CurrentCulture;
                return dateTime.ToString(c.DateTimeFormat.ShortDatePattern);
            }
            return null;
        }

        /// <summary>
        /// Convert back not used.
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
