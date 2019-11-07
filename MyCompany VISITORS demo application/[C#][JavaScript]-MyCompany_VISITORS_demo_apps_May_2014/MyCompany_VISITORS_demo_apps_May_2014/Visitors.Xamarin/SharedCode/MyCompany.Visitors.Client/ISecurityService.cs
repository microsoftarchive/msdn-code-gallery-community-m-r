namespace MyCompany.Visitors.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MyCompany.Visitors.Client.Web;

    /// <summary>
    /// Class to access to the Security Controller 
    /// </summary>
    public interface ISecurityService : IBaseRequest
    {
        /// <summary>
        /// Log out
        /// </summary>
        /// <returns></returns>
        Task<string> GetLogoutUrl();
    }
}
