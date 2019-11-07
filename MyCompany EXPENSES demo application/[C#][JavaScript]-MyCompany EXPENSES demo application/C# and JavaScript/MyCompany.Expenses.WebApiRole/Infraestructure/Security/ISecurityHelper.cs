
namespace MyCompany.Expenses.WebApiRole
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