using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DTOs
{
    public class ReviewAddDTO
    {
        [Required]
        public int StyleId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public byte Rating { get; set; }

        [Required]
        [MinLength(10)]
        public string ReviewText { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
    }
}
