using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using PerpetualEngine.Storage;

namespace SimpleStorageDemo.iOS
{
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        UIWindow window;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            window = new UIWindow(UIScreen.MainScreen.Bounds);

            // open a new storage group with name "Demo" --- this is even possible
            // in code which is shared between Android and iOS because EditGroup is
            // a property holding a delegate which creates a plattform specific
            // instance
            var storage = SimpleStorage.EditGroup("Demo");

            // loading key "app_launches" with an empty string as default value
            var appLaunches = storage.Get("app_launches", "").Split(',').ToList();

            // adding a new timestamp to list to show that SimpleStorage is working
            appLaunches.Add(DateTime.Now.ToString());

            // save the value with key "app_launches" for next application start
            storage.Put("app_launches", String.Join(",", appLaunches));

            // simple presentation of the timestamp list with MonoTouch.Dialog
            var section = new Section();
            section.AddAll(from l in appLaunches where !String.IsNullOrEmpty(l) select new StringElement(l));
            window.RootViewController = new DialogViewController(new RootElement("SimpleStorage Demo") {
                section
            });
            
            window.MakeKeyAndVisible();
            
            return true;
        }
    }
}

