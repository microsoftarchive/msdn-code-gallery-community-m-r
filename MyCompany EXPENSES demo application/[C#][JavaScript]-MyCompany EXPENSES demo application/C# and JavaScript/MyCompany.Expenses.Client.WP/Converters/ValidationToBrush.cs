namespace MyCompany.Expenses.Client.WP.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;
    using System.Windows.Media;

    /// <summary>
    /// COnverts the result of the validation to a brush.
    /// </summary>
    public class ValidationToBorderBrushConverter : IValueConverter
    {
        /// <summary>
        /// Converts bool to border brush
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool validationFailed;
            bool.TryParse(value.ToString(), out validationFailed);

            if (validationFailed)
                return new SolidColorBrush(Color.FromArgb(255, 200,100,100));

            return (App.Current.Resources["PhoneTextBoxBrush"] as SolidColorBrush);
        }

        /// <summary>
        /// Converts border brush to validation (not implemented)
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
