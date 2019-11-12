using System;
using Windows.UI.Xaml.Data;

namespace MyEvents.Client.Organizer.Converters
{
    /// <summary>
    /// String converter to format dates based on conventions
    /// </summary>
    public class FormatStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value.ToString() == "0")
                return value.ToString();

            if (parameter.ToString().Contains("{Date}"))
            {
                DateTime dt;
                DateTime.TryParse(value.ToString(), out dt);

                return dt.Date.ToString("dd MMMM yyyy");
            }
            else if (parameter.ToString().Contains("{Time}"))
            {

                DateTime dt;
                DateTime.TryParse(value.ToString(), out dt);

                return dt.ToString("hh:mm tt");
            }
            else if (parameter.ToString().Contains("{TimeOffset}"))
            {
                int offset;
                int.TryParse(value.ToString(), out offset);

                if (offset > -1)
                    return string.Format("(GMT+{0})", offset);
                else
                    return string.Format("(GMT{0})", offset);
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
            else
                return String.Format((string)parameter, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
