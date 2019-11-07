using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQ101Samples
{
    public class Order
    {
        public Order(int orderID, DateTime orderDate, decimal total)
        {
            OrderID = orderID;
            OrderDate = orderDate;
            Total = total;
        }

        public Order() { }

        public int OrderID;
        public DateTime OrderDate;
        public decimal Total;
    }
}
