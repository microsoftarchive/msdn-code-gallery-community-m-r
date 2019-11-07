using DataAccessEfCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessEfCore.DataAccess.Configurations
{
    public class OrderConfig: IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(order => order.TotalValue)
                .HasColumnType("decimal(10, 2)");

            builder.Property(order => order.CreatedDateTime)
                .IsRequired();

            // relationship

            builder.HasOne(order => order.Province)
                .WithMany()
                .HasForeignKey(order => order.ProvinceId);
        }
    }
}
