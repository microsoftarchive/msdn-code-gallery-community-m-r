using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Packman_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Characters.Dots[] dots = new Characters.Dots[100];
        Characters.Block[] blocks = new Characters.Block[19];

        private void button1_Click(object sender, EventArgs e)
        {

            this.PacmanGroupBox.Controls.Clear();
            this.PacmanGroupBox.Refresh();

            GameInitialization();

        }

        void GameInitialization()
        {
            Characters.Pacman pack = new Characters.Pacman(ref dots, ref blocks);
            pack.Pacman_PointsChanged += new Characters.Pacman_PointsChanged(pack_Pacman_PointsChanged);
            pack.Pacman_Messages += new Characters.Pacman_Messages(pack_Pacman_Messages);
            pack.Location = new Point(100, 100);

            this.PacmanGroupBox.Controls.Add(pack);

            LoadDots();
            LoadBlocks();
        }

        void LoadBlocks()
        {
            Characters.Block top = new Characters.Block(560, 20, new Point(20, 20));
            Characters.Block left = new Characters.Block(20, 480, new Point(20, 20));
            Characters.Block right = new Characters.Block(20, 480, new Point(560, 20));
            Characters.Block down = new Characters.Block(560, 20, new Point(20, 480));


            Characters.Block b = new Characters.Block(100, 20, new Point(80, 80));
            Characters.Block b1 = new Characters.Block(20, 100, new Point(80, 80));

            Characters.Block b2 = new Characters.Block(100, 20, new Point(420, 80));
            Characters.Block b3 = new Characters.Block(20, 100, new Point(500, 80));

            Characters.Block b9 = new Characters.Block(20, 100, new Point(80, 320));
            Characters.Block b10 = new Characters.Block(100, 20, new Point(80, 420));

            Characters.Block b11 = new Characters.Block(100, 20, new Point(420, 420));
            Characters.Block b12 = new Characters.Block(20, 100, new Point(500, 320));

            ////////////////////////////////////////////////

            Characters.Block b4 = new Characters.Block(20, 100, new Point(260, 100));
            Characters.Block b5 = new Characters.Block(20, 100, new Point(320, 100));

            Characters.Block b6 = new Characters.Block(180, 20, new Point(220, 240));

            Characters.Block b7 = new Characters.Block(20, 100, new Point(260, 300));
            Characters.Block b8 = new Characters.Block(20, 100, new Point(320, 300));
            
      

            Characters.Block b13 = new Characters.Block(20, 60, new Point(160, 220));
            Characters.Block b14 = new Characters.Block(20, 60, new Point(440, 220));


            blocks[0] = b;
            blocks[1] = b1;
            blocks[2] = b2;
            blocks[3] = b3;
            blocks[4] = b4;
            blocks[5] = b5;
            blocks[6] = b6;
            blocks[7] = b7;
            blocks[8] = b8;
            blocks[9] = b9;
            blocks[10] = b10;
            blocks[11] = b11;
            blocks[12] = b12;
            blocks[13] = b13;
            blocks[14] = b14;
            blocks[15] = top;
            blocks[16] = left;
            blocks[17] = down;
            blocks[18] = right;

            this.PacmanGroupBox.Controls.AddRange(blocks);
        }

        void LoadDots()
        {
            Characters.Dots d = new Characters.Dots();
            d.Location = new Point(40, 40);
            Characters.Dots d1 = new Characters.Dots();
            d1.Location = new Point(80, 40);
            Characters.Dots d2 = new Characters.Dots();
            d2.Location = new Point(120, 40);
            Characters.Dots d3 = new Characters.Dots();
            d3.Location = new Point(160, 40);

            Characters.Dots d4 = new Characters.Dots();
            d4.Location = new Point(200, 40);
            Characters.Dots d5 = new Characters.Dots();
            d5.Location = new Point(240, 40);
            Characters.Dots d6 = new Characters.Dots();
            d6.Location = new Point(280, 40);
            Characters.Dots d7 = new Characters.Dots();
            d7.Location = new Point(320, 40);

            Characters.Dots d8 = new Characters.Dots();
            d8.Location = new Point(360, 40);
            Characters.Dots d9 = new Characters.Dots();
            d9.Location = new Point(400, 40);
            Characters.Dots d10 = new Characters.Dots();
            d10.Location = new Point(440, 40);
            Characters.Dots d11 = new Characters.Dots();
            d11.Location = new Point(480, 40);

            Characters.Dots d12 = new Characters.Dots(300);
            d12.Dot_Color = Color.BlueViolet;
            d12.Location = new Point(40, 80);
            Characters.Dots d13 = new Characters.Dots();
            d13.Location = new Point(40, 120);
            Characters.Dots d14 = new Characters.Dots();
            d14.Location = new Point(40, 160);
            Characters.Dots d15 = new Characters.Dots();
            d15.Location = new Point(40, 200);

            Characters.Dots d16 = new Characters.Dots();
            d16.Location = new Point(40, 240);
            Characters.Dots d17 = new Characters.Dots();
            d17.Location = new Point(40, 280);
            Characters.Dots d18 = new Characters.Dots();
            d18.Location = new Point(40, 320);
            Characters.Dots d19 = new Characters.Dots();
            d19.Location = new Point(40, 360);

            Characters.Dots d20 = new Characters.Dots();
            d20.Location = new Point(40, 400);
            Characters.Dots d21 = new Characters.Dots();
            d21.Location = new Point(40, 440);
            Characters.Dots d22 = new Characters.Dots();
            d22.Location = new Point(80, 440);
            Characters.Dots d23 = new Characters.Dots();
            d23.Location = new Point(120, 440);

            Characters.Dots d24 = new Characters.Dots();
            d24.Location = new Point(160, 440);
            Characters.Dots d25 = new Characters.Dots();
            d25.Location = new Point(200, 440);
            Characters.Dots d26 = new Characters.Dots();
            d26.Location = new Point(240, 440);
            Characters.Dots d27 = new Characters.Dots();
            d27.Location = new Point(280, 440);

            Characters.Dots d28 = new Characters.Dots();
            d28.Location = new Point(320, 440);
            Characters.Dots d29 = new Characters.Dots();
            d29.Location = new Point(360, 440);
            Characters.Dots d30 = new Characters.Dots();
            d30.Location = new Point(400, 440);
            Characters.Dots d31 = new Characters.Dots();
            d31.Location = new Point(440, 440);

            Characters.Dots d32 = new Characters.Dots(200);
            d32.Dot_Color = Color.BlueViolet;
            d32.Location = new Point(480, 440);
            Characters.Dots d33 = new Characters.Dots();
            d33.Location = new Point(520, 440);
            Characters.Dots d34 = new Characters.Dots();
            d34.Location = new Point(520, 40);
            Characters.Dots d35 = new Characters.Dots();
            d35.Location = new Point(520, 80);


            Characters.Dots d36 = new Characters.Dots();
            d36.Location = new Point(520, 120);
            Characters.Dots d37 = new Characters.Dots();
            d37.Location = new Point(520, 160);
            Characters.Dots d38 = new Characters.Dots();
            d38.Location = new Point(520, 200);
            Characters.Dots d39 = new Characters.Dots();
            d39.Location = new Point(520, 240);

            Characters.Dots d40 = new Characters.Dots();
            d40.Location = new Point(520, 280);
            Characters.Dots d41 = new Characters.Dots();
            d41.Location = new Point(520, 320);
            Characters.Dots d42 = new Characters.Dots();
            d42.Location = new Point(520, 360);
            Characters.Dots d43 = new Characters.Dots();
            d43.Location = new Point(520, 400);


            Characters.Dots d44 = new Characters.Dots();
            d44.Location = new Point(520, 280);
            Characters.Dots d45 = new Characters.Dots();
            d45.Location = new Point(520, 320);
            Characters.Dots d46 = new Characters.Dots();
            d46.Location = new Point(520, 360);
            Characters.Dots d47 = new Characters.Dots();
            d47.Location = new Point(520, 400);




            dots[0] = d;
            dots[1] = d1;
            dots[2] = d3;
            dots[3] = d2;

            dots[4] = d4;
            dots[5] = d5;
            dots[6] = d6;
            dots[7] = d7;

            dots[8] = d8;
            dots[9] = d9;
            dots[10] = d10;
            dots[11] = d11;

            dots[12] = d12;
            dots[13] = d13;
            dots[14] = d14;
            dots[15] = d15;

            dots[16] = d16;
            dots[17] = d17;
            dots[18] = d18;
            dots[19] = d19;

            dots[20] = d20;
            dots[21] = d21;
            dots[22] = d22;
            dots[23] = d23;

            dots[24] = d24;
            dots[25] = d25;
            dots[26] = d26;
            dots[27] = d27;

            dots[28] = d28;
            dots[29] = d29;
            dots[30] = d30;
            dots[31] = d31;

            dots[32] = d32;
            dots[33] = d33;
            dots[34] = d34;
            dots[35] = d35;

            dots[36] = d36;
            dots[37] = d37;
            dots[38] = d38;
            dots[39] = d39;

            dots[40] = d40;
            dots[41] = d41;
            dots[42] = d42;
            dots[43] = d43;

            this.PacmanGroupBox.Controls.AddRange(dots);

        }

        void pack_Pacman_Messages(object sender, string messages)
        {
            MessageBox.Show(messages);
            button1_Click(sender, null);
        }

        void pack_Pacman_PointsChanged(object sender, int totalPoints)
        {
            label1.Text = totalPoints.ToString();
        }

      

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    (this.PacmanGroupBox.Controls[0] as Characters.ICharacter).Move(Characters.MovementWay.Up);
                    break;

                case Keys.S:
                    (this.PacmanGroupBox.Controls[0] as Characters.ICharacter).Move(Characters.MovementWay.Down);
                    break;

                case Keys.A:
                    (this.PacmanGroupBox.Controls[0] as Characters.ICharacter).Move(Characters.MovementWay.Left);
                    break;

                case Keys.D:
                    (this.PacmanGroupBox.Controls[0] as Characters.ICharacter).Move(Characters.MovementWay.Right);
                    break;
            }
        }

    }
}
