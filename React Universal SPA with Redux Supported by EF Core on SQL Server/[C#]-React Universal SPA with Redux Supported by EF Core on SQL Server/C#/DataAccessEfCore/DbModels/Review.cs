using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class Review
    {
        public int ReviewId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int StyleId { get; set; }

        [Required]
        [Range(1, 5)]
        public byte Rating { get; set; }

        [Required]
        public string ReviewText { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }

        public DateTime CreatedDateTimeUTC { get; set; }

        // relationships

        public UserIdentity User { get; set; }

        public Style Style { get; set; }
     }
}
