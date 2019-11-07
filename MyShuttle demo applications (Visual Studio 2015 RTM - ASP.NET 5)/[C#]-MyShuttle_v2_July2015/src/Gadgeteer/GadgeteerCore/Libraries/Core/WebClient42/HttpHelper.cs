////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using Microsoft.SPOT;

namespace Gadgeteer.Networking
{
    /// <summary>
    /// Helper class to create <see cref="HttpRequest"/> objects that are configured for the various http request methods (GET, PUT etc)
    /// </summary>
    public class HttpHelper
    {
        /// <summary>
        /// Create an Http PUT request.
        /// </summary>
        /// <param name="Url">The Url of the web server to which the request will be sent.</param>
        /// <param name="Content">The <see cref="PUTContent"/> object to be sent to the Url.</param>
        /// <param name="ContentType">The MIME-Type of the message.</param>
        /// <returns>An <see cref="HttpRequest"/> object that can be used to make PUT request.</returns>
        public static HttpRequest CreateHttpPutRequest(string Url, PUTContent Content, string ContentType)
        {
            return new HttpRequest(HttpRequest.RequestMethod.PUT, Url, Content, ContentType, null, null);
        }

        /// <summary>
        /// Create an Http POST request.
        /// </summary>
        /// <param name="Url">The Url of the web server to which the request will be sent.</param>
        /// <param name="Content">The <see cref="POSTContent"/> object to be sent to the Url.</param>
        /// <param name="ContentType">The MIME-Type of the message.</param>
        /// <returns>An <see cref="HttpRequest"/> object that can be used to make POST request.</returns>
        public static HttpRequest CreateHttpPostRequest(string Url, POSTContent Content, string ContentType)
        {
            return new HttpRequest(HttpRequest.RequestMethod.POST, Url, Content, ContentType, null, null);
        }

        /// <summary>
        /// Create an Http GET request.
        /// </summary>
        /// <param name="Url">The Url of the web server to which the request will be sent.</param>
        /// <param name="Content">The <see cref="GETContent"/> object to be sent to the Url.</param>
        /// <returns>An <see cref="HttpRequest"/> object that can be used to make GET request.</returns>
        public static HttpRequest CreateHttpGetRequest(string Url, GETContent Content)
        {
            return new HttpRequest(HttpRequest.RequestMethod.GET, Url, Content, null, null, null);
        }

        /// <summary>
        /// Create an Http GET request.
        /// </summary>
        /// <param name="Url">The Url of the web server to which the request will be sent.</param>
        /// <returns>An <see cref="HttpRequest"/> object that can be used to make GET request.</returns>
        public static HttpRequest CreateHttpGetRequest(string Url)
        {
            return new HttpRequest(HttpRequest.RequestMethod.GET, Url, null, null, null, null);
        }

        /// <summary>
        /// Create an Http DELETE request.
        /// </summary>
        /// <param name="Url">The Url of the web server to which the request will be sent.</param>
        /// <param name="Content">The <see cref="DELETEContent"/> object to be sent to the Url.</param>
        /// <returns>An <see cref="HttpRequest"/> object that can be used to make DELETE request.</returns>
        public static HttpRequest CreateHttpDeleteRequest(string Url, DELETEContent Content)
        {
            return new HttpRequest(HttpRequest.RequestMethod.DELETE, Url, Content, null, null, null);
        }

        /// <summary>
        /// Create an Http DELETE request.
        /// </summary>
        /// <param name="Url">The Url of the web server to which the request will be sent.</param>
        /// <returns>An <see cref="HttpRequest"/> object that can be used to make DELETE request.</returns>
        public static HttpRequest CreateHttpDeleteRequest(string Url)
        {
            return new HttpRequest(HttpRequest.RequestMethod.DELETE, Url, null, null, null, null);
        }
    }
}
