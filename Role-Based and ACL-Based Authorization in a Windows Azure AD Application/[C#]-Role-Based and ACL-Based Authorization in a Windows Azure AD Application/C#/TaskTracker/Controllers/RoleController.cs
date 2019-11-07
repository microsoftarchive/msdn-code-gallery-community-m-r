using Microsoft.WindowsAzure.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Web.Mvc;

namespace TaskTracker
{
    public class RoleController : Controller
    {
        public DirectoryDataService directoryService;

        public DirectoryDataService DirectoryService
        {
            get
            {
                if (directoryService == null)
                {
                    directoryService = GraphServiceHelper.CreateDirectoryDataService(this.HttpContext.Session);
                }
                return directoryService;
            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult ACLSubmit(FormCollection formCollection)
        {
            //remove ACL assignments marked by checkboxes
            XmlHelper.RemoveACLElemFromXml(formCollection);

            //add new ACL assignment
            if (formCollection != null && formCollection["ACLName"].Length > 0)
            {
                User user = new WebRetryHelper<User>(() => DirectoryService.users.Where(it => (it.userPrincipalName.Equals(formCollection["ACLName"]))).SingleOrDefault()).Value;
                string objectId = (user == null) ? null : user.objectId;
                if (objectId == null)
                {
                    objectId = GetGroupObjectIdFromName(DirectoryService, formCollection["ACLName"]);
                }
                if (objectId == null)
                {
                    return RedirectToAction("Error", "Home", new { errorMessage = "User/Group not found." });
                }
                XmlHelper.AppendACLElemToXml(objectId);
            }
            return RedirectToAction("RoleMappings", "Role");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult RoleMappingSubmit(FormCollection formCollection)
        {
            //remove role mapping assignments marked by checkboxes
            XmlHelper.RemoveRoleMappingsFromXml(formCollection);

            //add new role mapping assignment
            if (formCollection != null && formCollection["name"].Length > 0)
            {
                User user = new WebRetryHelper<User>(() => DirectoryService.users.Where(it => (it.userPrincipalName.Equals(formCollection["name"]))).SingleOrDefault()).Value;
                string objectId = (user == null) ? null : user.objectId;
                if (objectId == null)
                { 
                    objectId = GetGroupObjectIdFromName(DirectoryService, formCollection["name"]);
                }
                if (objectId == null)
                {
                    return RedirectToAction("Error", "Home", new { errorMessage = "User/Group not found." });
                }
                XmlHelper.AppendRoleMappingToXml(formCollection, objectId);
            }
            return RedirectToAction("RoleMappings", "Role");
        }

        [HttpGet]
        public ActionResult Roles()
        {
            ViewBag.Message = "My Roles";
            List<string> myroles = new List<String>();

            foreach (string str in RoleMapElem.Roles)
            {
                if (User.IsInRole(str))
                    myroles.Add(str);
            }
            ViewData["myroles"] = myroles;
            return View();
        }

        // Use Authorize attribute to allow only Admins to change role mappings
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult RoleMappings()
        {
            ViewBag.Message = "RoleMappings";
            List<List<RoleMapElem>> mappings = XmlHelper.GetRoleMappingsFromXml();
            List<ACLElem> ACL = XmlHelper.GetACLElemsFromXml();
            Dictionary<string, string> nameDict = new Dictionary<string,string>();

            for (int i = 0; i < mappings.Count; i++)
            {
                for(int j = 0; j < mappings[i].Count; j++)
                {
                    nameDict[mappings[i][j].ObjectId] = GetDisplayNameFromObjectId(DirectoryService, mappings[i][j].ObjectId);
                }
                ViewData[RoleMapElem.Roles[i] + "List"] = mappings[i];
            }

            for (int i = 0; i < ACL.Count; i++)
            {
                nameDict[ACL[i].ObjectId] = GetDisplayNameFromObjectId(DirectoryService, ACL[i].ObjectId);
            }
            ViewData["ACL"] = ACL;

            ViewData["nameDict"] = nameDict;
            ViewData["roles"] = RoleMapElem.Roles;
            return View();
        }

        private static string GetDisplayNameFromObjectId(DirectoryDataService directoryService, string id)
        {
            try
            {
                Group group = directoryService.groups.Where(it => (it.objectId == id)).SingleOrDefault();
                return group.displayName;
            }
            catch
            {
                try
                {
                    User user = directoryService.users.Where(it => (it.objectId == id)).SingleOrDefault();
                    return user.displayName;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static string GetGroupObjectIdFromName(DirectoryDataService directoryService, string name)
        {
            QueryOperationResponse<Group> response;
            //WebRetryHelper is a retries function with exponential back off in the case of transient exception
            DataServiceQuery<Group> groups = new WebRetryHelper<DataServiceQuery<Group>>(() => directoryService.groups).Value;
            groups = (DataServiceQuery<Group>)(groups.Where(group => String.Compare(group.displayName, name) == 0));
            response = groups.Execute() as QueryOperationResponse<Group>;
            List<Group> groupList = response.ToList();
            if (groupList.Count == 0) return null;
            return groupList[0].objectId;
        }

    }
}
