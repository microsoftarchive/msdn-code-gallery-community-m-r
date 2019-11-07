using System.Web.Mvc;
using System.Linq;
using MyEvents.Web.Authentication;
using Microsoft.Practices.Unity;
using MyEvents.Web.Binders;

namespace MyEvents.Web
{
    /// <summary>
    /// Class to configure the Asp.net mvc model binders.
    /// </summary>
    public class ModelBinderConfig
    {
        /// <summary>
        /// Registers the model binders.
        /// </summary>
        /// <param name="unityContainer"></param>
        public static void RegisterModelBinders(IUnityContainer unityContainer)
        {
            ModelBinders.Binders.Add(typeof(MyEventsIdentity), unityContainer.Resolve<MyEventsIdentityModelBinder>() as IModelBinder);
        }
    }
}