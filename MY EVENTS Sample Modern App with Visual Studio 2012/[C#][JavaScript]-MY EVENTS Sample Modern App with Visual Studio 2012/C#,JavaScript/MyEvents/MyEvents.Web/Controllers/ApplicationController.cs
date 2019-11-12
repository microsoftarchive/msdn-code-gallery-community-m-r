using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEvents.Web.Controllers
{
    /// <summary>
    /// Application controller.
    /// </summary>
    public class ApplicationController : Controller
    {
        /// <summary>
        /// Sets the application mode to online.
        /// </summary>
        /// <returns></returns>
        public ActionResult Internet()
        {
            ConfigurationManager.AppSettings["OfflineMode"] = "false";
            return RedirectToAction("Index", "Home");
        }
        
        /// <summary>
        /// Sets the application mode to offline.
        /// </summary>
        /// <returns></returns>
        public ActionResult NoInternet()
        {
            ConfigurationManager.AppSettings["OfflineMode"] = "true";
            return RedirectToAction("Index", "Home");
        }

    }
}
