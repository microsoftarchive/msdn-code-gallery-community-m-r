using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using PerpetualEngine.Storage;

namespace SimpleStorageDemo.Droid
{
    [Activity (Label = "SimpleStorageDemo", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            // Set the App Context before SimpleStorage is used anywhere in your Android App. Otherwise
            // the EditGroup(name) delegate can not be called (eg. nullpointer exception)
            SimpleStorage.SetContext(ApplicationContext);
        }

        protected override void OnResume()
        {
            base.OnResume();

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

            // simple presentation of the timestamp list
            var list = FindViewById<LinearLayout>(Resource.Id.list);
            foreach (var appLaunch in appLaunches) {
                if (String.IsNullOrEmpty(appLaunch))
                    continue;
                var textView = new TextView(this);
                textView.Text = appLaunch;
                textView.TextSize = 35;
                list.AddView(textView);
            }
        }
    }
}


