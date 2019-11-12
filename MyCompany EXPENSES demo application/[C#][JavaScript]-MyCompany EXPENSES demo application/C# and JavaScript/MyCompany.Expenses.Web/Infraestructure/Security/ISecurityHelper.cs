
namespace MyCompany.Expenses.Web
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