using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace MyEvents.Api.Client.Web
{
    /// <summary>
    /// Class to access to the WebAPI Controller exponsed by MyEvents.Api
    /// <remarks>
    /// MyEvents.Api.Client is a portable libray that must work in Windows Phone, WinRT and .NET 4.5 applications.Not all the framework features are available
    /// </remarks>
    /// </summary>
    public class BaseRequest 
    {
        /// <summary>
        /// Authorization token 
        /// </summary>
        protected string _authorizationToken = string.Empty;

        /// <summary>
        /// Service url Prefix
        /// </summary>
        protected string _urlPrefix = string.Empty;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        /// <param name="authenticationToken">Authentication Token</param>
        public BaseRequest(string urlPrefix, string authenticationToken)
        {
            if (String.IsNullOrEmpty(urlPrefix))
                throw new ArgumentNullException("urlPrefix");

            if (!urlPrefix.EndsWith("/"))
                urlPrefix = string.Concat(urlPrefix, "/");

            _urlPrefix = urlPrefix;
            _authorizationToken = authenticationToken;
        }

        /// <summary>
        /// Do Get Http request
        /// </summary>
        /// <param name="url">Url to call</param>
        /// <param name="callback">Client Callback function</param>
        /// <returns>HttpWebRequest</returns>
        protected IAsyncResult DoGet<T>(string url, Action<T> callback)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            var requestState = new GetRequestState<T>();
            requestState.Request = request;
            requestState.Action = callback;
            request.Headers["Authorization"] = "Basic " + this._authorizationToken;

            return request.BeginGetResponse(new AsyncCallback(GetResponseCallback<T>), requestState);
        }

        /// <summary>
        /// Do Post Http request
        /// </summary>
        /// <param name="url">Url to call</param>
        /// <param name="entity">Entity to send</param>
        /// <param name="callback">Client Callback function</param>
        /// <returns>HttpWebRequest</returns>
        protected IAsyncResult DoPost<T, U>(string url, U entity, Action<T> callback)
        {
            return DoAction<T, U>(url, entity, "POST", callback);
        }

        /// <summary>
        /// Do Post Http request
        /// </summary>
        /// <param name="url">Url to call</param>
        /// <param name="callback">Client Callback function</param>
        /// <returns>HttpWebRequest</returns>
        protected IAsyncResult DoPost<T>(string url, Action<T> callback)
        {
            return DoAction<T>(url, "POST", callback);
        }

        /// <summary>
        /// Do Put Http request
        /// </summary>
        /// <param name="url">Url to call</param>
        /// <param name="entity">Entity to send</param>
        /// <param name="callback">Client Callback function</param>
        /// <returns>HttpWebRequest</returns>
        protected IAsyncResult DoPut<T, U>(string url, U entity, Action<T> callback)
        {
            return DoAction<T, U>(url, entity, "PUT", callback);
        }

        /// <summary>
        /// Do Put Http request
        /// </summary>
        /// <param name="url">Url to call</param>
        /// <param name="callback">Client Callback function</param>
        /// <returns>HttpWebRequest</returns>
        protected IAsyncResult DoPut<T>(string url, Action<T> callback)
        {
            return DoAction<T>(url, "PUT", callback);
        }

        /// <summary>
        /// Do Delete Http request
        /// </summary>
        /// <param name="url">Url to call</param>
        /// <param name="entity">Entity to send</param>
        /// <param name="callback">Client Callback function</param>
        /// <returns>HttpWebRequest</returns>
        protected IAsyncResult DoDelete<T, U>(string url, U entity, Action<T> callback)
        {
            return DoAction<T, U>(url, entity, "DELETE", callback);
        }

        /// <summary>
        /// Do Delete Http request
        /// </summary>
        /// <param name="url">Url to call</param>
        /// <param name="callback">Client Callback function</param>
        /// <returns>HttpWebRequest</returns>
        protected IAsyncResult DoDelete<T>(string url, Action<T> callback)
        {
            return DoAction<T>(url, "DELETE", callback);
        }

        /// <summary>
        /// Do Post Http request
        /// </summary>
        /// <param name="url">Url to call</param>
        /// <param name="verb">POST/PUT/DELETE</param>
        /// <param name="entity">Entity to send</param>
        /// <param name="callback">Client Callback function</param>
        /// <returns>HttpWebRequest</returns>
        protected IAsyncResult DoAction<T, U>(string url, U entity, string verb, Action<T> callback)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = verb;
            request.ContentType = "application/json;charset=utf-8";
            request.Headers["Authorization"] = "Basic " + this._authorizationToken;

            var requestState = new PostRequestState<T, U>();
            requestState.Request = request;
            requestState.Action = callback;
            requestState.Entity = entity;

            return request.BeginGetRequestStream(new AsyncCallback(BeginRequestCallback<T, U>), requestState);
        }

        /// <summary>
        /// Do Post Http request
        /// </summary>
        /// <param name="url">Url to call</param>
        /// <param name="verb">POST/PUT/DELETE</param>
        /// <param name="callback">Client Callback function</param>
        /// <returns>HttpWebRequest</returns>
        protected IAsyncResult DoAction<T>(string url, string verb, Action<T> callback)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = verb;
            request.ContentType = "application/json;charset=utf-8";
            request.Headers["Authorization"] = "Basic " + this._authorizationToken;

            var requestState = new GetRequestState<T>();
            requestState.Request = request;
            requestState.Action = callback;

            return request.BeginGetRequestStream(new AsyncCallback(BeginRequestCallback<T>), requestState);
        }

        private void BeginRequestCallback<T, U>(IAsyncResult ar)
        {
            var requestState = (PostRequestState<T, U>)ar.AsyncState;

            using (StreamWriter stream = new StreamWriter(requestState.Request.EndGetRequestStream(ar)))
            {
                string serialized = JsonConvert.SerializeObject(requestState.Entity);
                stream.Write(serialized);
            }

            requestState.Request.BeginGetResponse(new AsyncCallback(GetPostResponseCallback<T, U>), requestState);
        }

        private void BeginRequestCallback<T>(IAsyncResult ar)
        {
            var requestState = (GetRequestState<T>)ar.AsyncState;

            using (Stream stream = requestState.Request.EndGetRequestStream(ar))
            {
            }

            requestState.Request.BeginGetResponse(new AsyncCallback(GetResponseCallback<T>), requestState);
        }

        private void GetPostResponseCallback<T, U>(IAsyncResult asynchronousResult)
        {
            var callback = (PostRequestState<T, U>)asynchronousResult.AsyncState;

            HttpWebRequest request = callback.Request;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);

            Stream streamResponse = response.GetResponseStream();
            StreamReader streamRead = new StreamReader(streamResponse);

            string responseString = streamRead.ReadToEnd();

            if (!String.IsNullOrEmpty(responseString))
            {
                var result = JsonConvert.DeserializeObject<T>(responseString);

                if (callback != null)
                    callback.Action(result);
            }
            else
            {
                if (callback != null)
                    callback.Action(callback.DefaultValue);
            }
        }

        private void GetResponseCallback<T>(IAsyncResult asynchronousResult)
        {
            var callback = (GetRequestState<T>)asynchronousResult.AsyncState;

            HttpWebRequest request = callback.Request;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);


            Stream streamResponse = response.GetResponseStream();
            StreamReader streamRead = new StreamReader(streamResponse);
            string responseString = streamRead.ReadToEnd();

            if (!String.IsNullOrEmpty(responseString))
            {
                var result = JsonConvert.DeserializeObject<T>(responseString);

                if (callback != null)
                    callback.Action(result);
            }
            else
            {
                if (callback != null)
                    callback.Action(callback.DefaultValue);
            }
        }

    }

}
