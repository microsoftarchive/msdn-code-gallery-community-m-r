namespace MyShuttle.MobileServices.Controllers
{
    using Microsoft.WindowsAzure.Mobile.Service;
    using MyShuttle.MobileServices.Services;
    using Newtonsoft.Json.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class NotifyVehicleArrivedController : ApiController
    {
        public ApiServices Services { get; set; }

        public async Task PostNotifyArrival(JObject data)
        {
            await PushNotificationsService.NotifyVehicleArrivedAsync(data.GetValue("employeeId").Value<string>());
        }

    }
}
