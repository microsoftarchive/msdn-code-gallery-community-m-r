using DataAccessEfCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessEfCore.DataAccess.Configurations
{
    public class SkuConfig: IEntityTypeConfiguration<Sku>
    {
        public void Configure(EntityTypeBuilder<Sku> builder)
        {
            // model-level query filter

            builder.HasQueryFilter(sku => !sku.SoftDeleted);

            // relationships

            builder.HasOne(sku => sku.Style)
                .WithMany(style => style.Skus)
                .HasForeignKey(sku => sku.StyleId);


        }
    }
}
