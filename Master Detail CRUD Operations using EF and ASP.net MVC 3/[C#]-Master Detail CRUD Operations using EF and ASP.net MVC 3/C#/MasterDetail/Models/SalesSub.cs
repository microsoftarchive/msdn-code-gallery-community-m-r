using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MasterDetail.Models
{
    public class SalesSub
    {
        
        [Key, Column(Order = 0)]
        public int SalesId { get; set; }

        [Key, Column(Order = 1)]
        public string ItemName { get; set; }

        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }


        public virtual SalesMain SalesMain { get; set; }
    }
}