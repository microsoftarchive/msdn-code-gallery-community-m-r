namespace MyShuttle.Model
{
    using System;

    public class Ride
    {
        public int RideId { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public double StartLatitude { get; set; }

        public double StartLongitude { get; set; }

        public double EndLatitude { get; set; }

        public double EndLongitude { get; set; }

        public string StartAddress { get; set; }

        public string EndAddress { get; set; }

        public double Distance { get; set; }

        public int Duration { get; set; }

        public double Cost { get; set; }

        public byte[] Signature { get; set; }

        public double Rating { get; set; }

        public string Comments { get; set; }

        public int VehicleId { get; set; }

        public int CarrierId { get; set; }

        public Carrier Carrier { get; set; }

        public Vehicle Vehicle { get; set; }

        public int DriverId { get; set; }

        public Driver Driver { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        // // This properties are needed to integrate the database with Mobile Services
        public DateTimeOffset? CreatedAt { get; set; }

        public bool Deleted { get; set; }

        public string Id { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public byte[] Version { get; set; }
    }
}