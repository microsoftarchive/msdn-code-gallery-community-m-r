using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class Category
    {
        public byte CategoryId { get; set; }

        [Required]
        [StringLength(30)]
        public string CategoryName { get; set; }
    }
}
