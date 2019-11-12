

namespace MyCompany.Travel.Web
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
        string GetUser(bool isNoAuth = false);
    }
}