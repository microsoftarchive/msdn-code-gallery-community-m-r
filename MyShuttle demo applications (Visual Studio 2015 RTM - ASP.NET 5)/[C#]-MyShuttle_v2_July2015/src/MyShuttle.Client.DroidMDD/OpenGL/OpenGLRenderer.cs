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
using Javax.Microedition.Khronos.Opengles;
using System.Runtime.InteropServices;

namespace MyShuttle.Client.Droid.OpenGL
{
    public class OpenGLRenderer : Java.Lang.Object, GLSurfaceView.IRenderer
    {
        public void OnSurfaceCreated(IGL10 gl, Javax.Microedition.Khronos.Egl.EGLConfig config)
        {
            NativeInit(IntPtr.Zero);
        }

        public void OnSurfaceChanged(IGL10 gl, int w, int h)
        {
            //gl.glViewport(0, 0, w, h);
            NativeResize(IntPtr.Zero, IntPtr.Zero, w, h);
        }

        public void OnDrawFrame(IGL10 gl)
        {
            NativeRender(IntPtr.Zero);
        }

        public void Done()
        {
            NativeDone(IntPtr.Zero);
        }
        
        [DllImport("loadingopengl", EntryPoint = "nativeInit")]
        private static extern void NativeInit(IntPtr jnienv);
        [DllImport("loadingopengl", EntryPoint = "nativeResize")]
        private static extern void NativeResize(IntPtr jnienv, IntPtr thiz, int w, int h);
        [DllImport("loadingopengl", EntryPoint = "nativeRender")]
        private static extern void NativeRender(IntPtr jnienv);
        [DllImport("loadingopengl", EntryPoint = "nativeDone")]
        private static extern void NativeDone(IntPtr jnienv);
    }
}