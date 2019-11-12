namespace CIK.News.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    using CIK.News.Web.Infras;

    [Authorize]
    public class CategoryController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
