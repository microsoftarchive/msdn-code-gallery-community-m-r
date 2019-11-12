namespace CIK.News.Web.Infras.ActionResults.Admin
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using CIK.News.Entities.NewsAgg;
    using CIK.News.Framework;
    using CIK.News.Framework.Extensions;
    using CIK.News.Web.Infras.ViewModels;
    using CIK.News.Web.Infras.ViewModels.Admin.DashBoard;

    public class DashBoardViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        private readonly IItemRepository _itemRepository;
        private readonly string _titleSearchText;
        private readonly int _currentPage;

        private readonly int _numOfPage;

        public DashBoardViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression,
                string titleSearchText,
                int currentPage)
            : this(viewNameExpression, titleSearchText, currentPage,
                   DependencyResolver.Current.GetService<IItemRepository>())
        {
        }

        public DashBoardViewModelActionResult(
                    Expression<Func<TController, ActionResult>> viewNameExpression,
                    string titleSearchText,
                    int currentPage,
                    IItemRepository itemRepository) : base(viewNameExpression)
        {
            this._itemRepository = itemRepository;
            this._titleSearchText = titleSearchText;
            this._currentPage = currentPage;
            
            this._numOfPage = this.ConfigurationManager.GetAppConfigBy("NumOfPage").ToInteger();
        }

        #endregion

        #region implementation

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            var dashBoardViewModel = new DashBoardViewModel();

            int numOfRecords;

            var items = this._itemRepository.SeachByTitle(this._titleSearchText, this._currentPage, this._numOfPage, out numOfRecords);

            dashBoardViewModel.Items = items.ToList();

            dashBoardViewModel.PagingData = new PagingViewModel(
                                                    this._currentPage,
                                                    this._numOfPage,
                                                    numOfRecords,
                                                    string.Format(
                                                        "{0}",
                                                        "/Admin/DashBoard/Index/{page}"),
                                                     null);

            this.GetViewResult(dashBoardViewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentNotNull(this._itemRepository, "ItemRepository");
            Guard.ArgumentMustMoreThanZero(this._currentPage, "CurrentPage");
            Guard.ArgumentMustMoreThanZero(this._numOfPage, "NumOfPage");
        }

        #endregion
    }
}