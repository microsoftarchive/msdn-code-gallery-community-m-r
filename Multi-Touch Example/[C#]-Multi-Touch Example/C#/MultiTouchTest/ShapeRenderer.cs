using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using Matrix = SharpDX.Matrix;
using TextAntialiasMode = SharpDX.Direct2D1.TextAntialiasMode;

namespace MultiTouchTest
{
    public class ShapeRenderer
    {
        private TextFormat textFormat;
        private TextFormat textFormat2;
        private StrokeStyle strokeStyle;
        private Brush sceneColorBrush;
        private Brush lineColorBrush;
        private StrokeStyleProperties strokeProperties;

        private Ellipse ellipse;
        private Windows.Devices.Input.TouchCapabilities TouchCapabilities;

        public ShapeRenderer()
        {

        }

        public virtual void Initialize(DeviceManager deviceManager)
        {
            TouchCapabilities = new Windows.Devices.Input.TouchCapabilities();

            // Initialize a TextFormat
            textFormat = new TextFormat(deviceManager.FactoryDirectWrite, "Calibri", 20) { TextAlignment = TextAlignment.Center, ParagraphAlignment = ParagraphAlignment.Center };
            textFormat2 = new TextFormat(deviceManager.FactoryDirectWrite, "Calibri", 20) { TextAlignment = TextAlignment.Leading, ParagraphAlignment = ParagraphAlignment.Center };

            sceneColorBrush = new SolidColorBrush(deviceManager.ContextDirect2D, Color.Tomato);

            strokeProperties = new StrokeStyleProperties();
            strokeProperties.StartCap = CapStyle.Round;
            strokeProperties.EndCap = CapStyle.Round;
            strokeProperties.LineJoin = LineJoin.Round;

            strokeStyle = new StrokeStyle(deviceManager.FactoryDirect2D, strokeProperties);
        }

        // 繪圖主要方法
        public virtual void Render(TargetBase target)
        {

            var context2D = target.DeviceManager.ContextDirect2D;

            context2D.BeginDraw();
            context2D.Clear(Color.White);

            var sizeX = (float)target.RenderTargetBounds.Width;
            var sizeY = (float)target.RenderTargetBounds.Height;

            try
            {
                if (MainPage.pointers != null)
                {
                    for (int j = 0; j < MainPage.pointers.Count; j++)
                    {
                        // Different color for touch points
                        lineColorBrush = new SolidColorBrush(context2D, MainPage.pointers[j].color);

                        for (int i = 0; i < MainPage.pointers[j].Pointers.Count; i++)
                        {
                            if (i == MainPage.pointers[j].Pointers.Count - 1)
                            {
                                float x = (float)MainPage.pointers[j].Pointers[i].X;
                                float y = (float)MainPage.pointers[j].Pointers[i].Y;
                                // Pointers info
                                context2D.DrawText(string.Format("PointerID:{0}\nX:{1}\nY:{2}\n{3}", MainPage.pointers[j].PointerId, x, y, MainPage.pointers[j].DeviceType.ToString())
                                    , textFormat, new RectangleF(x - 150, y - 100, x - 20, y - 20), sceneColorBrush);

                                // Draw horizontal line
                                context2D.DrawLine(new DrawingPointF(0, y), new DrawingPointF(context2D.PixelSize.Width, y), lineColorBrush);
                                // Draw vertical line
                                context2D.DrawLine(new DrawingPointF(x, 0), new DrawingPointF(x, context2D.PixelSize.Height), lineColorBrush);
                                // Draw a circle (and like a Crosshair :D )
                                ellipse = new Ellipse(new DrawingPointF(x, y), 30, 30);
                                context2D.DrawEllipse(ellipse, lineColorBrush);
                                continue;
                            }

                            var beginPoint = new DrawingPointF((float)MainPage.pointers[j].Pointers[i].X, (float)MainPage.pointers[j].Pointers[i].Y);
                            var endPoint = new DrawingPointF((float)MainPage.pointers[j].Pointers[i + 1].X, (float)MainPage.pointers[j].Pointers[i + 1].Y);
                            context2D.DrawLine(beginPoint, endPoint, lineColorBrush, 10, strokeStyle);
                        }
                    }

                    // Update pointers contacts
                    context2D.DrawText(string.Format("Pointers Count:{0}/{1}", MainPage.pointers.Count, TouchCapabilities.Contacts), textFormat2, new RectangleF(8, 30, 8 + 200, 30 + 16), sceneColorBrush);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error:" + ex);
            }

            context2D.EndDraw();
        }
    }
}
