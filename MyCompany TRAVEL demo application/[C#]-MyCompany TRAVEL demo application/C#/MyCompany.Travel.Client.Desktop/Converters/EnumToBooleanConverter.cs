namespace MyCompany.Travel.Client.Desktop.Converters
{
    using System;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// enum to bool converter
    /// </summary>
    public class EnumToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Convert a travel type to bool.
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
                string parameterString = parameter as string;
                if (parameterString == null)
                    return DependencyProperty.UnsetValue;

                if (Enum.IsDefined(value.GetType(), value) == false)
                    return DependencyProperty.UnsetValue;

                object parameterValue = Enum.Parse(value.GetType(), parameterString);

                return parameterValue.Equals(value);
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
            if (value != null && (bool)value)
            {
                string parameterString = parameter as string;
                if (!string.IsNullOrEmpty(parameterString))
                    return Enum.Parse(targetType, parameterString);
            }

            return DependencyProperty.UnsetValue;
        }
    }
}
