using System;
using System.Windows.Data;

namespace MyEvents.Client.Organizer.Desktop.Converters
{
    /// <summary>
    /// Class to validate multiple HasErrors properties.
    /// </summary>
    class BooleanValidationMultiConverter : IMultiValueConverter
    {
        /// <summary>
        /// Convert multiple boolean values to one.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values != null)
            {
                foreach (object value in values)
                {
                    if ((Boolean)value == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        
        /// <summary>
        /// not implemented.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
