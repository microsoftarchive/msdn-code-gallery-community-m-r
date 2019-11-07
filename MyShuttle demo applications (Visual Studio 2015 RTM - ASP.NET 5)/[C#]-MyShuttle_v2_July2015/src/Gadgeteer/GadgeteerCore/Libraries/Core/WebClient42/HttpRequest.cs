////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using Microsoft.SPOT;
using System.Collections;
using System.Net;
using System.Threading;
using System.IO;

namespace Gadgeteer.Networking
{
    ///<summary>
    /// This class builds up a web request which can then be sent to the specified server.
    ///</summary>
    ///<remarks>Many of the web (and web service) access methods return an object of this class. Use the returned object to set up a response
    /// handler, for example: reqest.<see cref="HttpRequest.ResponseReceived"/> += new HttpRequest.ResponseHandler(messageReq_ResponseReceived);
    /// Note that the request must then be sent using the <see cref="HttpRequest.SendRequest"/> method, which is non-blocking</remarks>
    public class HttpRequest
    {
        /// <summary>
        /// The standard HTTP request methods that can be used to configure an HttpRequest object
        /// </summary>
        public enum RequestMethod { 
            /// <summary>
            /// GET request
            /// </summary>
            GET, 
            
            /// <summary>
            /// PUT request
            /// </summary>
            PUT, 
            
            /// <summary>
            /// POST request
            /// </summary>
            POST, 
            
            /// <summary>
            /// Delete request
            /// </summary>
            DELETE };

        private string[] RequestMethodString = { "GET", "PUT", "POST", "DELETE" };

        //paramaeters 
        /// <summary>
        /// The URL to be requested.
        /// </summary>
        public string URL { get; internal set; }
        /// <summary>
        /// The stream type of the encaspulated data.
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// The stream type that the request accepts or expects. 
        /// This field can be used explicitly for a certain stream type. 
        /// </summary>
        public string Accepts { get; set; }
        /// <summary>
        /// Geturns the method http uses for the request. 
        /// </summary>
        public RequestMethod HttpRequestMethod { get; internal set; }
        /// <summary>
        /// specifies a field that is used to simulate a certain device, such as a windows phone. Example user agents are: 
        /// <list type="bullet">
        /// <item><description>Internet Explorer 9:  Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)</description></item>
        /// <item><description>Windows Phone: Mozilla/4.0 (compatible; MSIE 7.0; Windows Phone OS 7.0; Trident/3.1; IEMobile/7.0)</description></item>
        /// </list>
        /// </summary>
        public string UserAgent { get; set; }
        /// <summary>
        /// The stream of the request. The stream is created by the sub-classes GET-, PUT-, POST-, DELETE stream.
        /// </summary>
        private HttpContent content;
        /// <summary>
        /// Identifies additional header fields that can be added by the user.
        /// </summary>
        private Hashtable AdditionalHeaderFieldsTable = new Hashtable();
        /// <summary>
        /// Specifies the state of the current request. True, if a response is already received. False, if no response is received.
        /// </summary>
        public bool IsReceived { get; internal set; }
        /// <summary>
        /// Private lock to prohibit race conditions.
        /// </summary>
        private object _Lock = new Object();
        /// <summary>
        /// The response returned by the requested server.  The data of the reponse is preserved and immediately raises an event
        /// if the data is requested again or an additional event handler is added after the server sent the response. 
        /// </summary>
        private HttpResponse response;

        /// <summary>
        /// Specifies the web proxy settings
        /// </summary>
        public WebProxy Proxy { get; set; }

        /// <summary>
        /// Specifies the signature for the method that is called after a response is received from the requested source. 
        /// </summary>
        /// <param name="sender">The <see cref="HttpRequest"/> object that raised the event</param>
        /// <param name="response">The <see cref="HttpResponse"/> object that the web server returned</param>
        public delegate void ResponseHandler(HttpRequest sender, HttpResponse response);

        ///<summary>
        /// Event used for an asynchronously handled Http request.
        ///</summary>
        public event ResponseHandler ResponseReceived
        {
            add
            {
                //lock (_Lock)
                {
                    _ResponseReceived += value;
                    // invoke newly added event handler directly if a response was already received 
                    if (IsReceived)
                    {
                        Program.BeginInvoke(value, this, response);
                    }
                }
            }
            remove
            {
                //lock (_Lock)
                {
                    _ResponseReceived -= value;
                }
            }
        }

        /// <summary>
        /// The internally used event that includes the added handlers.
        /// </summary>
        private ResponseHandler _ResponseReceived;
        /// <summary>
        /// The internally used event handler for the event pattern specified by the rest of the Gadgeteer API.
        /// </summary>
        private ResponseHandler _OnReceived;

        /// <summary>
        /// Raises the <see cref="HttpRequest.ResponseReceived"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="HttpRequest"/> object that raised the event</param>
        /// <param name="response">The <see cref="HttpResponse"/> object that the web server returned</param>
        internal void OnResponseReceivedEvent(HttpRequest sender, HttpResponse response)
        {
            if (_OnReceived == null)
            {
                _OnReceived = new ResponseHandler(OnResponseReceivedEvent);
            }
            if (Program.CheckAndInvoke(_ResponseReceived, _OnReceived, sender, response))
            {
                _ResponseReceived(this, response);
            }
        }

        ///<summary>
        /// Adds a new field to the http header.
        ///</summary>
        /// <param name="name">The name of the header field.</param>
        /// <param name="value">The value that correspondes to the name parameter. </param>
        public void AddHeaderField(string name, string value)
        {
            AdditionalHeaderFieldsTable.Add(name, value);
        }

        ///<summary>
        /// Synchronously requests a server with the specified parameters.  
        /// This should not be called directly. Instead, WaitForResponse 
        /// should be used. This method provides the acutal implementation 
        /// for the async request
        ///</summary>
        private void HandleRequestSync()
        {
            try
            {
                lock (_Lock)
                {
                    // if response was already made return and raise event
                    if (IsReceived)
                    {
                        OnResponseReceivedEvent(this, response);
                        return;
                    }

                    // add RawContentBytes to URL if http method is GET or DELETE
                    if (content != null)
                    {
                        if (HttpRequestMethod == RequestMethod.GET)
                        {
                            URL += ((GETContent)content).stringContent;
                        }
                        else if (HttpRequestMethod == RequestMethod.DELETE)
                        {
                            URL += ((DELETEContent)content).stringContent;
                        }
                    }

                    // build up the required message 
                    HttpWebRequest webRequest = HttpWebRequest.Create(URL) as HttpWebRequest;

                    // set the proxy server
                    if (Proxy != null)
                    {
                        webRequest.Proxy = Proxy;
                    }

                    // add certificates to the request 
                    //if (certs.Count > 0)
                    //{
                    //    request.HttpsAuthentCerts = (X509Certificate[])certs.ToArray(typeof(X509Certificate));
                    //}

                    webRequest.Method = RequestMethodString[(int)HttpRequestMethod];

                    webRequest.ProtocolVersion = HttpVersion.Version11;

                    if (Accepts != null)
                    {
                        webRequest.Accept = Accepts;
                    }

                    if (ContentType != null)
                    {
                        webRequest.ContentType = ContentType;
                    }

                    if (UserAgent != null)
                    {
                        webRequest.UserAgent = UserAgent;
                    }

                    ICollection keys = AdditionalHeaderFieldsTable.Keys;
                    foreach (string key in keys)
                    {
                        webRequest.Headers.Set(key, (string)AdditionalHeaderFieldsTable[key]);
                    }

                    // Adding RawContentBytes to http request, no RawContentBytes body allowed for GET and DELETE, but URL Parameters 
                    // RawContentBytes can be null for GET and DELETE. This is also possible for for PUT and POST, but should not occur
                    if (content != null)
                    {
                        if (HttpRequestMethod == RequestMethod.POST)
                        {
                            if (((POSTContent)(content)).byteContent != null)
                            {
                                webRequest.ContentLength = ((POSTContent)(content)).byteContent.Length;
                                Stream reqStream = webRequest.GetRequestStream();
                                reqStream.Write(((POSTContent)(content)).byteContent, 0, ((POSTContent)(content)).byteContent.Length);
                                reqStream.Close();
                            }
                            else
                            {
                                webRequest.ContentLength = 0;
                            }
                        }
                        else if (HttpRequestMethod == RequestMethod.PUT)
                        {
                            if (((PUTContent)(content)).byteContent != null)
                            {
                                webRequest.ContentLength = ((PUTContent)(content)).byteContent.Length;
                                Stream reqStream = webRequest.GetRequestStream();
                                reqStream.Write(((PUTContent)(content)).byteContent, 0, ((PUTContent)(content)).byteContent.Length);
                                reqStream.Close();
                            }
                            else
                            {
                                webRequest.ContentLength = 0;
                            }
                        }
                    }

                    //compute the response and fill HttpResponse class with arguements 
                    HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse;
                    if (webResponse.StatusCode == HttpStatusCode.OK)
                    {
                        // read data from response 
                        Stream stream = webResponse.GetResponseStream();

                        byte[] byteData = new byte[4096];
                        int temp = 0;

                        MemoryStream memoryStream = new MemoryStream();

                        while ((temp = stream.Read(byteData, 0, byteData.Length)) > 0)
                        {
                            if (temp == 0)
                            {
                                break;
                            }
                            memoryStream.Write(byteData, 0, temp);
                        }
                        stream.Close();
                        memoryStream.Close();

                        ContentType = webResponse.GetResponseHeader("Content-Type");
                        response = new HttpResponse(webResponse.StatusCode.ToString(), memoryStream.ToArray(), URL, webResponse.Headers, ContentType);
                    }
                    else
                    {
                        // request failed, but still record some of the response information to pass response received event
                        response = new HttpResponse(webResponse.StatusCode.ToString(), null, URL, webResponse.Headers, ContentType);
                    }
                    IsReceived = true;
                    webResponse.Close();
                }
                OnResponseReceivedEvent(this, response);
            }
            catch (Exception e)
            {
                Debug.Print("");
                Debug.Print("An exception occured while connecting to the Internet. Please, make sure that a valid URL is used and a network connection is up.");
                Debug.Print("");
                Debug.Print(e.Message);
            }
        }

        /// <summary>
        /// Determines whether the request has been sent so that <see cref="HttpRequest.WaitForResponse"/> and <see cref="HttpRequest.SendRequest"/>
        /// do not start the request again, which would raise an exception.
        /// </summary>
        bool RequestSent = false;

        /// <summary>
        /// Starts the request and blocks the current thread until a response from the specified source is received. An 
        /// event is raised as soon as the requested server replies. 
        /// </summary>
        /// <returns>A <see cref="HttpResponse"/> object that contains data and methods to handle the response.</returns>
        private HttpResponse WaitForResponse()
        {
            if (RequestSent == false)
            {
                RequestSent = true;
                HandleRequestSync();
            }
            return this.response;
        }

        /// <summary>
        /// Blocks the current thread and returns a <see cref="Picture"/> object.
        /// </summary>
        /// <returns>The requested <see cref="Picture"/>.</returns>
        private Picture WaitForPicture()
        {
            return WaitForResponse().Picture;
        }

        /// <summary>
        /// Blocks the current thread and returns raw bytes.
        /// </summary>
        /// <returns>A byte[] array.</returns>
        private byte[] WaitForBytes()
        {
            return WaitForResponse().RawContentBytes;
        }

        /// <summary>
        /// Blocks the current thread and returns a <see cref="Stream"/> object.
        /// </summary>
        /// <returns>The requested <see cref="Stream"/> object.</returns>
        private Stream WaitForStream()
        {
            return WaitForResponse().Stream;
        }

        /// <summary>
        /// Blocks the current thread and returns a string.
        /// </summary>
        /// <returns>The requested data as a string. </returns>
        private string WaitForText()
        {
            return WaitForResponse().Text;
        }

        /// <summary>
        /// Sends the request asynchronously and does not block the current thread. 
        /// The Received event is raised as soon as the server replies. 
        /// </summary>
        public void SendRequest()
        {
            if (!RequestSent)
            {
                RequestSent = true;
                Thread t = new Thread(new ThreadStart(HandleRequestSync));
                t.Start();
            }
        }

        /// <summary>
        /// Internal constructor that creates an <see cref="HttpRequest"/> object
        /// </summary>
        /// <param name="method">The underlying Http method.</param>
        /// <param name="URL">The URL of the server.</param>
        /// <param name="content">The request content to be sent to the server.</param>
        /// <param name="contentType">The mimetype of the encapsulated data.</param>
        /// <param name="acceptsType">The type that the client accepts or would prefer to receive. Can be ignored by the server.</param>
        /// <param name="userAgent">Generally used to identfy a requesting device or browser. Can simulate a specific environment.</param>
        internal HttpRequest(RequestMethod method, string URL, HttpContent content, string contentType, string acceptsType, string userAgent)
        {
            //always necessary 
            this.HttpRequestMethod = method;
            this.URL = URL;
            this.content = content;

            //optional 
            this.ContentType = contentType;
            this.Accepts = acceptsType;
            this.UserAgent = userAgent;

            lock (_Lock)
            {
                IsReceived = false;
            }
        }
    }
}
