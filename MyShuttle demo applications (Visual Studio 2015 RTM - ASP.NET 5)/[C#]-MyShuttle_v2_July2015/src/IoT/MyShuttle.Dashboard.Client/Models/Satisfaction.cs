namespace MyShuttle.Dashboard.Client.Models
{
    public class Satisfaction
    {
        public double[] Ratings { get; set; }
        public double SatisfactionPercent { get; set; }
        public double Positives { get; set; }
        public int Accepted { get; set; }
        public int Total { get; set; }
        public double AcceptedPercent { get; set; }
    }
}
