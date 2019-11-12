using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class SpecTerrain
    {
        public byte TerrainId { get; set; }

        [Required]
        [StringLength(50)]
        public string TerrainSpec { get; set; }

        // relationships

        public ICollection<StyleTerrain> StyleTerrains { get; set; }
    }
}
