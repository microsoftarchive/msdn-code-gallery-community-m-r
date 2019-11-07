namespace MyShuttle.Dashboard.Client.Models
{
    using System;

    public class VehicleAlarmResult
    {
        public DateTime Date { get; set; }
        public string Code { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
    }
}
