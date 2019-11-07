using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class SpecConstruction
    {
        public byte ConstructionId { get; set; }

        [Required]
        [StringLength(50)]
        public string ConstructionSpec { get; set; }
    }
}
