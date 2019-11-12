using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int CustomerId { get; set; }

        public byte[] Picture { get; set; }

        public Customer Customer { get; set; }

        public virtual ICollection<Ride> Rides{ get; set; }


        // This properties are needed to integrate the database with Mobile Services
        public DateTimeOffset? CreatedAt { get; set; }

        public bool Deleted { get; set; }

        public string Id { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public byte[] Version { get; set; }
    }
}
