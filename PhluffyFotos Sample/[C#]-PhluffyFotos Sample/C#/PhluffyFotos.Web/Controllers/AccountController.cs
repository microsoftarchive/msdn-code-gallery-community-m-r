namespace PhluffyFotos.Web.Controllers
{
    using System;
    using System.Globalization;
    using System.Security.Principal;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;
    using System.Web.Security;
    using PhluffyFotos.Data;
    using PhluffyFotos.Data.WindowsAzure;
    using PhluffyFotos.Web.ViewModels;

    public interface IFormsAuthentication
    {
        void SignIn(string userName, bool createPersistentCookie);

        void SignOut();
    }

    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);

        MembershipCreateStatus CreateUser(string userName, string password, string email);
    }

    [HandleError]
    public class AccountController : Controller
    {
        // This constructor is used by the MVC framework to instantiate the controller using
        // the default forms authentication and membership providers.
        public AccountController()
            : this(null, null, null)
        {
        }

        // This constructor is not used by the MVC framework but is instead provided for ease
        // of unit testing this type. See the comments at the end of this file for more
        // information.
        public AccountController(IFormsAuthentication formsAuth, IMembershipService service, IPhotoRepository repository)
        {
            this.FormsAuth = formsAuth ?? new FormsAuthenticationService();
            this.MembershipService = service ?? new AccountMembershipService();
            this.Repository = repository ?? new PhotoRepository();
        }

        public IPhotoRepository Repository
        {
            get;
            private set;
        }

        public IFormsAuthentication FormsAuth
        {
            get;
            private set;
        }

        public IMembershipService MembershipService
        {
            get;
            private set;
        }

        public ActionResult LogOn()
        {
            return View(new AccountLogOnViewModel
            {
                RememberMe = false,
                ReturnUrl = Request.QueryString["ReturnUrl"]
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings",
            Justification = "Needs to take same parameter type as Controller.Redirect()")]
        public ActionResult LogOn(AccountLogOnViewModel account)
        {
            if (!ModelState.IsValid)
            {
                return View(account);
            }

            if (!this.MembershipService.ValidateUser(account.UserName, account.Password))
            {
                ModelState.AddModelError("UserName", "The Username or Password provided is incorrect.");
                return View(account);
            }

            this.FormsAuth.SignIn(account.UserName, account.RememberMe);
            if (!string.IsNullOrEmpty(account.ReturnUrl))
            {
                return Redirect(account.ReturnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Album");
            }
        }

        public ActionResult LogOff()
        {
            this.FormsAuth.SignOut();

            return RedirectToAction("Index", "Album");
        }

        public ActionResult Register()
        {
            ViewData["PasswordLength"] = this.MembershipService.MinPasswordLength;

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(AccountRegisterViewModel account)
        {
            ViewData["PasswordLength"] = this.MembershipService.MinPasswordLength;

            if (!ModelState.IsValid)
            {
                return View(account);
            }

            MembershipCreateStatus createStatus = this.MembershipService.CreateUser(account.UserName, account.Password, account.Email);

            if (createStatus != MembershipCreateStatus.Success)
            {
                ModelState.AddModelError("UserName", ErrorCodeToString(createStatus));
                return View(account);
            }

            this.FormsAuth.SignIn(account.UserName, false /* createPersistentCookie */);

            // create album, provision blob storage
            this.Repository.BootstrapUser(account.UserName, account.Album);

            return RedirectToAction("Index", "Album");
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity is WindowsIdentity)
            {
                throw new InvalidOperationException("Windows authentication is not supported.");
            }
        }

        #region Validation Methods
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://msdn.microsoft.com/en-us/library/system.web.security.membershipcreatestatus.aspx for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        #endregion

        // The FormsAuthentication type is sealed and contains static members, so it is difficult to
        // unit test code that calls its members. The interface and helper class below demonstrate
        // how to create an abstract wrapper around such a type in order to make the AccountController
        // code unit testable.
        internal class FormsAuthenticationService : IFormsAuthentication
        {
            public void SignIn(string userName, bool createPersistentCookie)
            {
                FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
            }

            public void SignOut()
            {
                FormsAuthentication.SignOut();
            }
        }        
    }
}
