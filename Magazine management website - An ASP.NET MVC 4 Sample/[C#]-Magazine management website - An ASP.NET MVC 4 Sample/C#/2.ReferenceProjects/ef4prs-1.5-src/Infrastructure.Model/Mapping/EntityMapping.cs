using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Infrastructure.Model.Domain;


namespace Infrastructure.Model.Mapping
{
    public abstract class EntityMapping<T> : EntityTypeConfiguration<T> where T : Entity
    {
        protected EntityMapping()
        {
            HasKey(x => x.Id);
        }
    }
}
