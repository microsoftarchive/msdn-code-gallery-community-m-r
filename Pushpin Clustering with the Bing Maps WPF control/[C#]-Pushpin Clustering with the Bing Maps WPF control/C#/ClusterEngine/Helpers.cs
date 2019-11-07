using Microsoft.Maps.MapControl.WPF;
using System;
using System.Windows;

namespace ClusterEngine
{
    internal static class Helpers
    {
        /* Constants used to speed up calculations*/
        private static double PiBy180 = (Math.PI / 180),
            OneBy4PI = 1 / (4 * Math.PI);

        public static Point CalculateGlobalPixel(Location location, double tileZoomRatio)
        {
            //Formulas based on following article:
            //http://msdn.microsoft.com/en-us/library/bb259689.aspx
            var sinLatitude = Math.Sin(location.Latitude * PiBy180);

            //If Latitude == 90 or -90 then Y will become infinity when Math.log is calculated
            if (sinLatitude == 1 || sinLatitude == -1)
            {
                sinLatitude += 0.0000000001;
            }

            var x = ((location.Longitude + 180) / 360) * tileZoomRatio;
            var y = (0.5 - Math.Log((1 + sinLatitude) / (1 - sinLatitude)) * OneBy4PI) * tileZoomRatio;

            return new Point(Math.Round(x), Math.Round(y));
        }
    }
}
