using System.Collections.Generic;
using System.Net;

namespace BrokerWinForm
{
    public class Publisher
    {
        
        private IPEndPoint _ipEndPoint = null;
        public IPEndPoint IpEndPoint
        {
            get { return _ipEndPoint; }
            set { _ipEndPoint = value; }
        }

        private List<EventData> _events = new List<EventData>();
        public List<EventData> Events
        {
            get { return _events; }
            set { _events = value; }
        }

        public Publisher(IPEndPoint ipEndPoint, EventData evnt)
        {
            IpEndPoint = ipEndPoint;
            Events.Add(evnt);
        }
    }
}
