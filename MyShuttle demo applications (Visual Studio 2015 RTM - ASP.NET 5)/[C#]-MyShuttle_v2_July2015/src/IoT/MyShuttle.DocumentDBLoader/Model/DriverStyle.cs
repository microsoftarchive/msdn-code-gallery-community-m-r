using System;
using System.Dynamic;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace MyShuttle.DocumentDBLoader.Model
{
    public class DriverStyle
    {
        [JsonProperty(PropertyName = "id")]
        public string DocumentId { get; set; }
        public int DriverId { get; set; }
        public int ClassificationAvg { get; set; }
        public Classification[] Classifications { get; set; }

        public static DriverStyle CreateFromDriverClassification(DriverClassification driverClassification)
        {
            return new DriverStyle
            {
                DriverId = driverClassification.DriverId,
                ClassificationAvg = driverClassification.Classification,
                Classifications = new[]
                    {
                        new Classification
                        {
                            Timestamp = driverClassification.Timestamp,
                            Value = driverClassification.Classification
                        }
                    }
            };
        }
    }
}
