
namespace DataAccessEfCore.DbModels
{
    public class StyleIdealFor
    {
        public int StyleId { get; set; }

        public byte IdealForId { get; set; }

        // relationships

        public Style Style { get; set; }

        public IdealFor IdealFor { get; set; }
    }
}
