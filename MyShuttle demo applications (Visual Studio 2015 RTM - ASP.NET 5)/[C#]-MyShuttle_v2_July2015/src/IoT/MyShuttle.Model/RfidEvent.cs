
namespace MyShuttle.Model
{
    using Newtonsoft.Json;
    using System.Runtime.Serialization;

    [DataContract]
    public class RfidEvent : MetricEvent
    {
        public override EventMessage GetMessage(string data)
        {
            return JsonConvert.DeserializeObject<RfidEvent>(data).GetMessage();

        }

        public override EventMessage GetMessage()
        {
            return new EventMessage()
            {
                DeviceId = DeviceId,
                Message = string.Format("Rfid event received. DriverId = {0}", DriverId)
            };
        }
    }
}
