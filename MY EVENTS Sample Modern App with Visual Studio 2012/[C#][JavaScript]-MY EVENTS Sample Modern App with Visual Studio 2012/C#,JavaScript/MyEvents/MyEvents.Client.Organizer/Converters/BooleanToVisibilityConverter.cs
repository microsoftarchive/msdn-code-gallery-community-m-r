using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MyEvents.Client.Organizer.Converters
{
    /// <summary>
    /// Class that holds the transformation between a boolean and the Visibility
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {

        /// <summary>
        /// Converts from boolean to Visibility
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value is bool)
            {
                return (bool)value == true ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Converts back from Visibility to boolean
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
