namespace MyShuttle.Dashboard.Client.Models
{
    public class VehicleData
    {
        public int VehicleId { get; set; }
        public int Rides { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Device { get; set; }
        public byte[] Picture { get; set; }


    }
}