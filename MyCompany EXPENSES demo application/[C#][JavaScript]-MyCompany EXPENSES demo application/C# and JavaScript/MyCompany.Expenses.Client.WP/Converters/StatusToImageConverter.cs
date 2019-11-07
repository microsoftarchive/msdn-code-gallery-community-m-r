namespace MyCompany.Expenses.Client.WP.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;

    /// <summary>
    /// Status to image converter
    /// </summary>
    public class StatusToImageConverter : IValueConverter
    {
        /// <summary>
        /// Converts the enum Status to an image.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is ExpenseStatus))
            {
                return string.Empty;
            }

            ExpenseStatus type = (ExpenseStatus)value;
            switch (type)
            {
                case ExpenseStatus.Pending:
                    return "/Assets/StatusPending.png";
                case ExpenseStatus.Approved:
                    return "/Assets/StatusApproved.png";
                case ExpenseStatus.Denied:
                    return "/Assets/StatusDenied.png";
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
