
namespace MyShuttle.Model
{
    using Newtonsoft.Json;
    using System.Runtime.Serialization;

    [DataContract]
    public class AccelerometerEvent : MetricEvent
    {
        [DataMember]
        public double X { get; set; }

        [DataMember]
        public double Y { get; set; }

        [DataMember]
        public double Z { get; set; }


        public override EventMessage GetMessage(string data)
        {
            return JsonConvert.DeserializeObject<AccelerometerEvent>(data).GetMessage();
        }

        public override EventMessage GetMessage()
        {
            return new EventMessage()
            {
                DeviceId = DeviceId,
                Message = string.Format("Accelerometer event received. X = {0}, Y = {1}, Z = {2}", X, Y, Z)
            };
        }
    }
}
