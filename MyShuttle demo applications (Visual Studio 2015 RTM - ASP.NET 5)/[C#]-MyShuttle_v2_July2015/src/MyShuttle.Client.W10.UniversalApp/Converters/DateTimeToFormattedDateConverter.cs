namespace MyShuttle.Client.W10.UniversalApp.Converters
{
    using Core.Providers;
    using System;
    using Windows.UI.Xaml.Data;

    public class DateTimeToFormattedDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var dateTimeToParse = (DateTime)value;

            return string.Format(new HumanizedDateProvider(), "{0}", dateTimeToParse);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
