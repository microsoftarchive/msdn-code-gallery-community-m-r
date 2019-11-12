using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyShuttle.API.DocumentDb.ConsoleApp
{
    class GpsRouteFileParser
    {
        private readonly string _fname;
        public GpsRouteFileParser(string file)
        {
            _fname = file;
        }

        public IEnumerable<Tuple<double, double>> Read()
        {
            var xdoc = XDocument.Load(_fname);
            return xdoc.Root.Elements("trkpt").Select(e => new Tuple<double, double>(
                item1: double.Parse(e.Attribute("lat").Value),
                item2: double.Parse(e.Attribute("lon").Value)));
        }
    }
}
