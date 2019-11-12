using Cirrious.CrossCore.Converters;
using System;

namespace MyShuttle.Client.Core.Converters
{
    public class NullToBoolWithOptionalInverseConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var returnValue = value == null;

            if (parameter is bool)
            {
                var inverse = (bool)parameter;

                if (inverse)
                {
                    returnValue = !returnValue;
                }
            }

            return returnValue;
        }
    }
}
