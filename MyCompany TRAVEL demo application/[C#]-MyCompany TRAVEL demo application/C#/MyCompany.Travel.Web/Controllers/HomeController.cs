namespace MyCompany.Travel.Web.Controllers
{
    using MyCompany.Travel.Web.Infraestructure.Security;
    using System.Web.Mvc;


    /// <summary>
    /// Home Controller
    /// </summary>
    public class HomeController : Controller
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
