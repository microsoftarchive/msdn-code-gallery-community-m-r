using DataAccessEfCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessEfCore.DataAccess.Configurations
{
    class SpecTextValueConfig: IEntityTypeConfiguration<SpecTextValue>
    {
        public void Configure(EntityTypeBuilder<SpecTextValue> builder)
        {
            builder.HasKey(specTextValue => specTextValue.StyleId);

            // relationships

            builder.HasOne(specTextValue => specTextValue.SpecKey)
                .WithMany()
                .HasForeignKey(specTextValue => specTextValue.SpecKeyId);
        }
    }
}
