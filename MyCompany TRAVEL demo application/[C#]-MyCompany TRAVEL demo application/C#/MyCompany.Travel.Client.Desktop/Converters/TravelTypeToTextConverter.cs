namespace MyCompany.Travel.Client.Desktop.Converters
{
    using MyCompany.Travel.Client.Desktop.Resources.Strings;
    using System;
    using System.IO;
    using System.Windows.Data;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// travel type to text converter
    /// </summary>
    public class TravelTypeToTextConverter : IValueConverter
    {
        /// <summary>
        /// Convert a travel type to text.
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
                TravelType travelType = (TravelType)value;
                switch (travelType)
                {
                    case TravelType.Unknown:
                        return StringProvider.GetString("TravelTypeUnkown");
                    case TravelType.OneWay:
                        return StringProvider.GetString("TravelTypeOneWay");
                    case TravelType.Roundtrip:
                        return StringProvider.GetString("TravelTypeRoundTrip");
                    default:
                        break;
                }
            }

            return null;
        }

        /// <summary>
        /// Convert back
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
