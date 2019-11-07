using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MyCompany.Visitors.Client.iOS.ViewController;
using MyCompany.Visitors.Client.iOS.ViewControllers;
using MyCompany.Visitors.Client;
using MyCompany.Visitors.Client.ViewModels;
namespace MyCompany.Visitors.Client.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {

        public static MyCompanyClient CompanyClient { get; private set; }

        public static VMMainPage VMMainPage { get; set; }

        // class-level declarations
        UIWindow window;

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
	        app.IdleTimerDisabled = true;
            // create a new window instance based on the screen size
            window = new UIWindow(UIScreen.MainScreen.Bounds)
            {
	            TintColor = Theme.AccentColor
            };

            CompanyClient = createCompanyClient();

            window.RootViewController = new UINavigationController (new MainViewController());

			window.MakeKeyAndVisible();
	        NSNotificationCenter.DefaultCenter.AddObserver("NSUserDefaultsDidChangeNotification", (n) =>
	        {
		        CompanyClient = createCompanyClient();

				Console.WriteLine("url changed");

                VMMainPage.InitializeData(CompanyClient);
	        });
            return true;
        }

        MyCompanyClient createCompanyClient()
        {
			return new MyCompanyClient (AppSettings.ApiUri.ToString () + "noauth/", "test");//AppSettings.SecurityToken);
		}
    }
}