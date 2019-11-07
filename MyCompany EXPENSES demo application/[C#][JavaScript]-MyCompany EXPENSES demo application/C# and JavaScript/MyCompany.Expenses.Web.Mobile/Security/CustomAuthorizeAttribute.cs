

namespace MyCompany.Expenses.Web.Mobile.Security
{
    using System;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Mvc;

    /// <summary>
    /// Custom Authorize Attribute
    /// </summary>
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Authorize Core
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Request.Url.AbsoluteUri.ToLower().Contains("noauth"))
            {
                httpContext.Session["isNoAuth"] = true;

                if (httpContext.Session["securityToken"] == null)
                {
                    httpContext.Session["securityToken"] = "testToken";
                    httpContext.Session["fullName"] = WebConfigurationManager.AppSettings["testModeIdentityName"].ToString();
                }
            }

            if (httpContext.Session["securityToken"] != null)
                return true;

            string resource = WebConfigurationManager.AppSettings["RemoteService"];
            string replyUrl = WebConfigurationManager.AppSettings["ReplyUrl"];
            string tenant = WebConfigurationManager.AppSettings["ida:Tenant"];
            string clientId = WebConfigurationManager.AppSettings["ClientId"];

            var authUrl = String.Format("https://login.windows.net/{0}/oauth2/authorize?response_type=code&resource={1}&client_id={2}&redirect_uri={3}"
                , tenant, resource, clientId, replyUrl);

            httpContext.Response.Redirect(authUrl);

            return false;
        }
    }
}