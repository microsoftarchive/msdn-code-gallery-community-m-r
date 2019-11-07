using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using MyEvents.Data;
using MyEvents.Model;
using MyEvents.Web.Authentication;
using MyEvents.Web.Mappers;
using MyEvents.Web.Models;
using MyEvents.Web.Services;

namespace MyEvents.Web.Controllers
{
    /// <summary>
    /// Account controller.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        /// <summary>
        /// Account controller constructor.
        /// </summary>
        /// <param name="authenticationService"> </param>
        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Login post action.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string returnUrl)
        {
            string loginUrl = _authenticationService.GetLoginUrl(Request.RequestContext.HttpContext, returnUrl);
            return Redirect(loginUrl);
        }

        /// <summary>
        /// Log Out action.
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut(MyEventsIdentity identity)
        {
            _authenticationService.DestroyAuthenticationTicket(identity);
            return RedirectToAction("Index", "Home");
        }
    }
}
