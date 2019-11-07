using DataAccessEfCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessEfCore.DataAccess.Configurations
{
    class StyleIdealForConfig: IEntityTypeConfiguration<StyleIdealFor>
    {
        public void Configure(EntityTypeBuilder<StyleIdealFor> builder)
        {
            builder.HasKey(styleIdealFor => new {styleIdealFor.StyleId, styleIdealFor.IdealForId});

            // relationships

            builder.HasOne(styleIdealFor => styleIdealFor.Style)
                .WithMany(style => style.StyleIdealFors)
                .HasForeignKey(styleIdealFor => styleIdealFor.StyleId);

            builder.HasOne(styleIdealFor => styleIdealFor.IdealFor)
                .WithMany(idealFor => idealFor.StyleIdealFors)
                .HasForeignKey(styleIdealFor => styleIdealFor.IdealForId);

        }
    }
}
