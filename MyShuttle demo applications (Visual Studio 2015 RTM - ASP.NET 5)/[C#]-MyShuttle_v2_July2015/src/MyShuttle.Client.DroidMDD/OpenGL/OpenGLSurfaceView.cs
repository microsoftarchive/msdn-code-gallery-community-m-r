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
using Android.Opengl;
using System.Runtime.InteropServices;

namespace MyShuttle.Client.Droid.OpenGL
{
    public class OpenGLSurfaceView : GLSurfaceView
    {
        public OpenGLSurfaceView(Context context)
                : base(context)
        {
            this.SetEGLConfigChooser(8, 8, 8, 8, 16, 0);
            this.mRenderer = new OpenGLRenderer();
            this.SetRenderer(mRenderer);
        }

        public void Close()
        {
            this.mRenderer.Done();
        }

        private OpenGLRenderer mRenderer;
    }
}