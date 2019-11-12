using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShanuAngularMenuCreation.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FGIN()
        { 
            return View();
        }
        public ActionResult FGOUT()
        {
            return View();
        }
    }
}