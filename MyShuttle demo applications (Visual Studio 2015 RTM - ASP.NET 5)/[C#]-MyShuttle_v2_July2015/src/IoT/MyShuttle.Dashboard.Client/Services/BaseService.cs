namespace MyShuttle.Dashboard.Client.Services
{
    using System;
    using System.Threading.Tasks;
    using Windows.Web.Http;
    using Newtonsoft.Json;

    public class BaseService
    {
        protected async Task<T> GetAsync<T>(string route)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(new Uri($"{Constants.ServerAddress}{route}"));

                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<T>(responseContent);

                return result;
            }
        }
    }
}