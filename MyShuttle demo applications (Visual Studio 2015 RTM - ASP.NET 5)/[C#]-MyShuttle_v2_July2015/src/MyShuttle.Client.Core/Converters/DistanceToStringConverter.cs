using Cirrious.CrossCore.Converters;
using System;

namespace MyShuttle.Client.Core.Converters
{
    public class DistanceToStringConverter : MvxValueConverter<double, string>
    {
        protected override string Convert(double value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.Format("{0:0.00}", value);
        }
    }
}
