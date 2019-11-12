using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Model.Dtos
{
    public class DrivingStyleDto
    {
        public int Aggressiveness { get; set; }
        public int Breakdowns { get; set; }
        public int Profiability { get; set; }
        public int Consumption { get; set; }
        public int Brakes { get; set; }
        public int Speed { get; set; }

        public double AvgStyle { get; set; }

        public DrivingStyleDto()
        {
            Aggressiveness = Breakdowns = Profiability = Consumption = Brakes = Speed = -1;
            AvgStyle = -1.0;
        }

        public static int SpeedFromAvgSpeed(double avgSpeed)
        {
            var speed = (int)((avgSpeed / 120.0) * 10.0);
            return speed <= 10 ? speed : 10;
        }
    }
}
