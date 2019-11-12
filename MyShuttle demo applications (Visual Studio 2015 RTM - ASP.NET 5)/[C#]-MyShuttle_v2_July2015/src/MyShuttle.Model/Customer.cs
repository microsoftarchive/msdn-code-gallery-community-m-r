
namespace MyShuttle.Model
{
    using System.Collections.Generic;

    public class Customer
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string CompanyID { get; set; }


        public string Address { get; set; }


        public string ZipCode { get; set; }


        public string City { get; set; }


        public string State { get; set; }


        public string Country { get; set; }


        public string Phone { get; set; }


        public string Email { get; set; }


        public ICollection<Employee> Employees { get; set; }

    }
}
