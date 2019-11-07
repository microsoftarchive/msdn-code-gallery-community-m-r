namespace MyCompany.Expenses.Client.WP.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;

    /// <summary>
    /// remove uppercase from a string.
    /// </summary>
    public class StringToLowerStringConverter : IValueConverter
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
            string UppercaseString = value.ToString();

            if (string.IsNullOrEmpty(UppercaseString))
                return string.Empty;

            return UppercaseString.ToLowerInvariant();
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
