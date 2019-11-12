////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Net;

namespace Gadgeteer.Networking
{
    /// <summary>
    /// Represents a Web Client used in a Microsoft .NET Gadgeteer application.
    /// </summary>
    public static class WebClient
    {
        /// <summary>
        /// Property to get or set the web proxy to use for this request.
        /// </summary>
        public static WebProxy Proxy;
  
        /// <summary>
        /// Make an asynchronous (non-blocking) request for a web page. Use the ResponseReceived event of the returned 
        /// HttpRequest object to obtain the actual web page.
        /// </summary>
        /// <param name="url">Url of the web page to get</param>
        /// <returns>An HttpRequest object on success, null otherwise</returns>
        public static HttpRequest GetFromWeb(string url)
        {
            HttpRequest request = HttpHelper.CreateHttpGetRequest(url);
            if (request != null)
            {
                if (Proxy != null)
                {
                    request.Proxy = Proxy;
                }
                request.SendRequest();
            }
            return request;
        }
    }
}
