using DataAccessEfCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessEfCore.DataAccess.Configurations
{
    class SpecAbilityConfig: IEntityTypeConfiguration<SpecAbility>
    {
        public void Configure(EntityTypeBuilder<SpecAbility> builder)
        {
            builder.HasKey(ability => ability.AbilityId);
        }
    }
}
