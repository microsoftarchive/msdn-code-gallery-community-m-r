using Cirrious.CrossCore.Converters;
using System;

namespace MyShuttle.Client.Core.Converters
{
    public class RatingToBoolConverter : MvxValueConverter<double, bool>
    {
        protected override bool Convert(double value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter == null)
            {
                // Hide stars by default
                return true;
            }

            double rateToCompareWith;

            try
            {
                rateToCompareWith = (long)parameter;
            }
            catch (InvalidCastException)
            {
                rateToCompareWith = (int)parameter;
            }

            var hideCurrentRate = value < rateToCompareWith;

            return hideCurrentRate;
        }
    }
}
