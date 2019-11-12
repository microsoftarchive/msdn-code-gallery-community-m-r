using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class Sku
    {
        public int SkuId { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 8)]
        [RegularExpression(@"^[0-9]{8}$")]
        public string SkuNo { get; set; }

        [Required]
        public int StyleId { get; set; }

        [Required]
        [StringLength(20)]
        public string Size { get; set; }

        [Required]
        public short Quantity { get; set; }

        public bool SoftDeleted { get; set; }

        // relationships

        public Style Style { get; set; }
    }
}
