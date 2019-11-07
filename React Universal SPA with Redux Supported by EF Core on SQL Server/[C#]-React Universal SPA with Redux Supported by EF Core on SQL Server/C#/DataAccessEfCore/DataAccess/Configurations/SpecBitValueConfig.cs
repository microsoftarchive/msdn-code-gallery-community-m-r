using DataAccessEfCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessEfCore.DataAccess.Configurations
{
    public class SpecBitValueConfig: IEntityTypeConfiguration<SpecBitValue>
    {
        public void Configure(EntityTypeBuilder<SpecBitValue> builder)
        {
            builder.HasKey(specBitValue => new {specBitValue.StyleId, specBitValue.DisplayIndex});


        }
    }
}
