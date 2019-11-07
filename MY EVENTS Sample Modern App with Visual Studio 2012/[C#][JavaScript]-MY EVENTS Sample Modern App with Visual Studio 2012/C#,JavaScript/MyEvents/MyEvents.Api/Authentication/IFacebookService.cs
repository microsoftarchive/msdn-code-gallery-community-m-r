using System;
using MyEvents.Model;
namespace MyEvents.Api.Authentication
{
    /// <summary>
    /// Facebook Service
    /// </summary>
    public interface IFacebookService
    {
        /// <summary>
        /// Obtains the user information for the given token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        RegisteredUser GetUserInformation(string token);
    }
}
