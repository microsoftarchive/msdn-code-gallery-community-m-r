using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivetWPFSampleApp.Models
{
    public class PersonChanged
    {
        public Person Person { get; private set; }

        public PersonChanged(Person person)
        {
            this.Person = person;
        }
    }
}
