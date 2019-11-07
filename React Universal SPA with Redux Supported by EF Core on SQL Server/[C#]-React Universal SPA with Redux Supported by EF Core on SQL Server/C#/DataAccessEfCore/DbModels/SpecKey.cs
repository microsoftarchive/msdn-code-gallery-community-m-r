using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class SpecKey
    {
        public byte SpecKeyId { get; set; }

        [Required]
        [StringLength(50)]
        public string SpecKeyName { get; set; }

        [StringLength(1000)]
        public string SpecKeyDesc { get; set; }
    }
}
