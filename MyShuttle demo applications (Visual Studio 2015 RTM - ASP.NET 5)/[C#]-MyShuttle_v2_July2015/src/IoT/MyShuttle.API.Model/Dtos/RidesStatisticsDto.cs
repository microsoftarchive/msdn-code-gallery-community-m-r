using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Model.Dtos
{
    public class RidesStatisticsDto
    {
        public double AvgSpeed { get; set; }
        public double Breakdowns { get; set; }
        public int Items { get; set; }
        public double Miles { get; set; }
    }
}
