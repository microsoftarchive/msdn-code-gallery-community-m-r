using Cirrious.CrossCore.Converters;
using MyShuttle.Client.Core.DocumentResponse;
using System;

namespace MyShuttle.Client.Core.Converters
{
    public class VehicleStatusToBoolConverter : MvxValueConverter<VehicleStatus, bool>
    {
        protected override bool Convert(VehicleStatus value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var vehicleNotFree = value != VehicleStatus.Free;

            var inverseValue = parameter as bool?;

            if (inverseValue != null && inverseValue.HasValue && inverseValue.Value)
            {
                vehicleNotFree = !vehicleNotFree;
            }

            return vehicleNotFree;
        }
    }
}
