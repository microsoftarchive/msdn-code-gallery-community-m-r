namespace MyShuttle.Dashboard.Client.Models
{
    public class ServicesSatisfactionResult
    {
        public Satisfaction[] Satisfactions { get; set; }

        public double PositivesDifference { get; set; }

        public double AcceptedDifference { get; set; }
    }
}


