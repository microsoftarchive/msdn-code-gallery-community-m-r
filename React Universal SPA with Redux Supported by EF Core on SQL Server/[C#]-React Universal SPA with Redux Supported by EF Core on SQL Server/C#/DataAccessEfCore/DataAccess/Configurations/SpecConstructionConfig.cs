using DataAccessEfCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessEfCore.DataAccess.Configurations
{
    class SpecConstructionConfig: IEntityTypeConfiguration<SpecConstruction>
    {
        public void Configure(EntityTypeBuilder<SpecConstruction> builder)
        {
            builder.HasKey(specConstruction => specConstruction.ConstructionId);
        }
    }
}
