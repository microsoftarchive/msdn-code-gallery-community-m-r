using System;

namespace MyShuttle.DocumentDBLoader.Model
{
    public class Classification
    {
        public DateTime Timestamp { get; set; }
        public int Value { get; set; }

        public static Classification CreateFromDriverClassification(DriverClassification driverClassification)
        {
            return new Classification
            {
                Timestamp = driverClassification.Timestamp,
                Value = driverClassification.Classification
            };
        }
    }
}