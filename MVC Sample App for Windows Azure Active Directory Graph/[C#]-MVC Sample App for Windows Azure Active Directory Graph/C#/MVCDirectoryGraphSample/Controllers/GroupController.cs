using Microsoft.WindowsAzure.ActiveDirectory;
using MvcDirectoryGraphSample.Helpers;
using MvcDirectoryGraphSample.Models;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Web.Mvc;

namespace MvcDirectoryGraphSample.Controllers
{
    /// <summary>
    /// Controller for the Groups Administration page.
    /// </summary>
    public class GroupController : Controller
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


        // GET: /Group/
        // Get: /Group?$skiptoken=xxx
        // Get: /Group?$filter=DisplayName eq 'xxxx'
        public ActionResult Index(string displayName, string skipToken)
        {
            QueryOperationResponse<Group> response;
            var groups = DirectoryService.groups;

            // If a filter query for displayName  is submitted, we throw away previous results we were paging.
            if (displayName != null)
            {                
                ViewBag.CurrentFilter = displayName;
 
                // Linq query for filter for DisplayName property.
                groups = (DataServiceQuery<Group>)(groups.Where(group => group.displayName.StartsWith(displayName)));
                response = groups.Execute() as QueryOperationResponse<Group>;
            }
            else
            {
                // Handle the case for first request vs paged request.
                if (skipToken == null)
                {
                    response = groups.Execute() as QueryOperationResponse<Group>;
                }
                else
                {
                    response = DirectoryService.Execute<Group>(new Uri(skipToken)) as QueryOperationResponse<Group>;
                }
            }
            List<Group> groupList = response.ToList();
 
            // Handle the SkipToken if present in the response.
            if (response.GetContinuation() != null)
            {
                ViewBag.ContinuationToken = response.GetContinuation().NextLinkUri;
            }
            return View(groupList);
        }

        // GET: /Group/Details/5
        public ActionResult Details(string id)
        {
            Group group = DirectoryService.groups.Where(it => (it.objectId == id)).SingleOrDefault();
            return View(group);
        }

        // GET: /Group/ManageUsers/5
        public ActionResult ManageUsers(String id)
        {
            GroupUsersModel groupUsers = new GroupUsersModel();

            // Get the Group object.
            Group group = DirectoryService.groups.Where(it => (it.objectId == id)).SingleOrDefault();
            groupUsers.Group = group;

            // Get all Users and divide them into the ones who belong to this group
            // and those who don't. We will show these differently in the View.

            DirectoryService.LoadProperty(group, "members");
            List<User> currentGroupUsers = group.members.OfType<User>().ToList<User>();

            List<User> otherUsers = new List<User>();
            var users = DirectoryService.users;
            foreach (User user in users)
            {
                if (!currentGroupUsers.Exists(it => it.objectId.Equals(user.objectId)))
                {
                    otherUsers.Add(user);
                }
            }
            groupUsers.CurrentGroupUsers = currentGroupUsers;
            groupUsers.OtherUsers = otherUsers;
            return View(groupUsers);
        }

        // GET: /User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /User/Create
        [HttpPost]
        public ActionResult Create(Group group)
        {
            ValidateGroup(group);
            if (ModelState.IsValid)
            {
                group.mailEnabled = false;
                DirectoryService.AddTogroups(group);
                DirectoryService.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: /User/Edit/5
        public ActionResult Edit(String id)
        {
            Group group = DirectoryService.groups.Where(it => (it.objectId == id)).SingleOrDefault();
            return View(group);
        }

        // POST: /User/Edit/5
        [HttpPost]
        public ActionResult Edit(Group group)
        {
            ValidateGroup(group);
            if (ModelState.IsValid)
            {
                // Fetch the group object from the service and overwrite the properties from the updated object
                // we got from the view.
                Group refreshedGroup = DirectoryService.groups.Where(it => (it.objectId == group.objectId)).SingleOrDefault();
                refreshedGroup.displayName = group.displayName;
                refreshedGroup.description = group.description;
                DirectoryService.UpdateObject(refreshedGroup);
                DirectoryService.SaveChanges(SaveChangesOptions.PatchOnUpdate);
                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: /Group/Delete/5
        public ActionResult Delete(String id)
        {
            Group group = DirectoryService.groups.Where(it => (it.objectId == id)).SingleOrDefault();
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: /Group/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(String id)
        {
            Group group = DirectoryService.groups.Where(it => (it.objectId == id)).SingleOrDefault();
            DirectoryService.DeleteObject(group);
            DirectoryService.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Validate the Create and Edit requests for presence of values for Required fields.
        /// </summary>
        /// <param name="group"></param>
        private void ValidateGroup(Group group)
        {
            ModelValidationHelper.ValidateStringProperty(ModelState, group.displayName, "DisplayName", "DisplayName is required.");
            ModelValidationHelper.ValidateStringProperty(ModelState, group.mailNickname, "MailNickname", "MailNickname is required.");
            ModelValidationHelper.ValidateProperty(ModelState, group.securityEnabled, "SecurityEnabled", "SecurityEnabled is required.");
        }

    }
}

