

namespace MyShuttle.MobileServices.Controllers
{
    using Microsoft.WindowsAzure.Mobile.Service;
    using MyShuttle.MobileServices.Services;
    using Newtonsoft.Json.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class NotifyApprovedRequestController : ApiController
    {
        public ApiServices Services { get; set; }

        public async Task Post(JObject data)
        {
            await PushNotificationsService.NotifyApprovedRequestAsync(data.GetValue("employeeId").Value<string>());
        }


    }
}
