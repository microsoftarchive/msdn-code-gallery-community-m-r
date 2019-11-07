using DataAccessEfCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessEfCore.DataAccess.Configurations
{
    public class SpecCoreConfig: IEntityTypeConfiguration<SpecCore>
    {
        public void Configure(EntityTypeBuilder<SpecCore> builder)
        {
            builder.HasKey(specCore => specCore.CoreId);
        }
    }
}
