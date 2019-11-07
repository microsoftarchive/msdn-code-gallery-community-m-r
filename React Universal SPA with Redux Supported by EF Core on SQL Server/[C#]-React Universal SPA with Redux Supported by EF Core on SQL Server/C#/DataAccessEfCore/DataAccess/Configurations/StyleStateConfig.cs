using DataAccessEfCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessEfCore.DataAccess.Configurations
{
    class StyleStateConfig: IEntityTypeConfiguration<StyleState>
    {
        public void Configure(EntityTypeBuilder<StyleState> builder)
        {
            builder.HasKey(styleState => styleState.StyleId);
        }
    }
}
