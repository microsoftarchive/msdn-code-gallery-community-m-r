using System.Web.Mvc;
using MyEvents.Web.Services;

namespace MyEvents.Web.Binders
{
    /// <summary>
    /// User info binder.
    /// </summary>
    public class MyEventsIdentityModelBinder : IModelBinder
    {
        private readonly IAuthenticationService _authenticationService;

        /// <summary>
        /// MyEventsIdentityModelBinder contructor.
        /// </summary>
        public MyEventsIdentityModelBinder(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Binds the model to a value by using the specified controller context and binding context.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (!controllerContext.HttpContext.Request.IsAuthenticated)
                return null;
            var identity = _authenticationService.GetIdentityFromTicket(controllerContext.HttpContext);
            return identity;
        }
    }
}