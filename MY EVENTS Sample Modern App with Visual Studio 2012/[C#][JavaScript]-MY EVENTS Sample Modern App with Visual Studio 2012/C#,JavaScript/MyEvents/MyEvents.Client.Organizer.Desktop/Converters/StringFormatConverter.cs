using System;
using System.Windows.Data;

namespace MyEvents.Client.Organizer.Desktop.Converters
{
    /// <summary>
    /// String converter to format dates based on conventions
    /// </summary>
    public class FormatStringConverter : IValueConverter
    {
        /// <summary>
        /// Convert a string based on the parameter
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            if (value.ToString() == "0")
                return value.ToString();

            if (parameter.ToString().Contains("{Date}"))
            {
                DateTime dt;
                DateTime.TryParse(value.ToString(), out dt);

                return dt.Date.ToString("dd MMMM yyyy");
            }
            if (parameter.ToString().Contains("{ShortDate}"))
            {
                DateTime dt;
                DateTime.TryParse(value.ToString(), out dt);

                return dt.Date.ToString("dd/MM/yyyy");
            }
            else if (parameter.ToString().Contains("{Time}"))
            {

                DateTime dt;
                DateTime.TryParse(value.ToString(), out dt);

                return dt.ToString("hh:mm tt");
            }
            else if (parameter.ToString().Contains("{Month}"))
            {
                DateTime dt;
                DateTime.TryParse(value.ToString(), out dt);

                return dt.ToString("MMMM").ToUpperFirstLetter();
            }
            else if (parameter.ToString().Contains("{Day}"))
            {
                DateTime dt;
                DateTime.TryParse(value.ToString(), out dt);

                return dt.ToString("dd");
            }
            else if (parameter.ToString().Contains("{Upper}"))
            {
                return value.ToString().ToUpper();
            }
            else
                return String.Format((string)parameter, value);
        }

        /// <summary>
        /// Convert back.
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
