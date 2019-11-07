using System.Web.Mvc;

namespace MyEvents.Web.Areas.Organizer.Controllers
{
    /// <summary>
    /// Error controller.
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// Index action.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

    }
}
