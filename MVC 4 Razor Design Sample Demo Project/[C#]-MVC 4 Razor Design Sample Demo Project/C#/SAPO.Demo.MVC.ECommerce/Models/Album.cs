using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAPO.Demo.MVC.ECommerce.Models
{
    public class Album
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public bool IsBookMarked { get; set; }   

    }
}