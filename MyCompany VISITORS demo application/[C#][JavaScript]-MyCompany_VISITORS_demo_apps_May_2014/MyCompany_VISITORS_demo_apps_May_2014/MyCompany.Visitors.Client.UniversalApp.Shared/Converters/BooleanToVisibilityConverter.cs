namespace MyCompany.Visitors.Client.UniversalApp.Converters
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Converter from Boolean to Visibility.
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts a Boolean value to a Visibility value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool val = System.Convert.ToBoolean(value);
            if(val)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
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
            Visibility val = (Visibility)value;
            if (val == Visibility.Visible)
            {
                return true; ;
            }
            return false;
        }
    }
}
