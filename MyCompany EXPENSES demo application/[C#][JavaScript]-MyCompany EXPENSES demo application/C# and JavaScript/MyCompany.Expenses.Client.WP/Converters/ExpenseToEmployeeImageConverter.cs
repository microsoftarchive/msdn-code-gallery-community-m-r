namespace MyCompany.Expenses.Client.WP.Converters
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Windows.Storage.Streams;

    /// <summary>
    /// Expense to employye image converter
    /// </summary>
    public class ExpenseToEmployeeImageConverter : IValueConverter
    {
        private const string defaultImage = "/Assets/no-photo.png";
        /// <summary>
        /// Gets the employee image from expense.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is Expense))
            {
                return defaultImage;
            }

            Expense expense = (Expense)value;

            if (expense.Employee.EmployeePictures != null && expense.Employee.EmployeePictures.Any())
            {
                using (Stream stream = new MemoryStream(expense.Employee.EmployeePictures.First().Content))
                {
                    var image = new BitmapImage();
                    image.SetSource(stream);
                    return image;
                }
            }

            return defaultImage;
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
