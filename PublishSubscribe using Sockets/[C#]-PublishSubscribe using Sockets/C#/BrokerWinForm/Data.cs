using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;

namespace BrokerWinForm
{
    public class Data
    {

        private List<Publisher> publishers = new List<Publisher>
            { 
                //error here!!
                new Publisher(
                    new System.Net.IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000), 
                    new EventData("first", "details about event first")
                    )
            };

        public List<Publisher> Publishers
        {

            get { return publishers; }
            set { publishers = value; }
        }

        public static List<Subscriber> Subscribers = new List<Subscriber>();
    }
}