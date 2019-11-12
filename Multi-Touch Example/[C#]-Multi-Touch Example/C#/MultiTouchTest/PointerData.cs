using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Input;
using Windows.Foundation;

namespace MultiTouchTest
{
    public class PointerData
    {
        public uint PointerId { get; set; }                 // store the PointerId
        public PointerDeviceType DeviceType { get; set; }   // store the DeviceType
        public List<Point> Pointers { get; set; }           // line of dots
        public Color4 color { get; set; }

        public PointerData(SharpDX.Direct2D1.DeviceContext context, uint id, PointerDeviceType type, Point p)
        {
            this.PointerId = id;
            this.DeviceType = type;
            this.Pointers = new List<Point>();
            this.Pointers.Add(p);
            Random rnd = new Random();
            // Colors of lines. presents in RGBA (a random value between 0.5 ~ 1.0)
            color = new Color4(rnd.NextFloat(0.5f, 1), rnd.NextFloat(0.5f, 1), rnd.NextFloat(0.5f, 1), 1);
        }
    }
}
