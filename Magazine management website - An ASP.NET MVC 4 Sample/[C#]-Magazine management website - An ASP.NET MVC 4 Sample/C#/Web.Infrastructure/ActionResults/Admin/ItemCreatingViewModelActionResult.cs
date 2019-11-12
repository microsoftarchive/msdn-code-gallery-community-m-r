namespace CIK.News.Web.Infras.ActionResults.Admin
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using CIK.News.Entities.NewsAgg;
    using CIK.News.Framework;
    using CIK.News.Web.Infras.ViewModels.Admin.DashBoard;

    public class ItemCreatingViewModelActionResult<TController> : ActionResultBase<TController> where TController : Controller
    {
         #region variables & ctors

        private readonly ICategoryRepository _categoryRepository;

        public ItemCreatingViewModelActionResult(
                Expression<Func<TController, ActionResult>> viewNameExpression)
            : this(viewNameExpression,
                   DependencyResolver.Current.GetService<ICategoryRepository>())
        {
        }

        public ItemCreatingViewModelActionResult(
                    Expression<Func<TController, ActionResult>> viewNameExpression,
                    ICategoryRepository categoryRepository) : base(viewNameExpression)
        {
            this._categoryRepository = categoryRepository;
        }

        #endregion

        #region implementation

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);

            var viewModel = new ItemCreatingViewModel();

            viewModel.Categories = this._categoryRepository.GetCategories().ToList();

            this.GetViewResult(viewModel).ExecuteResult(context);
        }

        public override void EnsureAllInjectInstanceNotNull()
        {
            Guard.ArgumentNotNull(this._categoryRepository, "CategoryRepository");
        }

        #endregion
    }
}