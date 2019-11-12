using DataAccessEfCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessEfCore.DataAccess.Configurations
{
    public class ReviewConfig: IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.Property(review => review.CreatedDateTime)
                .IsRequired();

            // relationships

            builder.HasOne(review => review.Style)
                .WithMany(style => style.Reviews)
                .HasForeignKey(review => review.StyleId);
        }
    }
}
