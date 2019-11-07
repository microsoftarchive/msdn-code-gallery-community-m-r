
namespace MyCompany.Expenses.Web.Mobile
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Web;
    using System.Web.Hosting;
    using System.Xml.Linq;

    /// <summary>
    /// Multi Tenant Issuer Name Registry
    /// </summary>
    public class MultiTenantIssuerNameRegistry : ValidatingIssuerNameRegistry
    {
        private static XDocument doc;
        private static string filePath;

        /// <summary>
        /// MultiTenantIssuerNameRegistry
        /// </summary>
        static MultiTenantIssuerNameRegistry()
        {
            filePath = HostingEnvironment.MapPath("~/App_Data/tenants.xml");
            doc = XDocument.Load(filePath);
        }

        /// <summary>
        /// Contains Key
        /// </summary>
        /// <param name="thumbprint"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static bool ContainsKey(string thumbprint)
        {
            return doc.Descendants("key")
                .Where(tenant => tenant.Attribute("id").Value == thumbprint)
                .Any();
        }

        /// <summary>
        /// Refresh Keys
        /// </summary>
        /// <param name="metadataLocation"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void RefreshKeys(string metadataLocation)
        {
            IssuingAuthority issuingAuthority = ValidatingIssuerNameRegistry.GetIssuingAuthority(metadataLocation);

            bool newKeys = false;
            foreach (string thumbPrint in issuingAuthority.Thumbprints)
            {
                if (!ContainsKey(thumbPrint))
                {
                    newKeys = true;
                    break;
                }
            }

            if (newKeys)
            {
                XElement keysRoot = (XElement)(from tenant in doc.Descendants("keys") select tenant).First();
                keysRoot.RemoveNodes();
                foreach (string thumbp in issuingAuthority.Thumbprints)
                {
                    XElement node = new XElement("key", new XAttribute("id", thumbp));
                    keysRoot.Add(node);
                }
                doc.Save(filePath);
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