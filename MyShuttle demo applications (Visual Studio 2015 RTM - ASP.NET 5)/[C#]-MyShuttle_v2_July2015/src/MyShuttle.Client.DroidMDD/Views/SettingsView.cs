using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Views;
using Android.Content.PM;

namespace MyShuttle.Client.Droid.Views
{
    [Activity(Label = "SettingsView", ScreenOrientation = ScreenOrientation.Portrait)]
    public class SettingsView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.RequestWindowFeature(WindowFeatures.NoTitle);

            this.SetContentView(Resource.Layout.SettingsView);
        }
    }
}