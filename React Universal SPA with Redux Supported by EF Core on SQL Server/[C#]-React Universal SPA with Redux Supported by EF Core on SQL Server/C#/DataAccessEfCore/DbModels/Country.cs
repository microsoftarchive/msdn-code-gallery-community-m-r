using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class Country
    {
        public byte CountryId { get; set; }

        [Required]
        [StringLength(50)]
        public string CountryName { get; set; }
    }
}
