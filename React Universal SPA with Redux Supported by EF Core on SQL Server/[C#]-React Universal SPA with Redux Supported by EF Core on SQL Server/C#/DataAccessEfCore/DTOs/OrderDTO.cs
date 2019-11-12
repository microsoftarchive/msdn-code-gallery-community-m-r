using System;

namespace DataAccessEfCore.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }

        public Guid CustomerOrderId { get; set; }

        public string City { get; set; }

        public decimal TotalValue { get; set; }

        public string CreatedDateTime { get; set; }
    }
}
