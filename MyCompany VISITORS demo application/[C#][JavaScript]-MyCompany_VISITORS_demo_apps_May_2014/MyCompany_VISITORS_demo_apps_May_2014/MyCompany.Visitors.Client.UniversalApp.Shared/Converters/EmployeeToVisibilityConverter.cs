namespace MyCompany.Visitors.Client.UniversalApp.Converters
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Converter from ICollection of EmployeePicture to image.
    /// </summary>
    public class EmployeeToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Convert ICollection of EmployeePicture to image.
        /// </summary>
        /// <param name="value">ICollection of VisitorPicture</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = (Employee)value;

            //If the Employee is empty
            if(val.EmployeeId == 0)
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
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
