using Cirrious.CrossCore.Converters;
using System;
using System.Globalization;

namespace MyShuttle.Client.Core.Converters
{
    public class PriceToStringConverter : MvxValueConverter<double, string>
    {
        protected override string Convert(double value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var returnValue = string.Format("{0}", value.ToString(CultureInfo.InvariantCulture));

            if (parameter != null && (parameter is bool))
            {
                var withoutDollarSign = (bool)parameter;
                    
                if (!withoutDollarSign)
                {
                    returnValue = "$" + returnValue;
                }
            }

            return returnValue;
        }
    }
}
