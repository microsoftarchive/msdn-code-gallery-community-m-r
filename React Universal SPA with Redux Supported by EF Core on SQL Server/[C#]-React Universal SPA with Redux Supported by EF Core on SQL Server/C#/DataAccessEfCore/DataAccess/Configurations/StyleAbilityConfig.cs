using DataAccessEfCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessEfCore.DataAccess.Configurations
{
    public class StyleAbilityConfig: IEntityTypeConfiguration<StyleAbility>
    {
        public void Configure(EntityTypeBuilder<StyleAbility> builder)
        {
            builder.HasKey(styleAbility => new {styleAbility.StyleId, styleAbility.AbilityId});

            // relationships

            builder.HasOne(styleAbility => styleAbility.Style)
                .WithMany(style => style.StyleAbilities)
                .HasForeignKey(styleAbility => styleAbility.StyleId);

            builder.HasOne(styleAbility => styleAbility.Ability)
                .WithMany(ability => ability.StyleAbilities)
                .HasForeignKey(styleAbility => styleAbility.AbilityId);
        }
    }
}
