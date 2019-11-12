namespace MyShuttle.Dashboard.Client.Models
{
    public class DriverResult
    {
        public int DriverId { get; set; }
        public byte[] DriverPhoto { get; set; }
        public int DriverTotalRides { get; set; }
        public string DriverName { get; set; }
        //public int AllVehicles { get; set; }
        public byte[] MostUsedVehiclePhoto { get; set; }
        public string MostUsedVehicleMake { get; set; }
        public string MostUsedVehicleModel { get; set; }
        public int MostUsedVehicleId { get; set; }
        public string MostUsedVehicleDevice { get; set; }

    }
}