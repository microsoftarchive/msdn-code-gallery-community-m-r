using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class Province
    {
        public short ProvinceId { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string ProvinceCode { get; set; }

        [Required]
        [StringLength(50)]
        public string ProvinceName { get; set; }

        [Required]
        public byte CountryId { get; set; }

        // relationships

        public Country Country { get; set; }
    }
}
