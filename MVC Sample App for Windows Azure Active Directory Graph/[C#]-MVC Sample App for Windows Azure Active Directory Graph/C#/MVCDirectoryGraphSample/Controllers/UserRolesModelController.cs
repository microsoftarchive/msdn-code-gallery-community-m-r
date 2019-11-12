using Microsoft.WindowsAzure.ActiveDirectory;
using MvcDirectoryGraphSample.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcDirectoryGraphSample.Controllers
{
    public class UserRolesModelController : Controller
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
        public ActionResult Submit(IEnumerable<string> addRoles, IEnumerable<string> removeRoles, string id)
        {
            User refreshedUser = DirectoryService.directoryObjects.OfType<User>().Where(it => (it.objectId == id)).SingleOrDefault();
            DirectoryService.LoadProperty(refreshedUser, "memberOf");
            var roles = DirectoryService.directoryObjects.OfType<Role>().ToList();
            if (addRoles != null)
            {
                foreach (var roleid in addRoles)
                {
                    DirectoryService.AddLink(roles.Single(it => (it.objectId == roleid)), "members", refreshedUser);
                }
            }

            if (removeRoles != null)
            {
                foreach (var roleid in removeRoles)
                {
                    DirectoryService.DeleteLink(roles.Single(it => (it.objectId == roleid)), "members", refreshedUser);
                }
            }
            DirectoryService.SaveChanges();
            return RedirectToAction("Index", "User");
            ////var roles = DirectoryService.Roles;
            ////foreach (var key in collection.Keys)
            ////{
            ////    if ((collection.GetValue((string)key).AttemptedValue == "true"))
            ////    {
            ////        var role = DirectoryService.Roles.Where(it => it.ObjectReference.Equals((string)key)).SingleOrDefault();
            ////        if (refreshedUser.MemberOf.Any(it => it.ObjectReference.Equals(role.ObjectReference)))
            ////        {
            ////            refreshedUser.MemberOf.Remove(refreshedUser.MemberOf.Single(it => it.ObjectReference.Equals(role.ObjectReference)));
            ////        }
            ////        else
            ////        {
            ////            refreshedUser.MemberOf.Add(role);
            ////        }
            ////    }
            ////}
            ////DirectoryService.SaveChanges();
            ////return View("Details", refreshedUser);
            
        }
    }
}
