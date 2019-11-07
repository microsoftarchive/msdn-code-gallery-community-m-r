
namespace MyCompany.Visitors.Client.Web
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary>
    /// Class to access to the WebAPI Controller
    /// </summary>
    public class BaseRequest : IBaseRequest
    {
        /// <summary>
        /// Security token 
        /// </summary>
        protected string _securityToken = string.Empty;

        /// <summary>
        /// Service url Prefix
        /// </summary>
        protected string _urlPrefix = string.Empty;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        /// <param name="securityToken">Authentication Token</param>
        public BaseRequest(string urlPrefix, string securityToken)
        {
            if (String.IsNullOrEmpty(urlPrefix))
                throw new ArgumentNullException("urlPrefix");

            if (!urlPrefix.EndsWith("/"))
                urlPrefix = string.Concat(urlPrefix, "/");

            _urlPrefix = urlPrefix;
            _securityToken = securityToken.StartsWith("Bearer ") ? securityToken.Substring(7) : securityToken;
        }

        /// <summary>
        /// Do GetByVisitor
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected async Task<T> GetAsync<T>(string url)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _securityToken);

            var response = await httpClient.GetStringAsync(url);
            return await Task.Run(() => JsonConvert.DeserializeObject<T>(response));
        }

        /// <summary>
        /// Do GetByVisitor
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected async Task GetAsync(string url)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _securityToken);

            await httpClient.GetStringAsync(url);
        }

        /// <summary>
        /// Do post with results
        /// </summary>
        /// <param name="url"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> PostAsync<T, U>(string url, U entity)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _securityToken);

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
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _securityToken);

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
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _securityToken);

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
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _securityToken);

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
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _securityToken);
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
