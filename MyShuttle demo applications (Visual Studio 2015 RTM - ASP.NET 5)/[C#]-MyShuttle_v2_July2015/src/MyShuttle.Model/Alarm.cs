namespace MyShuttle.Model
{
    public class Alarm
    {
        public string DeviceId { get; set; }

        public string Severity { get; set; }

        public string Time { get; set; }

        public string DeviceTime { get; set; }

        public string ODBCode { get; set; }

        public string Message { get; set; }

        public int VehicleId { get; set; }
    }
}