using DataAccessEfCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessEfCore.DataAccess.Configurations
{
    public class StyleConfig: IEntityTypeConfiguration<Style>
    {
        public void Configure(EntityTypeBuilder<Style> builder)
        {
            builder.Property(style => style.PriceCurrent)
                .HasColumnType("decimal(8, 2)");

            builder.Property(style => style.PriceRegular)
                .HasColumnType("decimal(8, 2)");

            // model-level query filter

            builder.HasQueryFilter(style => !style.SoftDeleted);

            // relationships

            builder.HasOne(style => style.Category)
                .WithMany()
                .HasForeignKey(style => style.CategoryId);

            builder.HasOne(style => style.Brand)
                .WithMany()
                .HasForeignKey(style => style.BrandId);

            builder.HasOne(style => style.Gender)
                .WithMany()
                .HasForeignKey(style => style.GenderId);

            builder.HasOne(style => style.StyleState)
                .WithOne()
                .HasForeignKey<StyleState>(styleState => styleState.StyleId);


        }
    }
}
