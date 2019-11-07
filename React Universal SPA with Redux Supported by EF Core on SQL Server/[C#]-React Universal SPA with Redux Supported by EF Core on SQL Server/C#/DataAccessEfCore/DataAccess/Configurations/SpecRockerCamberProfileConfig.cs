using System;
using Microsoft.EntityFrameworkCore;
using DataAccessEfCore.DbModels;

namespace DataAccessEfCore.DataAccess.Configurations
{
    class SpecRockerCamberProfileConfig : IEntityTypeConfiguration<SpecRockerCamberProfile>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SpecRockerCamberProfile> builder)
        {
            builder.HasKey(specRockerCamberProfile => specRockerCamberProfile.RockerCamberProfileId);
        }
    }
}
