using System;
using System.Net;

namespace MyEvents.Api.Client.Web
{
    /// <summary>
    /// Wrapper to use as callback in Web Request
    /// </summary>
    public class GetRequestState<T>
    {
        /// <summary>
        /// Entity that async method returns
        /// </summary>
        public Action<T> Action;

        /// <summary>
        /// Web Request
        /// </summary>
        public HttpWebRequest Request;

        /// <summary>
        /// Result default value
        /// </summary>
        public T DefaultValue;
    }
}
