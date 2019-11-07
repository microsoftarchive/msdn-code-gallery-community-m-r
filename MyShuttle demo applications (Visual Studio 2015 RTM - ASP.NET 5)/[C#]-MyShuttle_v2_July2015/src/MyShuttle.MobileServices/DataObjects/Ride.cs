namespace MyShuttle.MobileServices.DataObjects
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Ride : CustomDataEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
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

        public int DriverId { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}