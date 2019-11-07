using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.CrossCore.Converters;
using MyShuttle.Client.Core.DocumentResponse;

namespace MyShuttle.Client.iOS.Converters
{
    public class VehicleStatusToUIColorConverter : MvxValueConverter<VehicleStatus, UIColor>
    {
        protected override UIColor Convert(VehicleStatus value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var green = UIColor.FromRGB(0x43, 0xa7, 0x2e);
            var red = UIColor.FromRGB(0xec, 0x67, 0x4d);

            var returnColor = value == VehicleStatus.Free ?
                green :
                red;

            return returnColor;
        }
    }
}