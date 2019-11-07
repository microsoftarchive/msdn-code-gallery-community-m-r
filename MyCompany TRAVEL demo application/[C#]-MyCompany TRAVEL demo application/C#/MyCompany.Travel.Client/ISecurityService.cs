namespace MyCompany.Travel.Client
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Class to access to the account Controller 
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
