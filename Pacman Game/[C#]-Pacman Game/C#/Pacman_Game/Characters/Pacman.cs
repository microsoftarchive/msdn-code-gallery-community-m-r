using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Packman_Game.Characters
{
    public delegate void Pacman_Movement(object sender, System.Drawing.Point location);
    public delegate void Pacman_PointsChanged(object sender, int totalPoints);
    public delegate void Pacman_Messages(object sender, string messages);

    public class Pacman : System.Windows.Forms.Control, ICharacter
    {
        public event Pacman_Movement Pacman_Movement;
        public event Pacman_PointsChanged Pacman_PointsChanged;
        public event Pacman_Messages Pacman_Messages;

        private Dots[] _Dots = null;
        private Block[] _Blocks = null;

        private bool _Catched = false;

        public Pacman()
        {
            this.Width = this.Height = 40;
        }

        public Pacman(ref Dots[] dots)
            : this()
        {
            _Dots = dots;
            this.Pacman_Movement += new Characters.Pacman_Movement(Pacman_Pacman_Movement);
        }

        public Pacman(ref Dots[] dots, ref Block[] blocks)
            : this(ref dots)
        {
            _Blocks = blocks;
        }

        public Pacman(ref Block[] blocks)
            : this()
        {
            _Blocks = blocks;
        }

        void Pacman_Pacman_Movement(object sender, System.Drawing.Point location)
        {
            for (int i = 0; i <= _Dots.Length - 1; i++)
            {
                if (_Dots[i] == null)
                    continue;

                if (_Dots[i].Location.X >= location.X && _Dots[i].Location.X <= (location.X + (this.Width/3)) && _Dots[i].Location.Y >= location.Y && _Dots[i].Location.Y <= ((this.Height/3)+ location.Y))
                {
                    (sender as Characters.Pacman).TotalPoints += _Dots[i].Points;
                    _Dots[i].Dispose();
                    _Dots[i] = null;
                }
            }

            if ((_Dots.Where(d => d != null).Count() < 1))
            {
                if (Pacman_Messages != null)
                    Pacman_Messages(this, "You win !!");
            }
        }

        private bool IsBlock()
        {
            bool result = false;


            Point loc = new Point();
            loc.X = this.Location.X;
            loc.Y = this.Location.Y;

            if (_Movement == MovementWay.Right)
                loc.X += this.Width;

            for (int i = 0; i <= _Blocks.Length -1; i++)
            {
                if (_Blocks[i] == null)
                    continue;

                switch (_Movement)
                {
                    case MovementWay.Right:
                        if (loc.X == _Blocks[i].Location.X)
                        {
                            if (loc.Y >= (_Blocks[i].Location.Y - Speed) && loc.Y <= (_Blocks[i].Location.Y + _Blocks[i].Height - Speed))
                            {
                                result = true;
                                break;
                            }
                        }
                        break;
                    case MovementWay.Left:
                        if (loc.X == (_Blocks[i].Location.X + _Blocks[i].Width))
                        {
                            if (loc.Y >= (_Blocks[i].Location.Y - Speed)&& loc.Y <= (_Blocks[i].Location.Y + _Blocks[i].Height - Speed))
                            {
                                result = true;
                                break;
                            }
                        }
                        break;

                    case MovementWay.Up:
                        if (loc.Y == (_Blocks[i].Location.Y + _Blocks[i].Height))
                        {
                            if (loc.X >= (_Blocks[i].Location.X - Speed) && loc.X <= (_Blocks[i].Location.X + _Blocks[i].Width - Speed))
                            {
                                result = true;
                                break;
                            }
                        }
                        break;

                    case MovementWay.Down:
                        if ((loc.Y + this.Height) == _Blocks[i].Location.Y)
                        {
                            if (loc.X >= (_Blocks[i].Location.X - Speed) && loc.X <= (_Blocks[i].Location.X + _Blocks[i].Width -Speed))
                            {
                                result = true;
                                break;
                            }
                        }
                        break;
                }




            }

            return result;
        }


        public new void Move(MovementWay way)
        {
            if (_Catched)
                return;

            _Movement = way;

            OnPaint(new System.Windows.Forms.PaintEventArgs(this.CreateGraphics(), this.ClientRectangle));

            if (!IsBlock())
                switch (way)
                {
                    case MovementWay.Up:
                        this.Location = new System.Drawing.Point(this.Location.X, this.Location.Y - Speed);
                        break;

                    case MovementWay.Down:
                        this.Location = new System.Drawing.Point(this.Location.X, this.Location.Y + Speed);
                        break;

                    case MovementWay.Left:
                        this.Location = new System.Drawing.Point(this.Location.X - Speed, this.Location.Y);
                        break;

                    case MovementWay.Right:
                        this.Location = new System.Drawing.Point(this.Location.X + Speed, this.Location.Y);
                        break;
                }

            if (_Dots != null)
            {
                Pacman_Movement(this, this.Location);
                return;
            }

            if (Pacman_Movement != null)
                Pacman_Movement(this, this.Location);

        }

        private void FillRegion()
        {

            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, this.Width, this.Height);

            switch (_Movement)
            {
                case MovementWay.Right:
                    path.AddPie(0, 0, this.Width, this.Height, 310, 100);
                    break;

                case MovementWay.Left:
                    path.AddPie(0, 0, this.Width, this.Height, 130, 100);
                    break;

                case MovementWay.Up:
                    path.AddPie(0, 0, this.Width, this.Height, 220, 100);
                    break;

                case MovementWay.Down:
                    path.AddPie(0, 0, this.Width, this.Height, 40, 100);
                    break;
            }

            this.Region = new System.Drawing.Region(path);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            DrawCharacter.Draw(ref e, Type, _Movement);

            FillRegion();

            base.OnPaint(e);
        }

        private MovementWay _Movement = MovementWay.Right;

        CharacterType m_Type = CharacterType.Packman;
        public CharacterType Type
        {
            get
            {
                return m_Type;
            }
        }

        int m_TotalPoints = 0;
        public int TotalPoints
        {
            get
            {
                return m_TotalPoints;
            }
            set
            {
                m_TotalPoints = value;
                if (Pacman_PointsChanged != null)
                    Pacman_PointsChanged(this, value);
            }
        }

        public virtual void Catched(Enemy sender)
        {
            Graphics g = this.CreateGraphics();

            g.FillEllipse(System.Drawing.Brushes.Red, 0, 0, Width, Height);
            g.FillEllipse(System.Drawing.Brushes.Black, 20, 10, 5, 5);
            g.FillEllipse(System.Drawing.Brushes.Transparent, 35, 20, 10, 5);


            _Catched = true;

            if (Pacman_Messages != null)
                Pacman_Messages(this, "Pacman has been catched by an enemy.");
        }

        int m_Speed = 20;
        public int Speed
        {
            get
            {
                return m_Speed;
            }
            set
            {
                m_Speed = value;
            }
        }
    }
}
