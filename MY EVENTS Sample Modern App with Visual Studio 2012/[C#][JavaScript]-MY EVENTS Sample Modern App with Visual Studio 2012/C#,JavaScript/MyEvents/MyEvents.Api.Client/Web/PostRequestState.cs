using System;
using System.Net;

namespace MyEvents.Api.Client.Web
{
    /// <summary>
    /// Wrapper to use as callback in Web Request
    /// </summary>
    public class PostRequestState<T, U>
    {
        /// <summary>
        /// Entity that async method returns
        /// </summary>
        public Action<T> Action;

        /// <summary>
        /// Result default value
        /// </summary>
        public T DefaultValue;

        /// <summary>
        /// Web Request
        /// </summary>
        public HttpWebRequest Request;

        /// <summary>
        /// Entity to send doing a POST call
        /// </summary>
        public U Entity;

    }
}
