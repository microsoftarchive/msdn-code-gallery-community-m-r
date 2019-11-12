using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Model.Documents
{
    public class VehiclesSummaryDocument
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public int VehiclesCount { get; set; }
        public double TotalMiles { get; set; }
        public int Breakdowns { get; set; }
        public double AverageSpeed { get; set; }
        public int Accidents { get; set; }
        public double TotalSeconds { get; set; }

        public string[] Vehicles { get; set; }
    }
}
