
namespace MyCompany.Common.Security.Web
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// Custom Jwt Security Token Handler
    /// </summary>
    public class CustomJwtSecurityTokenHandler : JwtSecurityTokenHandler
    {
        /// <summary>
        /// Validate Token
        /// </summary>
        /// <param name="jwt"></param>
        /// <param name="validationParameters"></param>
        /// <returns></returns>
        public override ClaimsPrincipal ValidateToken(JwtSecurityToken jwt, TokenValidationParameters validationParameters)
        {
            if ((validationParameters.ValidIssuer == null) &&
                (validationParameters.ValidIssuers == null || !validationParameters.ValidIssuers.Any()))
            {
                validationParameters.ValidIssuers = new List<string> { ((ConfigurationBasedIssuerNameRegistry)Configuration.IssuerNameRegistry)
                    .ConfiguredTrustedIssuers.First().Value };
            }

            if (validationParameters.SigningToken == null)
            {

                validationParameters.SigningToken = new X509SecurityToken(new X509Certificate2(
                    SecurityHelper.GetSigningCertificate(ConfigurationManager.AppSettings["ida:FederationMetadataLocation"])));

            }
            return base.ValidateToken(jwt, validationParameters);
        }

    } 
}