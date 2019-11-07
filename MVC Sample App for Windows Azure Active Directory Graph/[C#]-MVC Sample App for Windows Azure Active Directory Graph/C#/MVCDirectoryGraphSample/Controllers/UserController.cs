using Microsoft.WindowsAzure.ActiveDirectory;
using MvcDirectoryGraphSample.Helpers;
using MvcDirectoryGraphSample.Models;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;


namespace MvcDirectoryGraphSample.Controllers
{
    /// <summary>
    /// Controller for the Users Administration page.
    /// </summary>    
    [HandleError]
    public class UserController : Controller
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

        // 
        // GET: /User/
        // Get: /User?$skiptoken=xxx
        // Get: /User?$filter=DisplayName eq 'xxxx'
        public ActionResult Index(string displayName, string nextLinkUri)
        {
            QueryOperationResponse<User> response;
            var users = DirectoryService.users;
            
            // If a filter query for displayName  is submitted, we throw away previous results we were paging.
            if (displayName != null)
            {
                ViewBag.CurrentFilter = displayName;
                
                // Linq query for filter for DisplayName property.
                users = (DataServiceQuery<User>)(users.Where(user => user.displayName.StartsWith(displayName)));
                response = users.Execute() as QueryOperationResponse<User>;                
            }
            else
            {
                // Handle the case for first request vs paged request.
                if (nextLinkUri == null)
                {
                    response = users.Execute() as QueryOperationResponse<User>;
                }
                else
                {
                    response = DirectoryService.Execute<User>(new Uri(nextLinkUri)) as QueryOperationResponse<User>;
                }
            }

            List<User> userList = response.ToList();
            
            // Handle the SkipToken if present in the response.
            if (response.GetContinuation() != null)
            {
                ViewBag.ContinuationToken = response.GetContinuation().NextLinkUri;
            }
            return View(userList);
        }

        // GET: /User/Details/5
        public ActionResult Details(string id)
        {
            User user = DirectoryService.users.Where(it => (it.objectId == id)).SingleOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: /User/Create
        public ActionResult Create()
        {
            // Verified Domains are used to populate the drop down for selecting
            // the domain name for UserPrincipalName field.
            var verifiedDomains = DirectoryService.tenantDetails.Single().verifiedDomains.ToList();
            var verifiedDomainValues = verifiedDomains.Select(it => it.name);
            ViewData["selectedDomain"] = new SelectList(verifiedDomainValues);
            return View();
        }

        // POST: /User/Create
        [HttpPost]
        public ActionResult Create(User user, string emailAlias, string selectedDomain)
        {
            ValidateUserForCreate(user, emailAlias);

            if (ModelState.IsValid)
            {
                user.userPrincipalName = string.Format(CultureInfo.InvariantCulture,"{0}@{1}", emailAlias, selectedDomain);
                user.mailNickname = emailAlias;            
                DirectoryService.AddTousers(user);
                DirectoryService.SaveChanges();                                
                return RedirectToAction("Index");
            }
            else
            {
                // If the request was not valid, re-populate the verified domains list.
                var verifiedDomains = DirectoryService.tenantDetails.Single().verifiedDomains.ToList();
                var verifiedDomainValues = verifiedDomains.Select(it => it.name);
                ViewData["selectedDomain"] = new SelectList(verifiedDomainValues);
            }
            return View(user);
        }


        // GET: /User/Edit/5
        public ActionResult Edit(String id)
        {
            User user = DirectoryService.users.Where(it => (it.objectId == id)).SingleOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            var verifiedDomains = DirectoryService.tenantDetails.Single().verifiedDomains.ToList();
            var verifiedDomainValues = verifiedDomains.Select(it => it.name);
            ViewData["selectedDomain"] = new SelectList(verifiedDomainValues);
            return View(user);
        }

        // POST: /User/Edit/5
        [HttpPost]
        public ActionResult Edit(User user, string emailAlias, string selectedDomain)
        {
            ValidateUserForEdit(user, emailAlias);

            if (ModelState.IsValid)
            {
                // Fetch the user object from the service and overwrite the properties from the updated object
                // we got from the view.
                User refreshedUser = DirectoryService.users.Where(it => (it.objectId == user.objectId)).SingleOrDefault();
 
                // Save the changes to User object and then write the File stream
                // for Thumbnail photo in case it has been updated by the user.
                refreshedUser.userPrincipalName = string.Format(CultureInfo.InvariantCulture, "{0}@{1}", emailAlias, selectedDomain);
                refreshedUser.mailNickname = emailAlias;
                CopyPropertyValuesFromViewObject(user, refreshedUser);
                DirectoryService.UpdateObject(refreshedUser);
                DirectoryService.SaveChanges(SaveChangesOptions.PatchOnUpdate);
                if (!String.IsNullOrEmpty(Request.Files[0].FileName))
                {
                    // Write the photo file to the Graph service.                    
                    Debug.Assert(Request.Files.Keys[0] == "photofile");
                    DirectoryService.SetSaveStream(refreshedUser, "thumbnailPhoto", Request.Files["photofile"].InputStream, true, "image/jpg");
                    DirectoryService.SaveChanges(SaveChangesOptions.PatchOnUpdate);
                }
                return RedirectToAction("Index");
            }
            else
            {
                var verifiedDomains = DirectoryService.tenantDetails.Single().verifiedDomains.ToList();
                var verifiedDomainValues = verifiedDomains.Select(it => it.name);
                ViewData["selectedDomain"] = new SelectList(verifiedDomainValues);
            }
            return View(user);
        }

        /// <summary>
        /// Copies the property values from the object passed in by the view to the object that was re-fetched from Graph Service. 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="refreshedUser"></param>
        private static void CopyPropertyValuesFromViewObject(User user, User refreshedUser)
        {
            refreshedUser.displayName = user.displayName;
            refreshedUser.accountEnabled = user.accountEnabled;
            if (user.passwordProfile.password != null)
            {
                refreshedUser.passwordProfile = new PasswordProfile();
                refreshedUser.passwordProfile.password = user.passwordProfile.password;
                refreshedUser.passwordProfile.forceChangePasswordNextLogin = user.passwordProfile.forceChangePasswordNextLogin;
            }
            refreshedUser.city = user.city;
            refreshedUser.country = user.country;
            refreshedUser.department = user.department;
            refreshedUser.facsimileTelephoneNumber = user.facsimileTelephoneNumber;
            refreshedUser.givenName = user.givenName;
            refreshedUser.immutableId = user.immutableId;
            refreshedUser.jobTitle = user.jobTitle;
            refreshedUser.lastDirSyncTime = user.lastDirSyncTime;
            refreshedUser.mail = user.mail;
            refreshedUser.mobile = user.mobile;
            refreshedUser.passwordPolicies = user.passwordPolicies;
            refreshedUser.physicalDeliveryOfficeName = user.physicalDeliveryOfficeName;
            refreshedUser.postalCode = user.postalCode;
            refreshedUser.preferredLanguage = user.preferredLanguage;
            refreshedUser.state = user.state;
            refreshedUser.streetAddress = user.streetAddress;
            refreshedUser.surname = user.surname;
            refreshedUser.telephoneNumber = user.telephoneNumber;
            refreshedUser.usageLocation = user.usageLocation;
            refreshedUser.userType = user.userType;
        }

        /// <summary>
        /// Validate User object for Create requests.
        /// </summary>
        private void ValidateUserForCreate(User user, string emailAlias)
        {
            ValidateUserForEdit(user, emailAlias);
            ModelValidationHelper.ValidateStringProperty(ModelState, user.passwordProfile.password, "Password", "Password is required.");
        }

        /// <summary>
        /// Validate User object for Edit requests.
        /// </summary>
        private void ValidateUserForEdit(User user, string emailAlias)
        {
            ModelValidationHelper.ValidateStringProperty(ModelState, user.displayName, "DisplayName", "DisplayName is required.");
            ModelValidationHelper.ValidateStringProperty(ModelState, emailAlias, "UserPrincipalName", "UserPrincipalName is required.");
            ModelValidationHelper.ValidateProperty(ModelState, user.accountEnabled, "AccountEnabled", "AccountEnabled is required.");
        }        

        /// <summary>
        /// Show the thumbnail photo of the user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ShowThumbnail(String id)
        {
            User user = DirectoryService.users.Where(it => (it.objectId == Convert.ToString(id))).SingleOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            if (user.thumbnailPhoto.ContentType != null)
            {
                var dsStream = DirectoryService.GetReadStream(user, "thumbnailPhoto", new DataServiceRequestArgs());
                return File(dsStream.Stream, "image/jpg");
            }
            return View();
        }

        // GET: /User/Delete/5
        public ActionResult Delete(String id)
        {
            User user = DirectoryService.users.Where(it => (it.objectId == id)).SingleOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(String id)
        {
            User user = DirectoryService.users.Where(it => (it.objectId == id)).SingleOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            DirectoryService.DeleteObject(user);
            DirectoryService.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /User/ManageRole/5
        public ActionResult ManageRole(String id)
        {
            UserRolesModel userRoles = new UserRolesModel();
            // Get the User object.
            User user = DirectoryService.users.Where(it => (it.objectId == id)).SingleOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            userRoles.User = user;
            // Get all Roles and divide them into the ones who belong to this group
            // and those who don't. We will show these differently in the View.
            List<Role> unassingedRoles = new List<Role>();
            DirectoryService.LoadProperty(user, "memberOf");
            List<Role> currentRoles = user.memberOf.OfType<Role>().ToList();

            var roles = DirectoryService.roles;
            foreach (Role role in roles)
            {
                if (!currentRoles.Exists(it => it.objectId.Equals(role.objectId)))
                {
                    unassingedRoles.Add(role);
                }
            }
            userRoles.UnassignedRoles = unassingedRoles;
            userRoles.CurrentRoles = currentRoles;
            return View(userRoles);
        }
    }
}

