using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCompany.Vacation.Web.Controllers
{
    /// <summary>
    /// Error Controller
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// Default Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}
