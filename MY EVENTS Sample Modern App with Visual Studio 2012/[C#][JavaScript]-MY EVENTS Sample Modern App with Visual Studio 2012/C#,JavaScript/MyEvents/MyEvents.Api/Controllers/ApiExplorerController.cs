using System.Web.Http;
using System.Web.Mvc;

namespace MyEvents.Api.Controllers
{
    /// <summary>
    /// Controller to show the Web API documentation
    /// </summary>
    /// <see href="http://blogs.msdn.com/b/yaohuang1/archive/2012/05/21/asp-net-web-api-generating-a-web-api-help-page-using-apiexplorer.aspx"/>
    public class ApiExplorerController : Controller
    {
        /// <summary>
        /// Show the Web API documentation
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var apiExplorer = GlobalConfiguration.Configuration.Services.GetApiExplorer();
            return View(apiExplorer);
        }
    }
}
