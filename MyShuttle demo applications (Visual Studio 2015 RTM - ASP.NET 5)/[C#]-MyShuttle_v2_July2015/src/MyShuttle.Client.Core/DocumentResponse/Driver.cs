
namespace MyShuttle.Client.Core.DocumentResponse
{
    using System.Collections.Generic;


    public class Driver
    {
        public int DriverId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public byte[] Picture { get; set; }

        public int CarrierId { get; set; }

        public Carrier Carrier { get; set; }

        public int? VehicleId { get; set; }

        public double RatingAvg { get; set; }

        public int TotalRides { get; set; }

        public Vehicle Vehicle { get; set; }

        public ICollection<Ride> Rides { get; set; }
    }
}