using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Infrastructure.Model.Domain;

namespace Infrastructure.Tests.Data.Domain.Mapping
{
    public abstract class EntityMappingBase<T> : EntityTypeConfiguration<T> where T : Entity
    {
        public EntityMappingBase()
        {
            HasKey(x => x.Id);
        }
    }
}
