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

    public class CategoryViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
        #region variables & ctors

        private readonly ICategoryRepository _categoryRepository;
        private readonly IItemRepository _itemRepository;
        private readonly int _categoryId;

        private readonly int _numOfPage;

        public CategoryViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression,
                int categoryId)
            : this(viewNameExpression, categoryId,
                   DependencyResolver.Current.GetService<ICategoryRepository>(),
                   DependencyResolver.Current.GetService<IItemRepository>())
        {
        }

        public CategoryViewModelActionResult(
                    Expression<Func<TController, ActionResult>> viewNameExpression,
                    int categoryId,
                    ICategoryRepository categoryRepository, 
                    IItemRepository itemRepository) : base(viewNameExpression)
        {
            this._categoryRepository = categoryRepository;
            this._itemRepository = itemRepository;
            this._categoryId = categoryId;

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

            if (cats != null && cats.Any())
            {
                headerViewModel.Categories = cats.ToList();
                footerViewModel.Categories = cats.ToList();
            }

            mainPageViewModel.LeftColumn = this.BindingDataForCategoryLeftColumnViewModel(this._categoryId);
            mainPageViewModel.RightColumn = this.BindingDataForMainPageRightColumnViewModel();

            var items = ((CategoryLeftColumnViewModel)mainPageViewModel.LeftColumn).Items;
            if (items != null && items.Count > 0)
                headerViewModel.SiteTitle = string.Format("Magazine Website - {0} category", items.FirstOrDefault().Category.Name);
            else
                headerViewModel.SiteTitle = "Magazine Website";

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
            Guard.ArgumentMustMoreThanZero(this._categoryId, "CategoryId");
        }

        #endregion

        #region private functions

        private CategoryLeftColumnViewModel BindingDataForCategoryLeftColumnViewModel(int categoryId)
        {
            var viewModel = new CategoryLeftColumnViewModel();

            var items = this._itemRepository.GetByCategory(categoryId);

            if (items == null)
                throw new NoNullAllowedException("Items".ToNotNullErrorMessage());

            if (items.Any())
            {
                viewModel.Items = items.ToList();
            }

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