namespace MyShuttle.Dashboard.Client.Models
{
    public class TopDriversResult
    {
        public int RequestedItems { get; set; }
        public int ActualItems { get; set; }
        public int TotalItems { get; set; }
        public Driver[] Items { get; set; }
    }
}