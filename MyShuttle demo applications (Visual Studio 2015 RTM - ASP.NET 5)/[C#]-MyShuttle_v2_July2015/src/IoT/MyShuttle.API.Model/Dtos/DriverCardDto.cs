using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Model.Dtos
{
    public class DriverCardDto
    {
        public int DriverId { get; set; }
        public byte[] DriverPhoto { get; set; }
        public int DriverTotalRides { get; set; }
        public string DriverName { get; set; }

        public object[] AllVehicles { get; set; }
        public byte[] MostUsedVehiclePhoto { get; private set; }
        public string MostUsedVehicleMake { get; private set; }
        public string MostUsedVehicleModel { get; private set; }
        public int MostUsedVehicleId { get; private set; }
        public string MostUsedVehicleDevice { get; private set; }

        public DriverCardDto(Driver driver)
        {
            DriverId = driver.DriverId;
            DriverPhoto = driver.Picture;
            DriverTotalRides = driver.TotalRides;
            DriverName = driver.Name;

            if (driver.Rides != null && driver.Rides.Any())
            {
                var ridesByVehicle = driver.Rides.GroupBy(r => r.VehicleId).
                    Select(g => new { VehicleId = g.Key, Vehicle = g.First().Vehicle, RidesCount = g.Count() }).
                    OrderByDescending(g => g.RidesCount);
                AllVehicles = ridesByVehicle.Select(g => new { Id = g.VehicleId, RidesCount = g.RidesCount }).ToArray();
                var mostUsedVehicle = ridesByVehicle.First().Vehicle;
                MostUsedVehiclePhoto = mostUsedVehicle.Picture;
                MostUsedVehicleMake = mostUsedVehicle.Make;
                MostUsedVehicleModel = mostUsedVehicle.Model;
                MostUsedVehicleId = mostUsedVehicle.VehicleId;
                MostUsedVehicleDevice = mostUsedVehicle.DeviceId;
            }

           
        }
    }
}
