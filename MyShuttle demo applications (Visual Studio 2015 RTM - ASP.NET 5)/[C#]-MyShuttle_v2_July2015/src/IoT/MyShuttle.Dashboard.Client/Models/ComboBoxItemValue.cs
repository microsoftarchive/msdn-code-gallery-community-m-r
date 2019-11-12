using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.Dashboard.Client.Models
{
    public class ComboBoxItemValue
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            // Narrator support
            return Value;
        }
    }
}
