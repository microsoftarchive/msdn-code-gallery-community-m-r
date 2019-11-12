using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.Plugins.Color;
using MyShuttle.Client.Core.DocumentResponse;
using System;

namespace MyShuttle.Client.iOS.Converters
{
    public class VehicleStatusToColorConverter : MvxColorValueConverter<VehicleStatus>
    {
        protected override Cirrious.CrossCore.UI.MvxColor Convert(VehicleStatus value, object parameter, System.Globalization.CultureInfo culture)
        {
            var green = new MvxColor(0x43, 0xa7, 0x2e);
            var red = new MvxColor(0xec, 0x67, 0x4d);

            var returnColor = value == VehicleStatus.Free ?
                green :
                red;

            return returnColor;
        }
    }
}