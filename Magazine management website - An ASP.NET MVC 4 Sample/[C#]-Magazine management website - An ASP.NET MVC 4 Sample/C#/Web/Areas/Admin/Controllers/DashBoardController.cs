namespace CIK.News.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    using CIK.News.Web.Infras;
    using CIK.News.Web.Infras.ActionResults.Admin;
    using CIK.News.Web.Infras.ViewModels.Admin.DashBoard;

    [Authorize]
    public class DashBoardController : BaseController
    {
        public ActionResult Index(int page = 1)
        {
            var viewModel = new DashBoardViewModel();
            TryUpdateModel(viewModel);

            return new DashBoardViewModelActionResult<DashBoardController>(
                x => x.Index(page),
                viewModel.TitleSearchText ?? string.Empty,
                page);
        }
    }
}