
namespace MyShuttle.MobileServices.Controllers
{
    using Microsoft.WindowsAzure.Mobile.Service;
    using MyShuttle.MobileServices.Services;
    using Newtonsoft.Json.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class NotifyNewRequestController : ApiController
    {
        public ApiServices Services { get; set; }

        public async Task Post(JObject data)
        {
            await PushNotificationsService.NotifyVehicleRequestedAsync
                (
                data.GetValue("employeeId").Value<string>(),
                data.GetValue("driverId").Value<int>(),
                data.GetValue("latitude").Value<double>(),
                data.GetValue("longitude").Value<double>()
            );
        }

    }
}
