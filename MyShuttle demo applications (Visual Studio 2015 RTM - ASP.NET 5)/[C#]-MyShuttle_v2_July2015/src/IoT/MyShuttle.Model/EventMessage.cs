using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.Model
{
    public class EventMessage
    {
        public EventMessage()
        {
            Time = DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss");
        }

        public string Time { get; set; }

        public string DeviceId { get; set; }

        public string Message { get; set; }
    }
}
