using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Model.Dtos
{
    public class VehicleDetailsSummaryDto
    {
        public double AvgSpeed { get; set; }
        public int Breakdowns { get; set; }
        public double TotalMiles { get; set; }
        public int TotalRides { get; set; }

        public double AvgConsumption { get; set; }
    }
}
