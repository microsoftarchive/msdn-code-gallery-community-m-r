namespace MyShuttle.MobileServices.Services
{
    using System;
    using Microsoft.WindowsAzure.Mobile.Service;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Collections.Generic;

    public class PushNotificationsService
    {
        public static ApiServices Services { get; private set; }

        static PushNotificationsService()
        {
            var options = new ConfigOptions();
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));
            Services = new ApiServices(config);
        }

        internal static async Task NotifyVehicleRequestedAsync(string employeeId, int driverId, double latitude, double longitude)
        {
            var message = new Dictionary<string, string>()
            {
                { "employeeId", employeeId },
                { "latitude", latitude.ToString() },
                { "longitude", longitude.ToString() }
            };

            string[] tags = { string.Format("VehicleRequested-{0}", driverId) };

            await Services.Push.HubClient.SendTemplateNotificationAsync(message, tags);
        }

        internal static async Task NotifyApprovedRequestAsync(string employeeId)
        {
            var message = new Dictionary<string, string>()
            {
                { "message", "The vehicle has accepted your request" }
            };

            string[] tags = { string.Format("VehicleApproved-{0}", employeeId) };

            await Services.Push.HubClient.SendTemplateNotificationAsync(message, tags);
        }

        internal static async Task NotifyRejectedRequestAsync(string employeeId)
        {
            var message = new Dictionary<string, string>()
            {
                { "message", "Sorry, the driver can not handle your request" }
            };

            string[] tags = { string.Format("VehicleRejected-{0}", employeeId) };
            await Services.Push.HubClient.SendTemplateNotificationAsync(message, tags);
        }

        internal static async Task NotifyVehicleArrivedAsync(string employeeId)
        {
            var message = new Dictionary<string, string>()
            {
                { "message", "Your vehicle has arrived" }
            };

            string[] tags = { string.Format("VehicleArrived-{0}", employeeId) };

            await Services.Push.HubClient.SendTemplateNotificationAsync(message, tags);
        }

    }
}
