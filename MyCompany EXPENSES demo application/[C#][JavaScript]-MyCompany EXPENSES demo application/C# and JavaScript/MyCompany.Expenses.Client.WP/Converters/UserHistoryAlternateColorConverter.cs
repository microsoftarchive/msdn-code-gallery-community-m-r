namespace MyCompany.Expenses.Client.WP.Converters
{
    using MyCompany.Expenses.Client.WP.ViewModel.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;
    using System.Windows.Media;

    /// <summary>
    /// alternate color converter
    /// </summary>
    public class UserHistoryAlternateColorConverter : IValueConverter
    {
        SolidColorBrush even = App.Current.Resources["ModuleColor"] as SolidColorBrush;
        SolidColorBrush odd = App.Current.Resources["ModuleColorLighten400"] as SolidColorBrush;

        /// <summary>
        /// Return alternate color
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Expense expense = value as Expense;

            if (expense != null)
            {
                var locator = App.Current.Resources["Locator"] as ViewModelLocator;
                int index = locator.UserHistory.Expenses.IndexOf(expense);
                if (index % 2 == 0)
                {
                    return even;
                }
                else
                {
                    return odd;
                }
            }

            return even;
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
