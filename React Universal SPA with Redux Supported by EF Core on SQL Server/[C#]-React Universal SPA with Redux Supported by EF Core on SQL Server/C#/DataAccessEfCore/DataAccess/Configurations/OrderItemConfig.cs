using DataAccessEfCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessEfCore.DataAccess.Configurations
{
    class OrderItemConfig: IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(orderItem => orderItem.Price)
                .HasColumnType("decimal(8, 2)");

            builder.Property(orderItem => orderItem.SubTotal)
                .HasComputedColumnSql("[Price]*[Quantity]");
        }
    }
}
