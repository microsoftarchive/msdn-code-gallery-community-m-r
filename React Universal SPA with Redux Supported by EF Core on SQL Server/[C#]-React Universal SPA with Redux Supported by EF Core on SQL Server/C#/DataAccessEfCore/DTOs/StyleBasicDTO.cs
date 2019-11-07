
namespace DataAccessEfCore.DTOs
{
    public class StyleBasicDTO
    {
        public int StyleId { get; set; }
        public string StyleName { get; set; }
        public byte CategoryId { get; set; }
        public string BrandName { get; set; }
        public string GenderName { get; set; }
        public string ImageSmall { get; set; }
        public decimal PriceCurrent { get; set; }
        public decimal PriceRegular { get; set; }
    }
}
