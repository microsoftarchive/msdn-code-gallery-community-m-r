
namespace MyCompany.Vacation.Web
{
    using MyCompany.Vacation.Data;
    using MyCompany.Vacation.Model;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Web;
    using System.Web.Hosting;
    using System.Xml.Linq;

    /// <summary>
    /// SingleTenantIssuerNameRegistry
    /// </summary>
    public class SingleTenantIssuerNameRegistry : ValidatingIssuerNameRegistry
    {
        /// <summary>
        /// ContainsKey
        /// </summary>
        /// <param name="thumbprint"></param>
        /// <returns></returns>
        public static bool ContainsKey(string thumbprint)
        {
            using (MyCompanyContext context = new MyCompanyContext())
            {
                return context.IssuingAuthorityKeys
                    .Where(key => key.Id == thumbprint)
                    .Any();
            }
        }

        /// <summary>
        /// RefreshKeys
        /// </summary>
        /// <param name="metadataLocation"></param>
        public static void RefreshKeys(string metadataLocation)
        {
            IssuingAuthority issuingAuthority = ValidatingIssuerNameRegistry.GetIssuingAuthority(metadataLocation);

            bool newKeys = false;
            foreach (string thumbprint in issuingAuthority.Thumbprints)
            {
                if (!ContainsKey(thumbprint))
                {
                    newKeys = true;
                    break;
                }
            }

            if (newKeys)
            {
                using (MyCompanyContext context = new MyCompanyContext())
                {
                    context.IssuingAuthorityKeys.RemoveRange(context.IssuingAuthorityKeys);
                    foreach (string thumbprint in issuingAuthority.Thumbprints)
                    {
                        context.IssuingAuthorityKeys.Add(new IssuingAuthorityKey { Id = thumbprint });
                    }
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// IsThumbprintValid
        /// </summary>
        /// <param name="thumbprint"></param>
        /// <param name="issuer"></param>
        /// <returns></returns>
        protected override bool IsThumbprintValid(string thumbprint, string issuer)
        {
            return ContainsKey(thumbprint);
        }
    }
}
