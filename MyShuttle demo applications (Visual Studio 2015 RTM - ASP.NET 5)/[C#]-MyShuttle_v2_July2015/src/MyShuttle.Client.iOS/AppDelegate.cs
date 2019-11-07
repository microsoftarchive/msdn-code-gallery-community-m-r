using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.ViewModels;
using Microsoft.WindowsAzure.MobileServices;
using System;
using Chance.MvvmCross.Plugins.UserInteraction;
using MyShuttle.Client.Core.Settings;
using System.Globalization;

namespace MyShuttle.Client.iOS
{
    [Register ("AppDelegate")]
	public partial class AppDelegate : MvxApplicationDelegate
	{
		UIWindow _window;
        
        private string deviceToken;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
            // iOS 7 notifications
            //var notificationTypes =
            //     UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
            //UIApplication.SharedApplication
            //    .RegisterForRemoteNotificationTypes(notificationTypes); 

            // iOS 8 notifications
            var userNotificationSettings = UIUserNotificationSettings.GetSettingsForTypes(
                UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, 
                null);
            UIApplication.SharedApplication.RegisterUserNotificationSettings(userNotificationSettings);

			_window = new UIWindow (UIScreen.MainScreen.Bounds);

			var setup = new Setup(this, _window);
			setup.Initialize();

			var startup = Mvx.Resolve<IMvxAppStart>();
			startup.Start();

			_window.MakeKeyAndVisible ();
			
			return true;
		}

        public override void DidRegisterUserNotificationSettings(UIApplication application, UIUserNotificationSettings notificationSettings)
        {
            application.RegisterForRemoteNotifications();
        }

        public async override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            this.deviceToken = deviceToken.ToString().Replace("<", "").Replace(">", "").Replace(" ", "");

            var push = CommonAppSettings.MobileService.GetPush();
            var employeeId = CommonAppSettings.FixedEmployeeId;

            try
            {
                var jsonTemplate = "{\"aps\":{\"message\":\"$(message)\"}}";
                var expiry = DateTime.Now.AddDays(90).ToString(CultureInfo.CreateSpecificCulture("en-US"));

                await push.RegisterTemplateAsync(this.deviceToken, jsonTemplate, expiry, "MyShuttleTemplate",
                    new string[] 
                    { 
                        string.Format("VehicleApproved-{0}", employeeId),
                        string.Format("VehicleRejected-{0}", employeeId),
                        string.Format("VehicleArrived-{0}", employeeId)
                    });
            }
            catch (Exception e)
            {
                var userInteractionService = Mvx.Resolve<IUserInteraction>();

                if (userInteractionService != null)
                {
                    userInteractionService.Alert(e.Message, title: "Error registering notifications");
                }
            }
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, System.Action<UIBackgroundFetchResult> completionHandler)
        {
            var notificationContent = userInfo["aps"] as NSDictionary;

            if (notificationContent == null)
            {
                return;
            }

            var message = notificationContent["message"];

            if (message == null || string.IsNullOrWhiteSpace(message.ToString()))
            {
                return;
            }

            var userInteractionService = Mvx.Resolve<IUserInteraction>();

            if (userInteractionService != null)
            {
                userInteractionService.Alert(message.ToString());
            }
        }
	}
}