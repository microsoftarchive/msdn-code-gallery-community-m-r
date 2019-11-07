
namespace DataAccessEfCore.DbModels
{
    public class StyleTerrain
    {
        public int StyleId { get; set; }

        public byte TerrainId { get; set; }

        // relationships

        public Style Style { get; set; }

        public SpecTerrain Terrain { get; set; }
    }
}
