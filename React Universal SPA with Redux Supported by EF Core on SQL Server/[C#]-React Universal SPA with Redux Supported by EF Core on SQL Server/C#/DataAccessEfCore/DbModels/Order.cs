using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessEfCore.DbModels
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required]
        public Guid CustomerOrderId { get; set; }

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

        public DateTime CreatedDateTime { get; set; }

        public DateTime CreatedDateTimeUTC { get; set; }

        // relationships 

        public Province Province { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
