using DataAccessEfCore.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessEfCore.DataAccess.Configurations
{
    public class StyleTerrainConfig: IEntityTypeConfiguration<StyleTerrain>
    {
        public void Configure(EntityTypeBuilder<StyleTerrain> builder)
        {
            builder.HasKey(styleTerrain => new {styleTerrain.StyleId, styleTerrain.TerrainId});

            // relationships

            builder.HasOne(styleTerrain => styleTerrain.Style)
                .WithMany(style => style.StyleTerrains)
                .HasForeignKey(styleTerrain => styleTerrain.StyleId);

            builder.HasOne(styleTerrain => styleTerrain.Terrain)
                .WithMany(terrain => terrain.StyleTerrains)
                .HasForeignKey(styleTerrain => styleTerrain.TerrainId);
        }
    }
}
