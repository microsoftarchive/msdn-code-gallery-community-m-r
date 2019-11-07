
namespace MyShuttle.MobileServices.Controllers
{
    using Microsoft.WindowsAzure.Mobile.Service;
    using MyShuttle.MobileServices.Services;
    using Newtonsoft.Json.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class NotifyRejectedRequestController : ApiController
    {
        public ApiServices Services { get; set; }

        public async Task Post(JObject data)
        {
            await PushNotificationsService.NotifyRejectedRequestAsync(data.GetValue("employeeId").Value<string>());
        }

    }
}
