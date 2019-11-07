namespace MyShuttle.Dashboard.Client.Models
{
    public class Rate
    {
        public double AvgRate { get; set; }
        public int NumRates { get; set; }

        public bool AvgRateIsLow { get { return AvgRate < 3; } }
    }
}