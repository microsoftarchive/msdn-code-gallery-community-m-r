using DataAccessEfCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessEfCore.DataAccess.Configurations
{
    public class SpecSingleValueConfig: IEntityTypeConfiguration<SpecSingleValue>
    {
        public void Configure(EntityTypeBuilder<SpecSingleValue> builder)
        {
            builder.HasKey(specSingleValue => specSingleValue.StyleId);

            // relationships
            builder.HasOne(specSingleValue => specSingleValue.Core)
                .WithMany()
                .HasForeignKey(specSingleValue => specSingleValue.CoreId);

            builder.HasOne(specSingleValue => specSingleValue.Construction)
                .WithMany()
                .HasForeignKey(specSingleValue => specSingleValue.ConstructionId);

            builder.HasOne(specSingleValue => specSingleValue.MadeIn)
                .WithMany()
                .HasForeignKey(specSingleValue => specSingleValue.MadeInId);
        }

    }
}
