using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Model.Domain;

namespace Infrastructure.Tests.Data.Domain
{
    public class Order : Entity
    {
        public Order()
        {
            OrderLines = new List<OrderLine>();
        }
        
        public virtual IList<OrderLine> OrderLines
        {
            get;
            set;
        }

        public virtual DateTime OrderDate 
        { 
            get; set; 
        }

        public virtual Customer Customer 
        { 
            get; set; 
        }

        // for information on why we want this 'extra' property, see:
        // http://stuartgatenby.com/ef/2011/03/05/entity-framework-relationship-mapping-best-of-both-worlds-ef4-1ctp5-code-only-fluent-api/
        public virtual int CustomerId
        {
            get;
            set;
        }
    }
}
