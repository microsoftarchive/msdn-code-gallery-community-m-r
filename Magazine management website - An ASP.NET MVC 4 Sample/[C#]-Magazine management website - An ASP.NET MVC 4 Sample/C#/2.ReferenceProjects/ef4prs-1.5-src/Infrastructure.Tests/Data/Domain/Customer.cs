using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Model.Domain;


namespace Infrastructure.Tests.Data.Domain
{
    public class Customer : Entity
    {
        public Customer()
        {
            Orders = new List<Order>();
        }

        public virtual string Firstname 
        { 
            get; set; 
        }

        public virtual string Lastname 
        { 
            get; set; 
        }

        public virtual IList<Order> Orders
        {
            get;
            set;
        }

        public virtual DateTime Inserted 
        { 
            get; set; 
        }
    }
}
