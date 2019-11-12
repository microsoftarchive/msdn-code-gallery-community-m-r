
namespace DataAccessEfCore.DbModels
{
    public class StyleRockerCamberProfile
    {
        public int StyleId { get; set; }

        public byte RockerCamberProfileId { get; set; }

        // relationships

        public Style Style { get; set; }

        public SpecRockerCamberProfile RockerCamberProfile { get; set; }
    }
}
