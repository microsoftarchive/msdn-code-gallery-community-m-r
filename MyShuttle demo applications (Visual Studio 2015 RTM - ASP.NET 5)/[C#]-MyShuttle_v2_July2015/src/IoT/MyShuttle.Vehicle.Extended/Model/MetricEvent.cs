

namespace MyShuttle.Vehicle.Model
{
    using System;

    public abstract class MetricEvent
    {
        public MetricEvent()
        {
            Type = this.GetType().Name;
            DriverId = VehicleConfig.DriverId;
            EventDateTime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public string DeviceId { get; set; }

        public int DriverId { get; set; }

        public string Type { get; set; }

        public string EventDateTime { get; set; }
    }
}
