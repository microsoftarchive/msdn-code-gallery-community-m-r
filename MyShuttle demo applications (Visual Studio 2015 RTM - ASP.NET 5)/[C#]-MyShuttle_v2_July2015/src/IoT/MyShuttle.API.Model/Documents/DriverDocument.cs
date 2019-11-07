using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Model.Documents
{
    public class DriverDocument 
    {
        public int DriverId { get; set; }
        public string Timestamp { get; set; }
        public int Classification { get; set; }


        public DateTime GetTimestamp()
        {
            return Timestamp.FromDocumentDbDateTimeString();
        }
    }
}
