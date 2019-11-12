
namespace DataAccessEfCore.DbModels
{
    public class vwStyleIdealFor
    {
        public int StyleId { get; set; }
        public string StyleName { get; set; }
        public byte CategoryId { get; set; }
        public string CategoryName { get; set; }
        public short BrandId { get; set; }
        public string BrandName { get; set; }
        public byte GenderId { get; set; }
        public string GenderName { get; set; }
        public string ImageSmall { get; set; }
        public string ImageBig { get; set; }
        public decimal PriceCurrent { get; set; }
        public decimal PriceRegular { get; set; }
        public bool SoftDeleted { get; set; }
        public decimal AverageRatings { get; set; }
        public int ReviewCount { get; set; }
        public bool SoldOut { get; set; }

        public byte IdealForId { get; set; }
        public string IdealForSpec { get; set; }
    }
}
