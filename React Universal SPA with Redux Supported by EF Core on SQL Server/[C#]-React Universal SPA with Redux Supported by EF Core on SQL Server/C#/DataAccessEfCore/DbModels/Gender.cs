using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class Gender
    {
        public byte GenderId { get; set; }

        [Required]
        [StringLength(30)]
        public string GenderName { get; set; }
    }
}
