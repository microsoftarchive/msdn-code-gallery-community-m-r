using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class SpecBitValue
    {
        public int StyleId { get; set; }

        [Required]
        public byte DisplayIndex { get; set; }

        [Required]
        public byte SpecKeyId { get; set; }

        [Required]
        public bool SpecValue { get; set; }

        // relationships

        public SpecKey SpecKey { get; set; }
    }
}
