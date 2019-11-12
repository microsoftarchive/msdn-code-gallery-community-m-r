using DataAccessEfCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessEfCore.DataAccess.Configurations
{
    public class StyleRockerCamberProfileConfig : IEntityTypeConfiguration<StyleRockerCamberProfile>
    {
        public void Configure(EntityTypeBuilder<StyleRockerCamberProfile> builder)
        {
            builder.HasKey(styleRockerCamberProfile => new
            {
                styleRockerCamberProfile.StyleId,
                styleRockerCamberProfile.RockerCamberProfileId
            });

            // relationships

            builder.HasOne(styleRockerCamberProfile => styleRockerCamberProfile.Style)
                .WithMany(style => style.StyleRockerCamberProfiles)
                .HasForeignKey(styleRockerCamberProfile => styleRockerCamberProfile.StyleId);

            builder.HasOne(styleRockerCamberProfile => styleRockerCamberProfile.RockerCamberProfile)
                .WithMany(rockerCamberProfile => rockerCamberProfile.StyleRockerCamberProfiles)
                .HasForeignKey(styleRockerCamberProfile => styleRockerCamberProfile.RockerCamberProfileId);
        }
    }
}
