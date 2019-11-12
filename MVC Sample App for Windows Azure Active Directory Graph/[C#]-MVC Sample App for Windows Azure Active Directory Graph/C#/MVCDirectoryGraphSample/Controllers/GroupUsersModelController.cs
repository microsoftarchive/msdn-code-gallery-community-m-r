using Microsoft.WindowsAzure.ActiveDirectory;
using MvcDirectoryGraphSample.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcDirectoryGraphSample.Controllers
{
    /// <summary>
    /// Controller for GroupUsers management page.
    /// </summary>
    public class GroupUsersModelController : Controller
    {
        private DirectoryDataService directoryService;

        public DirectoryDataService DirectoryService
        {
            get
            {
                if (directoryService == null)
                {
                    directoryService = MVCGraphServiceHelper.CreateDirectoryDataService(this.HttpContext.Session);
                }
                return directoryService;
            }
        }

        // GET: /UserRolesModel/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Submit(string id, IEnumerable<string> addUsers, IEnumerable<string> removeUsers)
        {
            // Get the Group from the Graph Service.
            Group refreshedGroup = DirectoryService.groups.Where(it => (it.objectId == id)).SingleOrDefault();
  
            // Add the Users that are submitted for Add and remove the ones in the removeUsers list.
            var users = DirectoryService.users.ToList();
            if (addUsers != null)
            {
                foreach (var userid in addUsers)
                {
                    DirectoryService.AddLink(refreshedGroup, "members", users.Single(it => (it.objectId == userid)));
                }
            }

            if (removeUsers != null)
            {
                foreach (var userid in removeUsers)
                {
                    DirectoryService.DeleteLink(refreshedGroup, "members", users.Single(it => (it.objectId == userid)));
                }
            }
            DirectoryService.SaveChanges();
            return RedirectToAction("Index", "Group");
        }
    }
}
