namespace MyCompany.Expenses.Client
{
    using System.Threading.Tasks;

    /// <summary>
    /// Class to access to the Security Controller 
    /// </summary>
    public interface ISecurityService
    {
        /// <summary>
        /// Log out
        /// </summary>
        /// <returns></returns>
        Task<string> GetLogoutUrl();
    }
}
