
namespace DataAccessEfCore.DTOs
{
    public class StyleForListDTO: StyleBasicDTO
    {
        public decimal AverageRatings { get; set; }
        public int ReviewCount { get; set; }
        public bool SoldOut { get; set; }

    }
}
