using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OwinSignalR.Common
{
    public interface INotificationService 
    {
        void Send(string apiToken, string applicationSecret, string clientId, string methodToInvoke, object value);
    }

    public class NotificationService
        : INotificationService
    {
        public async void Send(
              string apiToken
            , string applicationSecret
            , string clientId
            , string methodToInvoke
            , object value)
        {
            string baseAddress = System.Configuration.ConfigurationManager.AppSettings["PulseUrl"];            

            if (!baseAddress.EndsWith("/")) 
            {
                baseAddress = baseAddress + "/";
            }

            HttpClient client = new HttpClient();

            var postBody = JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()                
            });

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string apiURIFormat = "{0}api/Pulse/connect/?Token={1}&ClientId={2}&Method={3}&ApplicationSecret={4}";

            await client.PostAsync(String.Format(apiURIFormat, baseAddress, apiToken, clientId, methodToInvoke, applicationSecret), new StringContent(postBody, Encoding.UTF8, "application/json"));
        }
    }
}
