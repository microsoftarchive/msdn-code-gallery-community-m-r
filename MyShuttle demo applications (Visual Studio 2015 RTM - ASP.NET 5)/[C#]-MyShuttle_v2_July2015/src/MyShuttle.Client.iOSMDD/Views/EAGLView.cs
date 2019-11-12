using System;

using OpenTK;
using OpenTK.Graphics.ES20;
using GL1 = OpenTK.Graphics.ES11.GL;
using All1 = OpenTK.Graphics.ES11.All;
using OpenTK.Platform.iPhoneOS;

using MonoTouch.Foundation;
using MonoTouch.CoreAnimation;
using MonoTouch.ObjCRuntime;
using MonoTouch.OpenGLES;
using MonoTouch.UIKit;
using System.Runtime.InteropServices;
using System.Drawing;

namespace MyShuttle.Client.iOS
{
	[Register ("EAGLView")]
	public class EAGLView : iPhoneOSGameView
	{
        private RectangleF screenBounds = UIScreen.MainScreen.Bounds;

        [DllImport("__Internal")]
		public extern static void appInit ();

		[DllImport("__Internal")]
		public extern static void appDeinit ();

		[DllImport("__Internal")]
		public extern static void appRender (int width, int height);

        public EAGLView(RectangleF frame) : base(frame)
        {
            LayerRetainsBacking = true;
            LayerColorFormat = EAGLColorFormat.RGBA8;
        }

		[Export ("initWithCoder:")]
		public EAGLView (NSCoder coder) : base (coder)
		{
			LayerRetainsBacking = true;
			LayerColorFormat = EAGLColorFormat.RGBA8;
		}

		[Export ("layerClass")]
		public static new Class GetLayerClass ()
		{
			return iPhoneOSGameView.GetLayerClass ();
		}

		protected override void ConfigureLayer (CAEAGLLayer eaglLayer)
		{
			eaglLayer.Opaque = true;
		}

		protected override void CreateFrameBuffer ()
		{
			ContextRenderingApi = EAGLRenderingAPI.OpenGLES1;
			base.CreateFrameBuffer ();

			appInit ();
		}

		protected override void DestroyFrameBuffer ()
		{
			base.DestroyFrameBuffer ();

			appDeinit ();
		}

		#region DisplayLink support

		int frameInterval;
		CADisplayLink displayLink;

		public bool IsAnimating { get; private set; }
		
		// How many display frames must pass between each time the display link fires.
		public int FrameInterval {
			get {
				return frameInterval;
			}
			set {
				if (value <= 0)
					throw new ArgumentException ();
				frameInterval = value;
				if (IsAnimating) {
					StopAnimating ();
					StartAnimating ();
				}
			}
		}

		public void StartAnimating ()
		{
			if (IsAnimating)
				return;
			
			CreateFrameBuffer ();
			displayLink = UIScreen.MainScreen.CreateDisplayLink (this, new Selector ("drawFrame"));
			displayLink.FrameInterval = frameInterval;
			displayLink.AddToRunLoop (NSRunLoop.Current, NSRunLoop.NSDefaultRunLoopMode);
			
			IsAnimating = true;
		}

		public void StopAnimating ()
		{
			if (!IsAnimating)
				return;

			displayLink.Invalidate ();
			displayLink = null;
			DestroyFrameBuffer ();
			IsAnimating = false;
		}

		[Export ("drawFrame")]
		void DrawFrame ()
		{
			OnRenderFrame (new FrameEventArgs ());
		}

		#endregion

		protected override void OnRenderFrame (FrameEventArgs e)
		{
			base.OnRenderFrame (e);
			
			MakeCurrent ();

			appRender ((int)screenBounds.Width, (int)screenBounds.Height);
			
			SwapBuffers ();
		}
	}
}
