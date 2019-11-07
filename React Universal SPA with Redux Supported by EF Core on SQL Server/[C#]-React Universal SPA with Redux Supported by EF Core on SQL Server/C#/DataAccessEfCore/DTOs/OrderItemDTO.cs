
namespace DataAccessEfCore.DTOs
{
    public class OrderItemDTO
    {
        public int SkuId { get; set; }

        public string Skis { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal SubTotal { get; set; }
    }
}
