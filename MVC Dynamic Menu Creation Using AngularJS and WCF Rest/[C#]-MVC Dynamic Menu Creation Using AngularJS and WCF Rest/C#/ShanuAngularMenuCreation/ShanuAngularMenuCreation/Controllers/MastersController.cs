using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShanuAngularMenuCreation.Controllers
{
    public class MastersController : Controller
    {
        // GET: Masters
        public ActionResult Index()
        { return View(); }


        public ActionResult ItemDetails()
        { return View(); }

        public ActionResult ItemManage()
        { return View(); }

        public ActionResult CATDetails()
        { return View(); }

        public ActionResult CATManage()
        { return View(); }
    }
}