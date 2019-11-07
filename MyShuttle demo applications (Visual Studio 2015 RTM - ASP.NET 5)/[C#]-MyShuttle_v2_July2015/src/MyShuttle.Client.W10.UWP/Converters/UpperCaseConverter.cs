namespace MyShuttle.Client.W10.UniversalApp.Converters
{
    using System;
    using Windows.UI.Xaml.Data;

    public class UpperCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            var str = value as string;

            if (value != null)
            {
                return str.ToUpper();
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
