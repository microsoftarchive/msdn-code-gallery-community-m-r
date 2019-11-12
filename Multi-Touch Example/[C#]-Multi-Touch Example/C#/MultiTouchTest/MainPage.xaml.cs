using SharpDX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace MultiTouchTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ImageBrush d2dBrush;
        private DeviceManager deviceManager;
        private SurfaceImageSourceTarget d2dTarget;
        private ShapeRenderer shapeRenderer;
        private FpsRenderer fpsRenderer;

        // A data structure to store pointers 
        public static List<PointerData> pointers = new List<PointerData>();

        /// <summary>
        /// Initialize a new instance of <see cref="MainPage"/>
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Get the Screen size
            var bounds = Window.Current.Bounds;
            double height = bounds.Height;
            double width = bounds.Width;
            Debug.WriteLine("Screen width:" + width + "  Screen height" + height);
            _Rect.Width = width;
            _Rect.Height = height;

            int pixelWidth = (int)(_Rect.Width * DisplayProperties.LogicalDpi / 96.0);
            int pixelHeight = (int)(_Rect.Height * DisplayProperties.LogicalDpi / 96.0);

            #region sharpDX Initialization

            // Use Rectangle that makes sharpDX to draw
            d2dBrush = new ImageBrush();
            _Rect.Fill = d2dBrush;

            // Safely dispose any previous instance
            // Creates a new DeviceManager (Direct3D, Direct2D, DirectWrite, WIC)
            deviceManager = new DeviceManager();

            // New CubeRenderer
            shapeRenderer = new ShapeRenderer();
            fpsRenderer = new FpsRenderer();

            d2dTarget = new SurfaceImageSourceTarget(pixelWidth, pixelHeight);
            d2dBrush.ImageSource = d2dTarget.ImageSource;

            // Add Initializer to device manager
            deviceManager.OnInitialize += d2dTarget.Initialize;
            deviceManager.OnInitialize += shapeRenderer.Initialize;
            deviceManager.OnInitialize += fpsRenderer.Initialize;


            // Render the cube within the CoreWindow
            d2dTarget.OnRender += shapeRenderer.Render;
            d2dTarget.OnRender += fpsRenderer.Render;

            // Initialize the device manager and all registered deviceManager.OnInitialize 
            deviceManager.Initialize(DisplayProperties.LogicalDpi);

            // Setup rendering callback
            CompositionTarget.Rendering += CompositionTarget_Rendering;

            // Callback on DpiChanged
            DisplayProperties.LogicalDpiChanged += DisplayProperties_LogicalDpiChanged;

            #endregion
        }

        #region sharpDX Methods

        void DisplayProperties_LogicalDpiChanged(object sender)
        {
            deviceManager.Dpi = DisplayProperties.LogicalDpi;
        }

        void CompositionTarget_Rendering(object sender, object e)
        {
            d2dTarget.RenderAll();
        }

        #endregion

        /// <summary>
        /// Get the Touch&Mouse pointer XY coordinate
        /// </summary>
        /// <param name="e">Event data that contains pointer XY coordinate</param>
        /// <returns>XY coordinate</returns>
        private Point GetMousePoint(PointerRoutedEventArgs e)
        {
            return new Point((int)e.GetCurrentPoint(_Rect).Position.X, (int)e.GetCurrentPoint(_Rect).Position.Y);
        }

private void _Rect_PointerPressed(object sender, PointerRoutedEventArgs e)
{
    // Add a Touch-pointer data to structure
    pointers.Add(new PointerData(deviceManager.ContextDirect2D, e.Pointer.PointerId, e.Pointer.PointerDeviceType, GetMousePoint(e)));
    Debug.WriteLine("[PointerPressed] Pointer ID:" + e.Pointer.PointerId);
}

private void _Rect_PointerMoved(object sender, PointerRoutedEventArgs e)
{
    foreach (PointerData pData in pointers)
    {
        // Find the correct PointerId in list and adds a point on it.
    if (pData.PointerId == e.Pointer.PointerId)
    {
        pData.Pointers.Add(GetMousePoint(e));e.Pointer.PointerDeviceType
        break;
    }
    }
}

private void _Rect_PointerReleased(object sender, PointerRoutedEventArgs e)
{
    // This event will fired when Touch&Mouse released "IN" the controls (released IN the Screen on this example)
    for (int i = 0; i < pointers.Count; i++)
    {
        // Find the correct PointerId and remove whole points of lines.
    if (pointers[i].PointerId == e.Pointer.PointerId)
    {
        pointers.RemoveAt(i);
        break;
    }
    }
    Debug.WriteLine("[PointerReleased] Pointer ID:" + e.Pointer.PointerId);
}

private void _Rect_PointerExited(object sender, PointerRoutedEventArgs e)
{
    // This event will fired when Touch&Mouse moved "OFF" the controls (moved OFF the Screen on this example)
    for (int i = 0; i < pointers.Count; i++)
    {
        // Find the correct PointerId and remove whole points of lines. (Do the same thing in PointerReleased)
        if (pointers[i].PointerId == e.Pointer.PointerId)
        {
            pointers.RemoveAt(i);
            break;
        }
    }
    Debug.WriteLine("[PointerExited] Pointer ID:" + e.Pointer.PointerId);
}

        private void _Rect_PointerCaptureLost(object sender, PointerRoutedEventArgs e)
        {
            Debug.WriteLine("_Rect_PointerCaptureLost");
        }

        private void _Rect_PointerCanceled(object sender, PointerRoutedEventArgs e)
        {
            Debug.WriteLine("_Rect_PointerCanceled");
        }
    }
}
