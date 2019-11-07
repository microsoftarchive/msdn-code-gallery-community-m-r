using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;
using MyShuttle.Client.Core.Providers;

namespace MyShuttle.Client.UniversalApp.Converters
{
    public class DateTimeToFormattedDateConverter : IValueConverter
    {
        public  object Convert(object value, Type targetType, object parameter, string language)
        {
            var dateTimeToParse = (DateTime)value;

            return string.Format(new HumanizedDateProvider(), "{0}", dateTimeToParse);
        }

        public  object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
