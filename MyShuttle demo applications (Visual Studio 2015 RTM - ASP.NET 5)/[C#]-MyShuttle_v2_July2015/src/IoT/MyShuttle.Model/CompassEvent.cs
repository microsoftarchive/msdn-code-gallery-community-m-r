

namespace MyShuttle.Model
{
    using Newtonsoft.Json;
    using System.Runtime.Serialization;

    [DataContract]
    public class CompassEvent : MetricEvent
    {
        [DataMember]
        public double HeadingDegrees { get; set; }

        public override EventMessage GetMessage(string data)
        {
            return JsonConvert.DeserializeObject<CompassEvent>(data).GetMessage();
        }

        public override EventMessage GetMessage()
        {
            return new EventMessage()
            {
                DeviceId = DeviceId,
                Message = string.Format("Compass event received. HeadingDegrees = {0}", HeadingDegrees)
            };
        }
    }
}
