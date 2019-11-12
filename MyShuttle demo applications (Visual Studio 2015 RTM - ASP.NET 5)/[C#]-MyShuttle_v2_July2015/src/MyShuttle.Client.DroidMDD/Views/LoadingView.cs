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
using MyShuttle.Client.Droid.OpenGL;

namespace MyShuttle.Client.Droid.Views
{
    [Activity]
    public class LoadingView : MvxActivity
    {
        public LoadingView()
            : base()
        { }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.RequestWindowFeature(WindowFeatures.NoTitle);

            this.mGLView = new OpenGLSurfaceView(this);
            this.SetContentView(this.mGLView);
        }

        protected override void OnPause()
        {
            base.OnPause();
            this.mGLView.OnPause();
        }

        protected override void OnResume()
        {
            base.OnResume();
            this.mGLView.OnResume();
        }

        protected override void OnStop()
        {
            base.OnStop();
            this.mGLView.Close();
        }

        /// <summary>
        /// Intercepts the Back Pressed event
        /// </summary>
        public override void OnBackPressed()
        {
            // Do nothing here. Just wait to closing.
        }

        private OpenGLSurfaceView mGLView;
    }
}