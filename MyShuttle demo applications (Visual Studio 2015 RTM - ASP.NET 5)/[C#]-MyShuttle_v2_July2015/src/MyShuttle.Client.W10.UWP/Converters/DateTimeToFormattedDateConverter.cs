namespace MyShuttle.Client.W10.UniversalApp.Converters
{
    using Core.Providers;
    using System;
    using Windows.UI.Xaml.Data;

    public class DateTimeToFormattedDateConverter : IValueConverter
    {
        public bool IsUpperCase { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var dateTimeToParse = (DateTime)value;

            var date = string.Format(new HumanizedDateProvider(), "{0}", dateTimeToParse);

            return IsUpperCase ? date.ToUpper() : date;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
