using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class Brand
    {
        public short BrandId { get; set; }

        [Required]
        [StringLength(50)]
        public string BrandName { get; set; }

        [Required]
        public byte CountryId { get; set; }

        public bool SoftDeleted { get; set; }

        // relationships
        public Country Country { get; set; }

    }

}
