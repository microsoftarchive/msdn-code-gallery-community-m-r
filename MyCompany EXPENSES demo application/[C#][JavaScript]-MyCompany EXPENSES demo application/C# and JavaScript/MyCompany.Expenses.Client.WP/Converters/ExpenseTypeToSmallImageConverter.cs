namespace MyCompany.Expenses.Client.WP.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Expense Type To Icon Converter
    /// </summary>
    public class ExpenseTypeToSmallImageConverter : IValueConverter
    {
        /// <summary>
        /// Converts the enum ExpenseType to an image.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is ExpenseType))
            {
                return string.Empty;
            }

            ExpenseType type = (ExpenseType)value;
            switch (type)
            {
                case ExpenseType.Travel:
                    return "/Assets/TravelIconSmall.png";
                case ExpenseType.Food:
                    return "/Assets/FoodIconSmall.png";
                case ExpenseType.Accommodation:
                    return "/Assets/BedIconSmall.png";
                case ExpenseType.Other:
                    return "/Assets/OthersIconSmall.png";
                default:
                    return string.Empty;
            }
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
