

namespace MyCompany.Expenses.Web.Mobile.Controllers
{
    using MyCompany.Common.CrossCutting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Globalization;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Configuration;
    using System.Web.Mvc;

    /// <summary>
    /// OAuth Controller
    /// </summary>
    public class OAuthController : Controller
    {
        private string remoteService = WebConfigurationManager.AppSettings["RemoteService"];
        private string replyUrl = WebConfigurationManager.AppSettings["ReplyUrl"];
        private string tenant = WebConfigurationManager.AppSettings["ida:Tenant"];
        private string clientId = WebConfigurationManager.AppSettings["ClientId"];
        private string clientSecret = WebConfigurationManager.AppSettings["ClientSecret"];
        private string code = string.Empty;
        private string securityToken = string.Empty;

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            TraceManager.TraceInfo("OAuthController Index");

            code = Request.QueryString["code"];
            string securityToken = await GetToken();
            if (!String.IsNullOrEmpty(securityToken))
            {
                TraceManager.TraceInfo("valid securityToken!");

                Session["securityToken"] = securityToken;
                Session["fullName"] = await GetFullname(securityToken);
                return RedirectToAction("Index", "Home");
            }
            return View(); 
        }

        private async Task<string> GetToken()
        {
            TraceManager.TraceInfo("GetToken");

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            string content = string.Format(
                    "grant_type=authorization_code&code={0}&client_id={1}&redirect_uri={2}&client_secret={3}",
                    code,
                    clientId,
                    replyUrl, clientSecret);

            var response = await httpClient.PostAsync
                    (string.Format("https://login.windows.net/{0}/oauth2/token", tenant),
                    new StringContent(content, Encoding.UTF8, "application/x-www-form-urlencoded"));

            string responseContent = await response.Content.ReadAsStringAsync();
            JObject jo = JsonConvert.DeserializeObject(responseContent) as JObject;
            return (string)jo["access_token"];
        }

        private async Task<string> GetFullname(string securityToken)
        {
            TraceManager.TraceInfo("GetFullname");

            string remoteService = WebConfigurationManager.AppSettings["RemoteService"];
            string url = String.Format(CultureInfo.InvariantCulture
                        , "{0}api/employees/1", remoteService);

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", securityToken);
            var response = await httpClient.GetStringAsync(url);
            JObject jo = JsonConvert.DeserializeObject(response) as JObject;
            return (string)jo["FirstName"] + " " + (string)jo["LastName"];
        }
    }
}