
namespace MyCompany.Expenses.Web.Mobile.Controllers
{
    using MyCompany.Common.CrossCutting;
    using MyCompany.Expenses.Web.Mobile.Security;
    using System.Web.Mvc;

    /// <summary>
    /// Home Controller
    /// </summary>
    [CustomAuthorizeAttribute]
    public class HomeController : Controller
    {
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            TraceManager.TraceInfo("HomeController");

            ViewBag.SecurityToken = Session["securityToken"];
            if (Session["isNoAuth"] != null)
            {
                ViewBag.IsNoAuth = Session["isNoAuth"].ToString().ToLower();
            }

            return View();
        }

        /// <summary>
        /// Detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        /// <summary>
        /// About
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// Contact
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}