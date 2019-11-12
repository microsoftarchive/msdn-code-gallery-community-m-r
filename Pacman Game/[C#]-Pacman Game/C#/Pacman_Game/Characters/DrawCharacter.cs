using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Packman_Game.Characters
{
    public sealed class DrawCharacter
    {
        public static void Draw(ref System.Windows.Forms.PaintEventArgs e, CharacterType type,MovementWay way = MovementWay.Right)
        {
            switch (type)
            {
                case CharacterType.Packman:
                    e.Graphics.Clear(System.Drawing.SystemColors.Control);
                    e.Graphics.FillEllipse(System.Drawing.Brushes.Yellow, new System.Drawing.Rectangle(0, 0, 40, 40));
                    switch (way)
                    {
                        case MovementWay.Right:
                            e.Graphics.FillPolygon(System.Drawing.SystemBrushes.Control, new System.Drawing.Point[] { new System.Drawing.Point(40, 0), new System.Drawing.Point(20, 20), new System.Drawing.Point(40, 40) });
                            e.Graphics.FillEllipse(System.Drawing.Brushes.Black, new System.Drawing.Rectangle(10, 10, 5, 5));
                            break;
                        case MovementWay.Left:
                            e.Graphics.FillPolygon(System.Drawing.SystemBrushes.Control, new System.Drawing.Point[] { new System.Drawing.Point(0, 0), new System.Drawing.Point(20, 20), new System.Drawing.Point(0, 40) });
                            e.Graphics.FillEllipse(System.Drawing.Brushes.Black, new System.Drawing.Rectangle(20, 10, 5, 5));
                            break;

                        case MovementWay.Up:
                            e.Graphics.FillPolygon(System.Drawing.SystemBrushes.Control, new System.Drawing.Point[] { new System.Drawing.Point(0, 0), new System.Drawing.Point(20, 20), new System.Drawing.Point(40, 0) });
                            e.Graphics.FillEllipse(System.Drawing.Brushes.Black, new System.Drawing.Rectangle(10, 20, 5, 5));
                            break;

                        case MovementWay.Down:
                            e.Graphics.FillPolygon(System.Drawing.SystemBrushes.Control, new System.Drawing.Point[] { new System.Drawing.Point(0, 40), new System.Drawing.Point(20, 20), new System.Drawing.Point(40, 40) });
                            e.Graphics.FillEllipse(System.Drawing.Brushes.Black, new System.Drawing.Rectangle(10, 10, 5, 5));
                            break;

                    }
                    break;

                case CharacterType.Enemy:
                    e.Graphics.FillEllipse(System.Drawing.Brushes.Blue, new System.Drawing.Rectangle(0, 0, 40, 50));
                    e.Graphics.FillEllipse(System.Drawing.Brushes.LightYellow, new System.Drawing.Rectangle(10, 10, 5, 5));
                    e.Graphics.FillEllipse(System.Drawing.Brushes.LightYellow, new System.Drawing.Rectangle(25, 10, 5, 5));
                    e.Graphics.FillPolygon(System.Drawing.SystemBrushes.Control,
                                            new System.Drawing.Point[] { 
                                                    new System.Drawing.Point(0, 40), 
                                                    new System.Drawing.Point(5, 30), 
                                                    new System.Drawing.Point(10, 40),
                                                    new System.Drawing.Point(15, 30),
                                                    new System.Drawing.Point(20, 40),
                                                    new System.Drawing.Point(25, 30),
                                                    new System.Drawing.Point(30, 40),
                                                    new System.Drawing.Point(35, 30),
                                                    new System.Drawing.Point(40, 40),});

                    break;
            }
        }

    }
}
