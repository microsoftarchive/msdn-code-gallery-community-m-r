using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int SkuId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public decimal SubTotal { get; set; }

        // relationships

        public Sku Sku { get; set; }
    }
}
