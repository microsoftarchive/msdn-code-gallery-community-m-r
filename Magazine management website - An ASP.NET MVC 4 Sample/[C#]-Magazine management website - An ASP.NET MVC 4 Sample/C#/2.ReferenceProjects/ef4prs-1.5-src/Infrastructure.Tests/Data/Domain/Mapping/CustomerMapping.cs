using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Tests.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Infrastructure.Tests.Data.Domain.Mapping
{
    public class CustomerMapping : EntityMappingBase<Customer>
    {
        public CustomerMapping()
        {
            HasKey(x => x.Id);

            Property(x => x.Firstname).IsRequired().HasMaxLength(25);
            Property(x => x.Lastname).IsRequired().HasMaxLength(25);
            Property(x => x.Inserted);

            HasMany(x => x.Orders)
                .WithRequired(y => y.Customer);

            ToTable("Customer");
        }
    }
}
