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

    public class HomePageViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        private readonly ICategoryRepository _categoryRepository;
        private readonly IItemRepository _itemRepository;

        private readonly int _numOfPage;

        public HomePageViewModelActionResult(Expression<Func<TController, ActionResult>> viewNameExpression)
            : this(viewNameExpression,
                   DependencyResolver.Current.GetService<ICategoryRepository>(),
                   DependencyResolver.Current.GetService<IItemRepository>())
        {
        }

        public HomePageViewModelActionResult(
            Expression<Func<TController, ActionResult>> viewNameExpression,
            ICategoryRepository categoryRepository,
            IItemRepository itemRepository)
            : base(viewNameExpression)
        {
            this._categoryRepository = categoryRepository;
            this._itemRepository = itemRepository;

            this._numOfPage = this.ConfigurationManager.GetAppConfigBy("NumOfPage").ToInteger();
        }

        #endregion

        #region implementation

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            var cats = this._categoryRepository.GetCategories();

            var mainViewModel = new HomePageViewModel();
            var headerViewModel = new HeaderViewModel();
            var footerViewModel = new FooterViewModel();
            var mainPageViewModel = new MainPageViewModel();

            headerViewModel.SiteTitle = "Magazine Website";
            if (cats != null && cats.Any())
            {
                headerViewModel.Categories = cats.ToList();
                footerViewModel.Categories = cats.ToList();
            }

            mainPageViewModel.LeftColumn = this.BindingDataForMainPageLeftColumnViewModel();
            mainPageViewModel.RightColumn = this.BindingDataForMainPageRightColumnViewModel();

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
        }

        #endregion

        #region private functions

        private MainPageRightColumnViewModel BindingDataForMainPageRightColumnViewModel()
        {
            var mainPageRightCol = new MainPageRightColumnViewModel();

            mainPageRightCol.LatestNews = this._itemRepository.GetNewestItem(this._numOfPage).ToList();
            mainPageRightCol.MostViews = this._itemRepository.GetMostViews(this._numOfPage).ToList();

            return mainPageRightCol;
        }

        private MainPageLeftColumnViewModel BindingDataForMainPageLeftColumnViewModel()
        {
            var mainPageLeftCol = new MainPageLeftColumnViewModel();

            var items = this._itemRepository.GetNewestItem(this._numOfPage);

            if (items != null && items.Any())
            {
                var firstItem = items.First();

                if (firstItem == null)
                    throw new NoNullAllowedException("First Item".ToNotNullErrorMessage());

                if (firstItem.ItemContent == null)
                    throw new NoNullAllowedException("First ItemContent".ToNotNullErrorMessage());

                mainPageLeftCol.FirstItem = firstItem;

                if (items.Count() > 1)
                {
                    mainPageLeftCol.RemainItems = items.Where(x => x.ItemContent != null && x.Id != mainPageLeftCol.FirstItem.Id).ToList();
                }
            }

            return mainPageLeftCol;
        }

        #endregion
    }
}