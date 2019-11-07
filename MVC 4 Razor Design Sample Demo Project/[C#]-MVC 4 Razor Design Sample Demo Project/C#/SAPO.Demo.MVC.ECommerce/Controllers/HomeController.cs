using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAPO.Demo.MVC.ECommerce.Controllers
{
    public class HomeController : Controller
    {     
        // GET: /Home/
        //public string Index()
        //{
        //    return "You're at Home";
        //}

        public ActionResult Index()
        {
            return View();
        }
    }
}
