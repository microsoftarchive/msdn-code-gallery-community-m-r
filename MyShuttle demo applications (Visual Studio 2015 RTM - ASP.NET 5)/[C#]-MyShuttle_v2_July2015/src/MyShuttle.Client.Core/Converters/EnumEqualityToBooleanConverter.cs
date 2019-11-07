using Cirrious.CrossCore.Converters;
using System;

namespace MyShuttle.Client.Core.Converters
{
    public class EnumEqualityToBooleanConverter : MvxValueConverter<Enum, bool>
    {
        protected override bool Convert(Enum value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var valueIsNotEqualToParameter = value.ToString() != (parameter as Enum).ToString();

            return valueIsNotEqualToParameter;
        }
    }
}