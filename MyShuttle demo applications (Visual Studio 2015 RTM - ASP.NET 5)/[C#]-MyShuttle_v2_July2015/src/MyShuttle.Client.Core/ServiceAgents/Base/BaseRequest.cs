namespace MyShuttle.Client.Core.Web
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class BaseRequest : IBaseRequest
    {
        protected string _urlPrefix = string.Empty;
        protected string _securityToken = string.Empty;

        public string UrlPrefix
        {
            get
            {
                return _urlPrefix;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value) || 
                    !Uri.IsWellFormedUriString(value, UriKind.Absolute))
                {
                    return;
                }

                _urlPrefix = value;
            }
        }
        
        public BaseRequest(string urlPrefix, string securityToken)
        {
            if (String.IsNullOrEmpty(urlPrefix))
                throw new ArgumentNullException("urlPrefix");

            if (!urlPrefix.EndsWith("/"))
                urlPrefix = string.Concat(urlPrefix, "/");

            _urlPrefix = urlPrefix;
            _securityToken = securityToken.StartsWith("Bearer ") ? securityToken.Substring(7) : securityToken;
        }

        private HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _securityToken);
            return httpClient;
        }

        private HttpClient CreateHttpClientForCustomHeaders()
        {
            var httpClient = new HttpClient();

            return httpClient;
        }

        /// <summary>
        /// Do GetByVisitor
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected async Task<T> GetAsync<T>(string url)
            where T : new()
        {
            HttpClient httpClient = CreateHttpClient();
            T result;

            try
            {
                var response = await httpClient.GetStringAsync(url);
                result = await Task.Run(() => JsonConvert.DeserializeObject<T>(response));
            }
            catch (Exception)
            {
                result = new T();
            }

            return result;
        }

        protected async Task<IEnumerable<T>> GetIEnumerableAsync<T>(string url)
        {
            HttpClient httpClient = CreateHttpClient();
            IEnumerable<T> result;

            try
            {
                var response = await httpClient.GetStringAsync(url);
                result = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<T>>(response));
            }
            catch
            {
                result = Enumerable.Empty<T>();
            }

            return result;
        }

        /// <summary>
        /// Do GetByVisitor
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected async Task GetAsync(string url, string headerName = null, string headerValue = null)
        {
            HttpClient httpClient;

            if (!string.IsNullOrWhiteSpace(headerName) && !string.IsNullOrWhiteSpace(headerValue))
            {
                httpClient = CreateHttpClientForCustomHeaders();
                httpClient.DefaultRequestHeaders.Add(headerName, headerValue);
            }
            else
            { 
                httpClient = CreateHttpClient();
            }

            var responseForDebuggingPurposes = await httpClient.GetStringAsync(url);
        }

        /// <summary>
        /// Do post with results
        /// </summary>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> PostAsync<T, U>(string url, U entity)
        {
            HttpClient httpClient = CreateHttpClient();

            var content = JsonConvert.SerializeObject(entity);
            var response = await httpClient.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));

            string responseContent = await response.Content.ReadAsStringAsync();

            return await Task.Run(() => JsonConvert.DeserializeObject<T>(responseContent));
        }

        /// <summary>
        /// Do post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task PostAsync<T>(string url, T entity)
        {
            HttpClient httpClient = CreateHttpClient();

            var content = JsonConvert.SerializeObject(entity);
            var response = await httpClient.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Do post
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task PostAsync(string url)
        {
            HttpClient httpClient = CreateHttpClient();

            var response = await httpClient.PostAsync(url, null);

            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Put
        /// </summary>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task PutAsync<T>(string url, T entity)
        {
            HttpClient httpClient = CreateHttpClient();

            var content = JsonConvert.SerializeObject(entity);
            var response = await httpClient.PutAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Put
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task DeleteAsync(string url)
        {
            HttpClient httpClient = CreateHttpClient();
            var response = await httpClient.DeleteAsync(url);

            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Refreshes the security token.
        /// </summary>
        /// <param name="securityToken"></param>
        public void RefreshToken(string securityToken)
        {
            _securityToken = securityToken;
        }
    }
}
