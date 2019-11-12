using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Model.Domain;

namespace Infrastructure.Tests.Data.Domain
{
    public class OrderLine : Entity
    {
        public virtual Product Product 
        { 
            get; set; 
        }

        public virtual int Quantity 
        { 
            get; set; 
        }

        public virtual double Price 
        { 
            get; set; 
        }

        public virtual Order Order 
        { 
            get; set; 
        }

        // for information on why we want this 'extra' property, see:
        // http://stuartgatenby.com/ef/2011/03/05/entity-framework-relationship-mapping-best-of-both-worlds-ef4-1ctp5-code-only-fluent-api/
        public virtual int OrderId
        {
            get;
            set;
        }

        // for information on why we want this 'extra' property, see:
        // http://stuartgatenby.com/ef/2011/03/05/entity-framework-relationship-mapping-best-of-both-worlds-ef4-1ctp5-code-only-fluent-api/
        public virtual int ProductId
        {
            get;
            set;
        }
    }
}
