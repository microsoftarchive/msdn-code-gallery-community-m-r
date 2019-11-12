using DataAccessEfCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessEfCore.DataAccess.Configurations
{
    public class DescriptionConfig: IEntityTypeConfiguration<Description>
    {
        public void Configure(EntityTypeBuilder<Description> builder)
        {
            builder.HasKey(desc => new { desc.StyleId, desc.DisplayIndex });

            // relationships

            builder.HasOne(desc => desc.Style)
                .WithMany(style => style.Descriptions)
                .HasForeignKey(desc => desc.StyleId);
        }
    }
}
