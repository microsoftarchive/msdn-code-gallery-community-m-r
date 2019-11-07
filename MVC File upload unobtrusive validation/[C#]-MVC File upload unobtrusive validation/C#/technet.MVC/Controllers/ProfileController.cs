using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using technet.MVC.Models;

namespace technet.MVC.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(new ProfileModel());
        }

        [HttpPost]
        public ActionResult Index(
            ProfileModel model)
        { 
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            return View(model);
        }
    }
}