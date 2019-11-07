namespace CIK.News.Web.Infras.ActionResults
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using CIK.News.Framework;
    using CIK.News.Framework.Configurations;

    public abstract class MyActionResult : ActionResult, IEnsureNotNull
    {
        public abstract void EnsureAllInjectInstanceNotNull();
    }

    public abstract class ActionResultBase<TController> : MyActionResult where TController : Controller
    {
        protected readonly Expression<Func<TController, ActionResult>> ViewNameExpression;
        protected readonly IExConfigurationManager ConfigurationManager;

        protected ActionResultBase (Expression<Func<TController, ActionResult>> expr)
            : this(DependencyResolver.Current.GetService<IExConfigurationManager>(), expr)
        {
        }

        protected ActionResultBase(
            IExConfigurationManager configurationManager,
            Expression<Func<TController, ActionResult>> expr)
        {
            Guard.ArgumentNotNull(expr, "ViewNameExpression");
            Guard.ArgumentNotNull(configurationManager, "ConfigurationManager");

            this.ViewNameExpression = expr;
            this.ConfigurationManager = configurationManager;
        }

        protected ViewResult GetViewResult<TViewModel>(TViewModel viewModel)
        {
            var m = (MethodCallExpression)this.ViewNameExpression.Body;
            if (m.Method.ReturnType != typeof(ActionResult))
            {
                throw new ArgumentException("ControllerAction method '" + m.Method.Name + "' does not return ActionResult type");
            }

            var result = new ViewResult
            {
                ViewName = m.Method.Name
            };

            result.ViewData.Model = viewModel;

            return result;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            this.EnsureAllInjectInstanceNotNull();
        }
    }
}