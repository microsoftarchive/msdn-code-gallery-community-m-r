using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBarCodec.Core;

namespace WebBarCodec.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "MVC WEB BARCODE!!";
            string vCode = "nha3mien.com";
            string barCode = BarCodeToHTML.get39(vCode, 2, 20);
            ViewBag.htmlBarcode = barCode;
            ViewBag.vCode = vCode;
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection f)
        {
            ViewBag.Message = "MVC WEB BARCODE!";
            var vCode = f["txtcode"];
            string barCode = BarCodeToHTML.get39(vCode, 2, 20);
            ViewBag.htmlBarcode = barCode;
            ViewBag.vCode = vCode;

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
