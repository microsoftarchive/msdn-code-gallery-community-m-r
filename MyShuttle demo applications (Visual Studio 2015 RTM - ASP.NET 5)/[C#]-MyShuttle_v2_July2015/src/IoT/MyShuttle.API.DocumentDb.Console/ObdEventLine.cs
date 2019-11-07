using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.DocumentDb.ConsoleApp
{
    class ObdEventLine
    {
        internal string DeviceId;

        public string Code { get; internal set; }
        public DateTime Date { get; internal set; }
        public int DriverId { get; internal set; }
    }
}
