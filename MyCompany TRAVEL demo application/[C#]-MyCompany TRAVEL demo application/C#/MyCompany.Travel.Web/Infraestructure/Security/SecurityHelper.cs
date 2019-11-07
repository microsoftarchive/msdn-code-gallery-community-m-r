namespace MyCompany.Travel.Web
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IdentityModel.Metadata;
    using System.IdentityModel.Selectors;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography.X509Certificates;
    using System.Security.Principal;
    using System.ServiceModel.Security;
    using System.Threading;
    using System.Web;
    using System.Xml;

    /// <summary>
    /// Security Helper
    /// </summary>
    public class SecurityHelper : ISecurityHelper
    {
        static string tenant = ConfigurationManager.AppSettings["ida:Tenant"];
        static string audience = ConfigurationManager.AppSettings["ida:Audience"];
        static string stsIssuer = null;
        static string stsMetadataAddress = null;
        static DateTime stsMetadataRetrievalTime = DateTime.MinValue;

        /// <summary>
        /// <see cref="MyCompany.Travel.Web.ISecurityHelper"/>
        /// </summary>
        /// <returns><see cref="MyCompany.Travel.Web.ISecurityHelper"/></returns>
        public string GetUser(bool isNoAuth = false)
        {
            // Test Mode is only for demos without internet, not for real apps!
            if (RequestIsNoAuthRoute() || isNoAuth)
            {
                return System.Configuration.ConfigurationManager.AppSettings["testModeIdentity"];
            }

            var principal = (ClaimsPrincipal)Thread.CurrentPrincipal;
            if (!principal.Identity.IsAuthenticated)
                return string.Empty;

            var nameClaim = principal.Claims
                        .FirstOrDefault(c => c.Type == ClaimTypes.Name || c.Type == ClaimTypes.Upn);

            if (nameClaim != null)
                return nameClaim.Value;


            return string.Empty;
        }


        /// <summary>
        /// Web Apps have route called "NoAuth" that is not securized. This route is used for demos without internet access
        /// In the request come from this source, the delegation handler doesn´t validate the header token
        /// </summary>
        /// <returns></returns>
        public static bool RequestIsNoAuthRoute()
        {
            if (HttpContext.Current != null && (RequestIsNoAuth() || UrlReferrerIsNoAuth()))
                return true;

            return false;
        }

        /// <summary>
        /// GetIdentityFromToken
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static IPrincipal GetIdentityFromToken(string token)
        {
            string federationMetadataLocation = ConfigurationManager.AppSettings["ida:FederationMetadataLocation"];
            string issuer;

            // Use JWTSecurityTokenHandler to validate the JWT token
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.Configuration = new SecurityTokenHandlerConfiguration()
            {
                SaveBootstrapContext = true,
                CertificateValidationMode = X509CertificateValidationMode.None,
                CertificateValidator = new CustomX509CertificateValidator()
            };

            // Get tenant information, used to validate incoming security tokens
            GetTenantInformation(federationMetadataLocation, out issuer);

            // Set the expected properties of the JWT token in the TokenValidationParameters
            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                AllowedAudience = audience,
                ValidIssuer = issuer,
                SigningToken = new X509SecurityToken(new X509Certificate2(SecurityHelper.GetSigningCertificate(ConfigurationManager.AppSettings["ida:FederationMetadataLocation"])))
            };

            return tokenHandler.ValidateToken(token, validationParameters);
        }

        /// <summary>
        /// This function retrieves ACS token (in format of OAuth 2.0 Bearer Token type) from 
        /// the Authorization header in the incoming HTTP request from the ShipperClient.
        /// </summary>
        /// <param name="authzHeader"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool TryRetrieveToken(string authzHeader, out string token)
        {
            token = authzHeader;

            if (string.IsNullOrEmpty(authzHeader))
            {
                return false;
            }

            // Remove the bearer token scheme prefix and return the rest as ACS token 
            token = token.StartsWith("Bearer ") ? token.Substring(7) : token;
            token = token.StartsWith("Authorization Bearer ") ? token.Substring(21) : token;
            return true;
        }

        /// <summary>
        /// Parses the Federation Metadata document to get the signing token.
        /// </summary>
        /// <param name="metadataAddress">Federation Metadata Endpoint</param>
        /// <returns>RawData of the Signing Certificate</returns>
        public static byte[] GetSigningCertificate(string metadataAddress)
        {
            if (metadataAddress == null)
            {
                throw new ArgumentNullException(metadataAddress);
            }

            using (XmlReader metadataReader = XmlReader.Create(metadataAddress))
            {
                MetadataSerializer serializer = new MetadataSerializer()
                {
                    CertificateValidationMode = X509CertificateValidationMode.None
                };

                EntityDescriptor metadata = serializer.ReadMetadata(metadataReader) as EntityDescriptor;

                if (metadata != null)
                {
                    SecurityTokenServiceDescriptor stsd = metadata.RoleDescriptors.OfType<SecurityTokenServiceDescriptor>().First();

                    if (stsd != null)
                    {
                        X509RawDataKeyIdentifierClause clause = stsd.Keys.First().KeyInfo.OfType<X509RawDataKeyIdentifierClause>().First();

                        if (clause != null)
                        {
                            return clause.GetX509RawData();
                        }
                        throw new Exception("The SecurityTokenServiceDescriptor in the metadata does not contain the Signing Certificate in the <X509Certificate> element");
                    }
                    throw new Exception("The Federation Metadata document does not contain a SecurityTokenServiceDescriptor");
                }
                throw new Exception("Invalid Federation Metadata document");
            }
        }

        static bool RequestIsNoAuth()
        {
            if (HttpContext.Current != null
                &&
                HttpContext.Current.Request != null
                &&
                HttpContext.Current.Request.Url != null
                &&
                HttpContext.Current.Request.Url.ToString().ToLowerInvariant().Contains("/noauth/api"))
            {
                return true;
            }

            return false;
        }

        static bool UrlReferrerIsNoAuth()
        {
            if (HttpContext.Current != null
                &&
                HttpContext.Current.Request != null
                &&
                HttpContext.Current.Request.UrlReferrer != null
                &&
                HttpContext.Current.Request.UrlReferrer.AbsoluteUri.ToLowerInvariant().Contains("/noauth"))
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Returns the federation information for the tenant identified by the metadataAddress
        /// </summary>
        /// <param name="metadataAddress"></param>
        /// <param name="issuer"></param>
        private static void GetTenantInformation(string metadataAddress, out string issuer)
        {
            // Cache metadata information for 24 hrs. 
            if (DateTime.Now.Subtract(stsMetadataRetrievalTime).TotalHours > 24 || string.IsNullOrEmpty(stsIssuer) || 0 != string.CompareOrdinal(stsMetadataAddress, metadataAddress))
            {
                stsIssuer = null;
                stsMetadataAddress = metadataAddress;
                using (XmlReader metadataReader = XmlReader.Create(metadataAddress))
                {
                    MetadataSerializer serializer = new MetadataSerializer()
                    {
                        CertificateValidationMode = X509CertificateValidationMode.None
                    };

                    EntityDescriptor metadata = serializer.ReadMetadata(metadataReader) as EntityDescriptor;

                    // Read the expected issuer from metadata
                    stsIssuer = metadata.EntityId.Id;
                }
                stsMetadataRetrievalTime = DateTime.Now;
            }

            issuer = stsIssuer;
        }
    }

    class CustomX509CertificateValidator : X509CertificateValidator
    {
        /// <summary>
        /// Avoid validation
        /// </summary>
        /// <param name="certificate"></param>
        public override void Validate(X509Certificate2 certificate)
        {
        }
    }
}