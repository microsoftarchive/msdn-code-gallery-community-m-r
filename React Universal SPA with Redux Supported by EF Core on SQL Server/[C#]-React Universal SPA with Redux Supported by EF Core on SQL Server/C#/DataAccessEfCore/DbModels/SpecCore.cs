using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class SpecCore
    {
        public byte CoreId { get; set; }

        [Required]
        [StringLength(50)]
        public string CoreSpec { get; set; }
    }
}
