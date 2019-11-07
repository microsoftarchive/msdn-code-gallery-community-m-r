using System;
using System.Web.Http;
using System.Web.Mvc;
using MyCompany.Expenses.Web.Areas.HelpPage.Models;

namespace MyCompany.Expenses.Web.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : Controller
    {
        /// <summary>
        /// Help controller constuctor
        /// </summary>
        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        /// <summary>
        /// Help controller constuctor
        /// </summary>
        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        /// <summary>
        /// Configuration
        /// </summary>        
        public HttpConfiguration Configuration { get; private set; }

        /// <summary>
        /// Index
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        /// <summary>
        /// Api
        /// </summary>
        /// <param name="apiId"></param>
        /// <returns></returns>
        public ActionResult Api(string apiId)
        {
            if (!String.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            return View("Error");
        }
    }
}