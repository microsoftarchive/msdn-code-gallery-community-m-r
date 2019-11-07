namespace MyShuttle.DocumentDBLoader.Model
{
    using System;

    public class DriverClassification
    {
        public int DriverId { get; set; }
        public DateTime Timestamp { get; set; }
        public int Classification { get; set; }
    }
}
