using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using MyShuttle.Client.Core.Settings;
using Cirrious.CrossCore;
using Chance.MvvmCross.Plugins.UserInteraction;
using Windows.Networking.PushNotifications;
using System.Xml.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace MyShuttle.Client.UniversalApp.Sevices.MobileServices.MyShuttle
{
    internal class MyShuttlePush
    {
        public async static void UploadChannel()
        {
            var channel = await PushNotificationChannelManager
                .CreatePushNotificationChannelForApplicationAsync();

            channel.PushNotificationReceived += channel_PushNotificationReceived;

            try
            {
                string template = String.Format(@"<toast>
                        <visual>
                            <binding template=""ToastText01"">
                                <text id=""1"">$(message)</text>
                            </binding>
                        </visual>
                        </toast>");

                var employeeId = CommonAppSettings.FixedEmployeeId;

                string[] tags = new string[] 
                    { 
                        string.Format("VehicleApproved-{0}", employeeId),
                        string.Format("VehicleRejected-{0}", employeeId),
                        string.Format("VehicleArrived-{0}", employeeId)
                    };

                await CommonAppSettings.MobileService.GetPush()
                    .RegisterTemplateAsync(channel.Uri, template, "MyShuttleTemplate", tags);
            }
            catch (Exception exception)
            {
                HandleRegisterException(exception);
            }
        }

        static void channel_PushNotificationReceived(PushNotificationChannel sender, 
            PushNotificationReceivedEventArgs args)
        {
        }

        private static async void HandleRegisterException(Exception exception)
        {
            await ShowAlertAsync(exception.Message, title: "Error");
        }

        private static async Task ShowAlertAsync(string message, string title = "")
        {
            var userInteractionService = Mvx.Resolve<IUserInteraction>();

            if (userInteractionService != null)
            {
                await userInteractionService.AlertAsync(message, title: title);
            }
        }
    }
}
