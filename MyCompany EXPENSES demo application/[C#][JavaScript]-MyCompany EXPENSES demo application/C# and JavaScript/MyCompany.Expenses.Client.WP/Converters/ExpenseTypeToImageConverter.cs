namespace MyCompany.Expenses.Client.WP.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;

    /// <summary>
    /// Expense type enum to image representation converter.
    /// </summary>
    public class ExpenseTypeToImageConverter : IValueConverter
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
                    return "/Assets/TravelIcon.png";
                case ExpenseType.Food:
                    return "/Assets/FoodIcon.png";
                case ExpenseType.Accommodation:
                    return "/Assets/BedIcon.png";
                case ExpenseType.Other:
                    return "/Assets/OthersIcon.png";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Convert an image to Expense type. Not implemented.
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
