namespace CIK.News.Web.Controllers
{
    using System.Web.Mvc;

    using CIK.News.Web.Infras;
    using CIK.News.Web.Infras.ActionResults.Client;

    [Authorize]
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return new HomePageViewModelActionResult<HomeController>(x=>x.Index());
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            return new DetailsViewModelActionResult<HomeController>(x => x.Details(id), id);
        }

        [AllowAnonymous]
        public ActionResult Category(int id)
        {
            return new CategoryViewModelActionResult<HomeController>(x => x.Category(id), id);
        }
    }
}
