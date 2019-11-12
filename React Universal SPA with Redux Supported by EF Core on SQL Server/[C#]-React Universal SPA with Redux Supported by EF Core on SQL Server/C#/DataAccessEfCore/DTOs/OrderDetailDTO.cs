using System;
using System.Collections.Generic;

namespace DataAccessEfCore.DTOs
{
    public class OrderDetailDTO
    {
        public int OrderId { get; set; }

        public Guid CustomerOrderId { get; set; }

        public string FullName { get; set; }

        public string ProvinceName { get; set; }

        public string City { get; set; }

        public string AddressLine { get; set; }

        public string PostalCode { get; set; }

        public decimal TotalValue { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public IEnumerable<OrderItemDTO> OrderItems { get; set; }
    }
}
