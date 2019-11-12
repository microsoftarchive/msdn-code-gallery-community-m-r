using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class Description
    {
        public byte DisplayIndex { get; set; }

        [Required]
        public int StyleId { get; set; }

        [Required]
        [StringLength(1000)]
        public string DescText { get; set; }

        // relationships
        
        public Style Style { get; set; }
    }
}
