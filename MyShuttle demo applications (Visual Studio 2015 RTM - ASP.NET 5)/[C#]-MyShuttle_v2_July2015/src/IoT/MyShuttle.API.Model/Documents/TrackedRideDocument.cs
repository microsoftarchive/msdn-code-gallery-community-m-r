using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Model.Documents
{
    public class TrackedRideDocument
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int DriverId { get; set; }

        public int[] RidesId { get; set; }


        public string Device { get; set; }

        public double Miles { get; set; }

        public int Breakdowns { get; set; }
        public double GpsAverageSpeed { get; set; }

        public GpsPoint[] Gps { get; set; }

        public ObdData[] Obds { get; set; }

    }

    public class ObdData
    {
        public string Code { get; set; }
        public DateTime Date { get; set; }
    }

    public class GpsPoint
    {
        public double Lat { get; set; }
        public double Lon { get; set; }

        public double DistanceFrom(GpsPoint other)
        {
            const int radius = 3956;                // Miles
            var lat1 = Lat * Math.PI / 180.0;
            var lat2 = other.Lat * Math.PI / 180.0;
            var diffLat = (other.Lat - Lat) * Math.PI / 180.0;
            var diffLon = (other.Lon - Lon) * Math.PI / 180.0;
            var a = Math.Pow(Math.Sin(diffLat / 2.0), 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(diffLon / 2.0), 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = radius * c;
            return d;
        }
    }

}
