using Cirrious.CrossCore.Converters;
using System;

namespace MyShuttle.Client.Core.Converters
{
    public class DateTimeToTimeConverter : MvxValueConverter<DateTime, string>
    {
        protected override string Convert(DateTime value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString("t").ToLower();
        }
        
    }
}
