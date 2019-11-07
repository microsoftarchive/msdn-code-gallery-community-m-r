

namespace MyCompany.Vacation.Web
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