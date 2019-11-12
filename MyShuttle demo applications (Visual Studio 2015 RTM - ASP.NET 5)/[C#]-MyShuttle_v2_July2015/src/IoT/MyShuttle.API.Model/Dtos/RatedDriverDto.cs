using System;
using System.Collections.Generic;

namespace MyShuttle.API.Model.Dtos
{
    public class RatedDriverDto
    {
        public int DriverId { get; set; }
        public DataClassification[] Classifications { get; set; }
        public int ClassificationAvg { get; set; }
    }

    public class DataClassification
    {
        public DateTime Timestamp { get; set; }
        public int Value { get; set; }
    }
}
