
namespace MyShuttle.Model
{
    using Newtonsoft.Json;
    using System.Runtime.Serialization;

    [DataContract]
    public class OBDEvent : MetricEvent
    {
        [DataMember]
        public string Code { get; set; }

        public override EventMessage GetMessage(string data)
        {
            return JsonConvert.DeserializeObject<OBDEvent>(data).GetMessage();
        }

        public override EventMessage GetMessage()
        {
            return new EventMessage()
            {
                DeviceId = DeviceId,
                Message = string.Format("OBD event received. Code = {0}", Code)
            };
        }

    }
}
