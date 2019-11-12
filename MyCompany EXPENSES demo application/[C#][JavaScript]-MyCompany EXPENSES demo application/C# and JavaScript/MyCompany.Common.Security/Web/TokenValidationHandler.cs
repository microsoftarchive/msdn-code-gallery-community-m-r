namespace MyCompany.Common.Security.Web
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Configuration;
    using System.Diagnostics;

    /// <summary>
    /// Token Validation Handler
    /// </summary>
    public class TokenValidationHandler : DelegatingHandler
    {
        /// <summary>
        /// SendAsync
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            string token = string.Empty;

            // NoAuth routes are for demos without internet access
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated || SecurityHelper.RequestIsNoAuthRoute())
            {
                return base.SendAsync(request, cancellationToken);
            }

            IEnumerable<string> authzHeaders = new List<string>();
            if (request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                SecurityHelper.TryRetrieveToken(authzHeaders.ElementAt(0), out token);
            }
            else
            {
                statusCode = HttpStatusCode.Unauthorized;
                return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(statusCode));
            }
            try
            {
                Thread.CurrentPrincipal = SecurityHelper.GetIdentityFromToken(token);
                HttpContext.Current.User = Thread.CurrentPrincipal;

                return base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            catch (Exception)
            {
                statusCode = HttpStatusCode.InternalServerError;
            }
            return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(statusCode));
        }
    }
}