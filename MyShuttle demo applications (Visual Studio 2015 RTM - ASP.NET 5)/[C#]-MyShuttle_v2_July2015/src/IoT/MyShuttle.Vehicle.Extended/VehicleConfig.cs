
namespace MyShuttle.Vehicle
{
    public static class VehicleConfig
    {
        public static readonly string DeviceId = "FGH-9876";
        public static readonly int DriverId = 1;
        public static readonly string OBDAccidentCode = "OBD_SB001";

        // Namespace info.
        public static readonly string ServiceNamespace = "YOUR_SERVICEBUS";
        public static readonly string HubName = "EVENT_HUBNAME";
        public static readonly string KeyName = "Send";
        public static readonly string Key = "EVENTHUB_KEY";
    }
}
