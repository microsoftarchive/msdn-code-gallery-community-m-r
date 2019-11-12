

namespace MyCompany.Common.Security.Web
{

    /// <summary>
    /// Security Helper
    /// </summary>
    public interface ISecurityHelper
    {
        /// <summary>
        /// Get Logged User Email
        /// </summary>
        /// <returns></returns>
        string GetUser();
    }
}