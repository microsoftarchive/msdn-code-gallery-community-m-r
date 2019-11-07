using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.DocumentDb.ConsoleApp
{
    class CommaSeparatedFileParser
    {
        private readonly string _fname;
        public CommaSeparatedFileParser(string file)
        {
            _fname = file;
        }

        public IEnumerable<string[]> Read()
        {
            var lines = File.ReadAllLines(_fname).Select(l=>l.Trim()).
                Where(l =>!string.IsNullOrEmpty(l) && !l.StartsWith("--"));

            return lines.Select(l => l.Split(',').Select(t => t.Trim()).ToArray());
        }
    }
}
