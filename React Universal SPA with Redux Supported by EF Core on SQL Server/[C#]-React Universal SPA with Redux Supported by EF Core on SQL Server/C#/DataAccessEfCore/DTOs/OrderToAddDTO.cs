using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DTOs
{
    public class OrderToAddDTO
    {
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        public int? UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        public short ProvinceId { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(100)]
        public string AddressLine { get; set; }

        [Required]
        [StringLength(7)]
        public string PostalCode { get; set; }

        [Required]
        public decimal TotalValue { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }

        [MinLength(1)]
        public IEnumerable<OrderItemToAddDTO> OrderItems { get; set; }
    }
}
