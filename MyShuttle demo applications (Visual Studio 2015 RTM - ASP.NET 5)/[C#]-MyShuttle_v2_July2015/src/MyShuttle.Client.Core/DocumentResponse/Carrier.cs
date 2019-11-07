

namespace MyShuttle.Client.Core.DocumentResponse
{
    using System.Collections.Generic;

    public class Carrier
    {
        public int CarrierId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CompanyID { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public byte[] Picture { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }

        public ICollection<Driver> Drivers { get; set; }
    }
}