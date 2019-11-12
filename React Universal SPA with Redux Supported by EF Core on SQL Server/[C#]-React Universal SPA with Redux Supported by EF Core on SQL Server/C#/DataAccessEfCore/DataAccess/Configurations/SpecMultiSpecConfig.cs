using DataAccessEfCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessEfCore.DataAccess.Configurations
{
    public class SpecMultiSpecConfig: IEntityTypeConfiguration<SpecMultiValue>
    {
        public void Configure(EntityTypeBuilder<SpecMultiValue> builder)
        {
            builder.HasKey(specMultiValue => specMultiValue.StyleId);
        }
    }
}
