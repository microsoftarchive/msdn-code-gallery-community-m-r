using Cirrious.MvvmCross.ViewModels;

namespace MyShuttle.Client.Core.DocumentResponse
{
    using System.Collections.Generic;

    public class Vehicle : MvxNotifyPropertyChanged
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

        public double DistanceFromGivenPosition { get; set; }

        public VehicleStatus VehicleStatus { get; set; }

        public bool IsSelected { get; set; }

        public int CarrierId { get; set; }

        public Carrier Carrier { get; set; }

        public int DriverId { get; set; }

        public double Rate { get; set; }

        public double RatingAvg { get; set; }

        public int TotalRides { get; set; }

        public Driver Driver { get; set; }

        public ICollection<Ride> Rides { get; set; }
        public string MakeAndModel
        {
            get
            {
                return string.Format("{0} {1}", Make, Model);
            }
        }
    }
}