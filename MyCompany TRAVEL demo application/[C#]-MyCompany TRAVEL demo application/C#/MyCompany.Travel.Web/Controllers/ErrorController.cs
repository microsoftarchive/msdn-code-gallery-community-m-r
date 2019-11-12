using System;
namespace MyCompany.Travel.Web.Controllers
{
    using MyCompany.Travel.Web.Infraestructure.Security;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;


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
