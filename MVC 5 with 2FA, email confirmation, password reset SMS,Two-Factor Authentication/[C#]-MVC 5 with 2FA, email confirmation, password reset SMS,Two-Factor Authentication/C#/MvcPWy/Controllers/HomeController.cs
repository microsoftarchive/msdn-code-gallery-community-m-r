using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPWy.Controllers
{
   public class HomeController : Controller
   {
public ActionResult Index()
{
   ViewBag.Link = TempData["ViewBagLink"];  
   return View();
}

      public ActionResult About()
      {
         ViewBag.Message = "Your application description page.";

         return View();
      }

[Authorize]
public ActionResult Contact()
{
   ViewBag.Message = "Your contact page.";

   return View();
}
   }
}