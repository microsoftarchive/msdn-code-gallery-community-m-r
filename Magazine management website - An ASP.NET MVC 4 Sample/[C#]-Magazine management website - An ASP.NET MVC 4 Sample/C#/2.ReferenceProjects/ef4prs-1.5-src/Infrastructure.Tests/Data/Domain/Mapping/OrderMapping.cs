using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Tests.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Infrastructure.Tests.Data.Domain.Mapping
{
    public class OrderMapping : EntityMappingBase<Order>
    {
        public OrderMapping()
        {
            HasKey(x => x.Id);

            Property(x => x.OrderDate);
            Property(x => x.CustomerId);

            HasRequired<Customer>(x => x.Customer)
                .WithMany(y => y.Orders)                
                .HasForeignKey(o => o.CustomerId);
                
            ToTable("Order");            
        }
    }
}
