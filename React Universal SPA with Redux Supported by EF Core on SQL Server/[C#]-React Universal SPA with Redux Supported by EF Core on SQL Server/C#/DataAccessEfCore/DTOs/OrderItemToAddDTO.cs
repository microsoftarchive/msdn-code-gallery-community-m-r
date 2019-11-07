using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DTOs
{
    public class OrderItemToAddDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int SkuId { get; set; }

        [Required]
        [Range(1.0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public short Quantity { get; set; }
    }
}
