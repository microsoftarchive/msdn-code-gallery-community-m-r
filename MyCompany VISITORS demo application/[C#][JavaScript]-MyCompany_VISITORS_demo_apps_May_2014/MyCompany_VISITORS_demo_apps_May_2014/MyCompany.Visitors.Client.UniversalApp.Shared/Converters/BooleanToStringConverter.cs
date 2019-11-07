namespace MyCompany.Visitors.Client.UniversalApp.Converters
{
    using System;
    using Windows.ApplicationModel.Resources;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Converter from Boolean to String.
    /// </summary>
    public class BooleanToStringConverter : IValueConverter
    {
        /// <summary>
        /// Converts a Boolean value to a String value.
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ResourceLoader loader = new ResourceLoader();
            bool val = System.Convert.ToBoolean(value);
            if (val)
                return loader.GetString("Yes");

            return loader.GetString("No");
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
