using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Model
{
    public class Vehicle
    {
        public int VehicleId { get; set; }

        public string LicensePlate { get; set; }

        public string Model { get; set; }

        public string Make { get; set; }

        public byte[] Picture { get; set; }

        public VehicleType Type { get; set; }

        public int Seats { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        /// <summary>
        /// ToDO: This property must be [NotMapped] property.
        /// </summary>
        public double DistanceFromGivenPosition { get; set; }

        public VehicleStatus VehicleStatus { get; set; }

        public int CarrierId { get; set; }

        public virtual Carrier Carrier { get; set; }

        public double Rate { get; set; }

        public double RatingAvg { get; set; }

        public int TotalRides { get; set; }

        public string DeviceId { get; set; }

        public int DriverId { get; set; }
        public virtual Driver Driver { get; set; }

        public ICollection<Ride> Rides { get; set; }


    }
}
