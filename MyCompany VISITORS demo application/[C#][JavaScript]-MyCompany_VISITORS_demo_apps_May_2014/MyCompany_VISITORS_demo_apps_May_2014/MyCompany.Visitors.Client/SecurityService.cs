namespace MyCompany.Visitors.Client
{
    using MyCompany.Visitors.Client.Web;
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// <see cref="MyCompany.Visitors.Client.ISecurityService"/>
    /// </summary>
    internal class SecurityService : BaseRequest, ISecurityService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        /// <param name="securityToken"></param>
        public SecurityService(string urlPrefix, string securityToken)
            : base(urlPrefix, securityToken)
        {

        }

        /// <summary>
        /// <see cref="MyCompany.Visitors.Client.ISecurityService"/>
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetLogoutUrl()
        {
            string url = String.Format(CultureInfo.InvariantCulture
              , "{0}api/security/logoutUrl", _urlPrefix);

            return await base.GetAsync<string>(url);
        }
    }
}
