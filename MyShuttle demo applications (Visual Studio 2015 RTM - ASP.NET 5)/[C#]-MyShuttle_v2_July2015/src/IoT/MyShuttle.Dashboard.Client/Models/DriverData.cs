namespace MyShuttle.Dashboard.Client.Models
{
    public class DriverData
    {
        public int DriverId { get; set; }
        public string Name { get; set; }
        public int TotalRides { get; set; }
        public double RatingAvg { get; set; }
        public byte[] Picture { get; set; }
    }
}