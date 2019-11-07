using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Tests.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Infrastructure.Tests.Data.Domain.Mapping
{
    public class OrderLineMapping : EntityMappingBase<OrderLine>
    {
        public OrderLineMapping()
        {                        
            HasKey(x => x.Id);

            Property(x => x.Price);
            Property(x => x.Quantity);
            Property(x => x.ProductId);
            Property(x => x.OrderId);

            HasRequired(x => x.Order)
                .WithMany(o => o.OrderLines)
                .HasForeignKey(ol => ol.OrderId);

            HasRequired(x => x.Product)
                .WithMany(p => p.OrderLines)
                .HasForeignKey(ol => ol.ProductId);

            ToTable("OrderLine");
        }
    }
}
