namespace MyCompany.Travel.Web.Security
{
    /// <summary>
    /// Methods to access to WAAD information
    /// </summary>
    public interface IADGraphApi
    {
        /// <summary>
        /// Check if the user is member of the group
        /// </summary>
        /// <param name="userPrincipalName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        bool IsInGroup(string userPrincipalName, string groupName);
    }
}
