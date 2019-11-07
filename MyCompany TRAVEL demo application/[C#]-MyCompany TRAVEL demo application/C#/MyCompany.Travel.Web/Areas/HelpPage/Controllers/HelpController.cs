using System;
using System.Web.Http;
using System.Web.Mvc;
using MyCompany.Travel.Web.Areas.HelpPage.Models;

namespace MyCompany.Travel.Web.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : Controller
    {
        /// <summary>
        /// HelpController
        /// </summary>
        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        /// <summary>
        /// HelpController
        /// </summary>
        /// <param name="config"></param>
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
        /// <returns></returns>
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