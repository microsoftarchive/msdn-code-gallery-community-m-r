using DataAccessEfCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessEfCore.DataAccess.Configurations
{
    class SpecSnowConditionConfig: IEntityTypeConfiguration<SpecSnowCondition>
    {
        public void Configure(EntityTypeBuilder<SpecSnowCondition> builder)
        {
            builder.HasKey(specSnowCondition => specSnowCondition.SnowConditionId);
        }
    }
}
