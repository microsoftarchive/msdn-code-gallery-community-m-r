using Cirrious.CrossCore.Converters;
using MyShuttle.Client.Core.Providers;
using System;

namespace MyShuttle.Client.Core.Converters
{
    public class DateTimeToFormattedDateConverter : MvxValueConverter<DateTime, string>
    {
        protected override string Convert(DateTime value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.Format(new HumanizedDateProvider(), "{0}", value);
        }
    }
}
