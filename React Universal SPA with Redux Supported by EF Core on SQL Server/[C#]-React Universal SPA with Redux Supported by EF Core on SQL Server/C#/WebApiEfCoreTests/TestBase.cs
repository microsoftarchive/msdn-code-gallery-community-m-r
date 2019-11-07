using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebApiEfCore;

namespace WebApiEfCoreTests
{
    public abstract class TestBase
    {
        protected readonly HttpClient _client;

        protected TestBase()
        {
            var current = System.IO.Directory.GetCurrentDirectory();
            var index = current.IndexOf("\\bin", StringComparison.Ordinal);
            var projectDir = current.Remove(index);

            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseContentRoot(projectDir)
                .UseConfiguration(new ConfigurationBuilder()
                    .SetBasePath(projectDir)
                    .AddJsonFile("appsettings.json")
                    .Build()
                )
                .UseStartup<Startup>());

            _client = server.CreateClient();
        }

        protected static string Host { get; } = "https://localhost:44328";

        protected HttpContent GetPostData(object inputModel)
        {
            var inputStr = JsonConvert.SerializeObject(inputModel);
            return new StringContent(inputStr, Encoding.Default, "application/json");
        }

        protected async Task<T> GetResponseResultAsync<T>(HttpResponseMessage response)
        {
            var responseStr = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(responseStr);
        }
    }
}
