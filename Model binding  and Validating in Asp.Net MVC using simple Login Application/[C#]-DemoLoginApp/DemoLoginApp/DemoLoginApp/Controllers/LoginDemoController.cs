using DemoLoginApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoLoginApp.Controllers
{
    public class LoginDemoController : Controller
    {

        SuryaTechDBEntities _dbEntites = new SuryaTechDBEntities();
        // GET: /LoginDemo/
        public ActionResult Index()
        
        {
            return View();
        }
        [HttpPost]
        
        public ActionResult Index([Bind(Exclude="iId")]tbl_RegisterUser model)
        {
            var type = Request.RequestType;
            if (ModelState.Values.ToArray()[0].Errors.Count==0 && ModelState.Values.ToArray()[1].Errors.Count==0)
            {
                ViewBag.count = 0;
                if (CheckUser(model.sUserName,model.sPassword))
                {
                    return RedirectToAction("Home", new { username=model.sUserName});
                }
                else
                {
                   
                    ModelState.Clear();
                    ModelState.AddModelError("", "User Id or Password not exist; try again !!!");
                    return View(model);
                }
               
            }
            else
            {
                ViewBag.count = 1;
                return View(model);
            }
            
        }

        private bool CheckUser(string UserId, string Password)
        {
            var User = _dbEntites.tbl_RegisterUser.FirstOrDefault(m => m.sUserName == UserId);
            if (User!=null)
            {
                if (User.sPassword == Password)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        [HttpPost]
        public ActionResult Register(tbl_RegisterUser model)
         {
            List<string> errorMessage = new List<string>();
            if (ModelState.IsValid)
            {
                _dbEntites.tbl_RegisterUser.Add(model);
                _dbEntites.SaveChanges();
                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                for (int i = 0; i < ModelState.Values.Count; i++)
                {
                    if (ModelState.Values.ToArray()[i].Errors.Count>0)
                    {
                        errorMessage.Add(ModelState.Values.ToArray()[i].Errors[0].ErrorMessage);
                    }
                    else
                    {
                        errorMessage.Add("");
                    }
                }

                return Json(new {keys= ModelState.Keys,ErrorMessage=errorMessage }, JsonRequestBehavior.AllowGet);
            }
        }
       
        public ActionResult Home(string username)
         {
            ViewData.Model = username;
            return View();
        }
        public ActionResult Logout()
        {
            return RedirectToAction("Index");
        }
	}
}