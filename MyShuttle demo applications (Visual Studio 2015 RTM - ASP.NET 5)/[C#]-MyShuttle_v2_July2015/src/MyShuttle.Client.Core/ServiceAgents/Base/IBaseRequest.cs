namespace MyShuttle.Client.Core.Web
{
    /// <summary>
    /// Base request interface
    /// </summary>
    public interface IBaseRequest
    {
        /// <summary>
        /// Refreshes the security token.
        /// </summary>
        /// <param name="securityToken"></param>
        void RefreshToken(string securityToken);
    }
}
