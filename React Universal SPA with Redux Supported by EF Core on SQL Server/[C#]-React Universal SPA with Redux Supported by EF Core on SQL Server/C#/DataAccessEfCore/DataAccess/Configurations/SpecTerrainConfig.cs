using DataAccessEfCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessEfCore.DataAccess.Configurations
{
    public class SpecTerrainConfig: IEntityTypeConfiguration<SpecTerrain>
    {
        public void Configure(EntityTypeBuilder<SpecTerrain> builder)
        {
            builder.HasKey(specTerrain => specTerrain.TerrainId);
        }
    }
}
