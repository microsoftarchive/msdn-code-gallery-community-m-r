
namespace MyCompany.Expenses.Web.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Web;
    using System.Web.Http;
    using System.Linq;
    using System.IdentityModel.Tokens;
    using System.Xml;
    using System.IO;
    using System.Web.Configuration;
    using MyCompany.Expenses.Web.Infraestructure.Security;

    /// <summary>
    /// FederationController
    /// </summary>
    [Authorize]
    [MyCompanyAuthorization]
    public class FederationController : ApiController
    {
        /// <summary>
        /// Federation Callback method to return the authorization token
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Post()
        {
            var response = this.Request.CreateResponse(HttpStatusCode.Redirect);
            response.Headers.Add("Location", "/api/federation/end?token=" + ExtractBootstrapToken());
            return response;
        }

        /// <summary>
        /// ExtractBootstrapToken
        /// </summary>
        /// <returns></returns>
        protected virtual string ExtractBootstrapToken()
        {
            var bootstrapContext = ClaimsPrincipal.Current.Identities.First().BootstrapContext as BootstrapContext;
            JwtSecurityToken jwt = bootstrapContext.SecurityToken as JwtSecurityToken;
            return jwt.RawData;
        }
    }
}