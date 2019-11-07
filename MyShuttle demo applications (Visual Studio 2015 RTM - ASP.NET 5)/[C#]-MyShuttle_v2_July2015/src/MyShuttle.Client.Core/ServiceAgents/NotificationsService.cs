using MyShuttle.Client.Core.Settings;
using MyShuttle.Client.Core.Web;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.Client.Core.ServiceAgents
{
    internal class NotificationsService : BaseRequest, INotificationsService
    {
        public NotificationsService(string urlPrefix, string securityToken)
            : base(urlPrefix, securityToken)
        {
        }

        public async Task RequestVehicleAsync(string employeeId, int driverId, double latitude, double longitude)
        {
            await CommonAppSettings.MobileService.InvokeApiAsync("notifyNewRequest",
                   new JObject(
                       new JProperty("employeeId", employeeId),
                       new JProperty("driverId", driverId),
                       new JProperty("latitude", latitude),
                       new JProperty("longitude", longitude)));
        }
    }
}
