using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.DocumentDb.ConsoleApp.HDinsight
{
    class TripdataRow
    {
        public int License { get; set; }
        public int HackLicense { get; set; }
        public string VendorId { get; set; }
        public int RateCode { get; set; }
        public string StoreAndFwdFlag { get; set; }
        public string PickupDate { get; set; }
        public string DropOffDate { get; set; }
        public int PassengerCount { get; set; }
        public double TripTimeSeconds { get; set; }
        public double TripDistance { get; set; }
        public double PickupLon { get; set; }
        public double PickupLat { get; set; }
        public double DropoffLon { get; set; }
        public double DropoffLat { get; set; }

    }
}
