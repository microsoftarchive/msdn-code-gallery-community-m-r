using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class UserIdentity
    {
        public int UserId { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string ScreenName { get; set; }

        public DateTime LastLogInDateTime { get; set; }

        public DateTime LastlogInDateTimeUTC { get; set; }
    }
}
