using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MasterDetail.Models
{
    public class SalesMain
    {
        
        [Key]
        public int SalesId { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime SalesDate { get; set; }
        public string SalesPerson { get; set; }

        public virtual ICollection<SalesSub> SalesSubs { get; set; }
    }
}