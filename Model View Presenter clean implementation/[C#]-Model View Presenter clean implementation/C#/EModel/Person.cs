using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EModel
{
    /// <summary>
    /// Just the data contract (A container which will not have any operations)
    /// </summary>
    public class Person
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string Dept { get; set; }
    }
}
