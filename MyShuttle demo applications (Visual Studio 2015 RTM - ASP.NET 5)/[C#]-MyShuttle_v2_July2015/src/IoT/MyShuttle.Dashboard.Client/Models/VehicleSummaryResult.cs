namespace MyShuttle.Dashboard.Client.Models
{
    public class VehicleSummaryResult
    {
        public int VehiclesCount { get; set; }
        public double TotalMiles { get; set; }
        public string Breakdowns { get; set; }
        public string Accidents { get; set; }
        public double TotalSeconds { get; set; }
        public double AverageSpeed { get; set; }
    }
}
