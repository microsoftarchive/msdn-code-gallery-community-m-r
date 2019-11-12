
namespace DataAccessEfCore.DbModels
{
    public class StyleState
    {
        public int StyleId { get; set; }

        public decimal AverageRatings { get; set; }

        public int ReviewCount { get; set; }

        public bool SoldOut { get; set; }
    }
}
