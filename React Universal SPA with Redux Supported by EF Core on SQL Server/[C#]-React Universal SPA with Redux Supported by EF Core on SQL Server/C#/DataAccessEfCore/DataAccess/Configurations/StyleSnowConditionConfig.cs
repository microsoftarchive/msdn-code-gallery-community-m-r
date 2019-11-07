using DataAccessEfCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessEfCore.DataAccess.Configurations
{
    public class StyleSnowConditionConfig: IEntityTypeConfiguration<StyleSnowCondition>
    {
        public void Configure(EntityTypeBuilder<StyleSnowCondition> builder)
        {
            builder.HasKey(styleSnowCondition => new
            {
                styleSnowCondition.StyleId,
                styleSnowCondition.SnowConditionId
            });

            // relationships

            builder.HasOne(styleSnowCondition => styleSnowCondition.Style)
                .WithMany(style => style.StyleSnowConditions)
                .HasForeignKey(styleSnowCondition => styleSnowCondition.StyleId);

            builder.HasOne(styleSnowCondition => styleSnowCondition.SnowCondition)
                .WithMany(snowCondition => snowCondition.StyleSnowConditions)
                .HasForeignKey(styleSnowCondition => styleSnowCondition.SnowConditionId);

        }

    }
}
