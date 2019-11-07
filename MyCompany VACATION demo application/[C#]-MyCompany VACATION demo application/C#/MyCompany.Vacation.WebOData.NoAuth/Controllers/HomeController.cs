
namespace MyCompany.Vacation.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Home Controller
    /// </summary>
    //[SharePointContextFilter]
    public class HomeController : Controller
    {
        /// <summary>
        /// Default Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //Microsoft.SharePoint.Client.User spUser = null;

            //var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            //using (var clientContext = spContext.CreateUserClientContextForSPHost())
            //{
            //    if (clientContext != null)
            //    {
            //        spUser = clientContext.Web.CurrentUser;

            //        clientContext.Load(spUser, user => user.Title);

            //        clientContext.ExecuteQuery();

            //        ViewBag.UserName = spUser.Title;
            //    }
            //}

            return View();
        }

        /// <summary>
        /// Angular Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Angular()
        {
            return View();
        }

    }
}
