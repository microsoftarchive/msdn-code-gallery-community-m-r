namespace CIK.News.Web.Infras.ActionResults.Client
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using CIK.News.Entities.NewsAgg;
    using CIK.News.Framework;
    using CIK.News.Framework.Extensions;
    using CIK.News.Web.Infras.ViewModels.Client;

    public class DetailsViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        private readonly ICategoryRepository _categoryRepository;
        private readonly IItemRepository _itemRepository;
        private readonly int _itemId;

        private readonly int _numOfPage;

        public DetailsViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression,
                int itemId)
            : this(viewNameExpression, itemId,
                   DependencyResolver.Current.GetService<ICategoryRepository>(),
                   DependencyResolver.Current.GetService<IItemRepository>())
        {
        }

        public DetailsViewModelActionResult(
                    Expression<Func<TController, ActionResult>> viewNameExpression,
                    int itemId,
                    ICategoryRepository categoryRepository, 
                    IItemRepository itemRepository) : base(viewNameExpression)
        {
            this._categoryRepository = categoryRepository;
            this._itemRepository = itemRepository;
            this._itemId = itemId;

            this._numOfPage = this.ConfigurationManager.GetAppConfigBy("NumOfPage").ToInteger();
        }

        #endregion

        #region implementation

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            this._itemRepository.IncreaseNumOfView(this._itemId);

            var cats = this._categoryRepository.GetCategories();

            var mainViewModel = new HomePageViewModel();
            var headerViewModel = new HeaderViewModel();
            var footerViewModel = new FooterViewModel();
            var mainPageViewModel = new MainPageViewModel();

            if (cats != null && cats.Any())
            {
                headerViewModel.Categories = cats.ToList();
                footerViewModel.Categories = cats.ToList();
            }

            mainPageViewModel.LeftColumn = this.BindingDataForDetailsLeftColumnViewModel(this._itemId);
            mainPageViewModel.RightColumn = this.BindingDataForMainPageRightColumnViewModel();

            headerViewModel.SiteTitle = string.Format("Magazine Website - {0}",
                ((DetailsLeftColumnViewModel)mainPageViewModel.LeftColumn).CurrentItem.ItemContent.Title);

            mainViewModel.Header = headerViewModel;
            mainViewModel.DashBoard = new DashboardViewModel();
            mainViewModel.Footer = footerViewModel;
            mainViewModel.MainPage = mainPageViewModel;

            this.GetViewResult(mainViewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentNotNull(this._categoryRepository, "CategoryRepository");
            Guard.ArgumentNotNull(this._itemRepository, "ItemRepository");
            Guard.ArgumentMustMoreThanZero(this._numOfPage, "NumOfPage");
            Guard.ArgumentMustMoreThanZero(this._itemId, "ItemId");
        }

        #endregion

        #region private functions

        private DetailsLeftColumnViewModel BindingDataForDetailsLeftColumnViewModel(int itemId)
        {
            var viewModel = new DetailsLeftColumnViewModel();

            var item = this._itemRepository.GetById(itemId);

            if (item == null)
                throw new NoNullAllowedException(string.Format("Item id={0}", itemId).ToNotNullErrorMessage());

            viewModel.CurrentItem = item;

            return viewModel;
        }

        private MainPageRightColumnViewModel BindingDataForMainPageRightColumnViewModel()
        {
            var mainPageRightCol = new MainPageRightColumnViewModel();

            mainPageRightCol.LatestNews = this._itemRepository.GetNewestItem(this._numOfPage).ToList();
            mainPageRightCol.MostViews = this._itemRepository.GetMostViews(this._numOfPage).ToList();

            return mainPageRightCol;
        }

        #endregion
    }
}