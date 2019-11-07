using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class SpecSingleValue
    {
        public int StyleId { get; set; }

        [Required]
        public byte DisplayIndex_Core { get; set; }

        [Required]
        public byte CoreId { get; set; }

        [Required]
        public byte DisplayIndex_Construction { get; set; }

        [Required]
        public byte ConstructionId { get; set; }

        [Required]
        public byte DisplayIndex_MadeIn { get; set; }

        [Required]
        public byte MadeInId { get; set; }

        // relationships

        public SpecCore Core { get; set; }

        public SpecConstruction Construction { get; set; }

        public Country MadeIn { get; set; }
    }
}
