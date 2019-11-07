using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class SpecTextValue
    {
        public int StyleId { get; set; }

        [Required]
        public byte DisplayIndex { get; set; }

        [Required]
        public byte SpecKeyId { get; set; }

        [Required]
        [StringLength(300)]
        public string TextValue { get; set; }

        // relationships

        public SpecKey SpecKey { get; set; }
    }
}
