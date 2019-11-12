namespace CIK.News.Web.Infras.ActionResults.Admin
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using CIK.News.Entities.NewsAgg;
    using CIK.News.Framework;
    using CIK.News.Framework.Extensions;
    using CIK.News.Web.Infras.ViewModels.Admin.DashBoard;

    public class ItemEditingViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
         #region variables & ctors

        private readonly ICategoryRepository _categoryRepository;
        private readonly IItemRepository _itemRepository;

        private readonly int _itemId;

        public ItemEditingViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression,
                int itemId)
            : this(viewNameExpression, itemId,
                   DependencyResolver.Current.GetService<ICategoryRepository>(),
                   DependencyResolver.Current.GetService<IItemRepository>())
        {
        }

        public ItemEditingViewModelActionResult(
            Expression<Func<TController, ActionResult>> viewNameExpression,
            int itemId,
            ICategoryRepository categoryRepository,
            IItemRepository itemRepository)
            : base(viewNameExpression)
        {
            this._categoryRepository = categoryRepository;
            this._itemRepository = itemRepository;
            this._itemId = itemId;
        }

        #endregion

        #region implementation

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            var item = this._itemRepository.GetById(this._itemId);

            if (item == null)
                throw new NoNullAllowedException(string.Format("Item with id={0}", this._itemId).ToNotNullErrorMessage());

            var viewModel = item.MapTo<ItemEditingViewModel>();

            viewModel.Categories = this._categoryRepository.GetCategories().ToList();

            this.GetViewResult(viewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentNotNull(this._categoryRepository, "CategoryRepository");
            Guard.ArgumentNotNull(this._itemRepository, "ItemRepository");
            Guard.ArgumentMustMoreThanZero(this._itemId, "ItemId");
        }

        #endregion
    }
}