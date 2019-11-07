using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Packman_Game.Characters
{
    public class Dots:System.Windows.Forms.Control , IDots
    {
        public Dots()
        {
            this.Width = this.Height = 30;
        }

        public Dots(int point)
            : this()
        {
            _points = point;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            System.Drawing.Pen p = new System.Drawing.Pen(Dot_Color);
            e.Graphics.FillEllipse(p.Brush,15,15,10,10);

            base.OnPaint(e);
        }

        int _points = 100;
        public int Points
        {
            get { return _points ; }
        }

        System.Drawing.Color m_Color = System.Drawing.Color.Yellow;
        public System.Drawing.Color Dot_Color
        {
            get
            {
                return m_Color;
            }
            set
            {
                m_Color = value;
            }
        }

        public new void Dispose()
        {
            _points = 0;
            this.Dispose(true);
        }

    }
}
