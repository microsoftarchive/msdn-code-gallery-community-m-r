
namespace MyShuttle.Model
{
    using Newtonsoft.Json;
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class MetricEvent
    {
        public MetricEvent()
        {
            Type = this.GetType().Name;
            EventDateTime = System.DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        }

        [DataMember]
        public string DeviceId { get; set; }

        [DataMember]
        public int DriverId { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string EventDateTime { get; set; }

        public bool CanHandle(string type)
        {
            return GetType().Name == type;
        }

        public virtual EventMessage GetMessage()
        {
            return new EventMessage()
            {
                DeviceId = DeviceId,
                Message = string.Format("Event received")
            };
        }

        public virtual EventMessage GetMessage(string data)
        {
            return JsonConvert.DeserializeObject<MetricEvent>(data).GetMessage();
        }

        
    }
}
