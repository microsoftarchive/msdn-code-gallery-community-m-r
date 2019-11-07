using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using System.Threading;
using System.Runtime.InteropServices;


namespace PictureBox.Image.Testes
{
    public partial class Form1 : Form
    {
        #region MACHADO_Atributes
        Bitmap map;
        Bitmap mapSecond;
        Boolean Move=false;
        Boolean Selection=false;
        Boolean SelectionSecond = false;  
        Point SelectedPixel;
        List<Point> points = new List<Point>();
        List<Point> pointsSecond = new List<Point>();
        Graphics MouseDown;
        Graphics MouseDownSecond;
         System.Drawing.SolidBrush myBrush;
         System.Drawing.SolidBrush myBrush2;
         System.Drawing.Image Origin;
         System.Drawing.Image FirstState;
         System.Drawing.Image FirstStateSecond;       
         Color SecondPicBrush;
         int SecondPicBrushWidth;
         private Capture _capture;
         private HaarCascade _face;
         Cross2DF cross;
         Point Current;
         Point[] p = new Point[8];
        #endregion

         public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Origin = pictureBox1.Image;
            pictureBox2.BackColor = Color.White;
            Bitmap temp = new Bitmap(pictureBox1.Image,
                new Size(pictureBox1.Width, pictureBox1.Height));
            pictureBox1.Image=temp;
            Bitmap tempSecond = new Bitmap(pictureBox2.Width,pictureBox2.Height);
            pictureBox2.Image = tempSecond;
            map = new Bitmap(pictureBox1.Image);
            mapSecond = new Bitmap(pictureBox2.Image);
            Move = false;
            checkBox1.Checked = false;
            MouseDown = pictureBox1.CreateGraphics();
            MouseDownSecond = pictureBox2.CreateGraphics();
            myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            myBrush2 = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            SecondPicBrush=Color.Black;
            SecondPicBrushWidth = 3;
        }

        #region MACHADO_FileManagement

        public void SaveImage()
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            FolderBrowserDialog fl = new FolderBrowserDialog();
            if (fl.ShowDialog() != DialogResult.Cancel)
            {

                pictureBox1.Image.Save(fl.SelectedPath + @"\" + textBox1.Text + @".png", System.Drawing.Imaging.ImageFormat.Png);
            };
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void LoadImage()
        {
            OpenFileDialog op = new OpenFileDialog();
            DialogResult dr = op.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string path = op.FileName;
                pictureBox1.Load(path);
                Bitmap temp = new Bitmap(pictureBox1.Image,
                   new Size(pictureBox1.Width, pictureBox1.Height));
                pictureBox1.Image = temp;
                map = new Bitmap(pictureBox1.Image);
                Origin = pictureBox1.Image;
            }
        }

        #endregion

        #region MACHADO_Behaviours

        private void OnMouseMove_AddToList(int x, int y) 
        {
            if (Move)
            {
                listBox1.Items.Add(map.GetPixel(x, y).ToString());

                int NumberOfItems = listBox1.ClientSize.Height / listBox1.ItemHeight;

                if (listBox1.TopIndex == listBox1.Items.Count - NumberOfItems - 1)
                {
                    listBox1.TopIndex = listBox1.Items.Count - NumberOfItems + 1;
                }
            }        
        }

        private void OnMouseMove_Draw(int x, int y)
        {
            if (Selection)
            {
                points.Add(new Point(x, y));
                MouseDown.FillRectangle(myBrush, x, y, 1, 1);
            }
        }

        private void OnCheckBoxChanged_ChangeMove()
        {
            if (checkBox1.Checked)
                Move = true;
            else
                Move = false;
        }

        private void OnPictureBox1Click(int x, int y)
        {
            label1.Text = map.GetPixel(x, y).ToString();
            PaintColor(map.GetPixel(x, y));
            SelectedPixel = new Point(x, y);
        }

        private void OnListBox1Click_ChangePanel1()
        {
            string color = listBox1.SelectedItem.ToString();
            string[] args = color.Split(',');
            string a = args[0].Substring(args[0].IndexOf("A") + 2);
            string r = args[1].Substring(args[1].IndexOf("R") + 2);
            string g = args[2].Substring(args[2].IndexOf("G") + 2);
            string b = args[3].Substring(args[3].IndexOf("B") + 2)
                .Substring(0, args[3].Substring(args[3].IndexOf("B") + 2).IndexOf(']'));

            Color temp = Color.FromArgb(Convert.ToInt32(a),
                Convert.ToInt32(r), Convert.ToInt32(g),
                Convert.ToInt32(b));

            panel1.BackColor = temp;


        }

        private void PaintColor(Color c)
        {
            panel1.BackColor = c;

        }

        public void OnButton2Click_ChangePixelColor()
        {
            if (!string.IsNullOrEmpty(txtB.Text)
                && !string.IsNullOrEmpty(txtR.Text)
                && !string.IsNullOrEmpty(txtG.Text)
                && Convert.ToInt32(txtR.Text) >= 0
                && Convert.ToInt32(txtG.Text) >= 0
                && Convert.ToInt32(txtB.Text) >= 0
                && Convert.ToInt32(txtR.Text) <= 255
                && Convert.ToInt32(txtG.Text) <= 255
                && Convert.ToInt32(txtB.Text) <= 255
                )
            {
                map.SetPixel(SelectedPixel.X, SelectedPixel.Y, Color.FromArgb(Convert.ToInt32(txtR.Text)
              , Convert.ToInt32(txtG.Text)
              , Convert.ToInt32(txtB.Text)));
                pictureBox1.Image = map;
                panel1.BackColor = Color.FromArgb(Convert.ToInt32(txtR.Text)
              , Convert.ToInt32(txtG.Text)
              , Convert.ToInt32(txtB.Text));
            }
        }

        public void MovePictureBoxPosition(int x, int y)
        {
            if (SelectedPixel != null)
            {
                SelectedPixel = new Point(SelectedPixel.X + x, SelectedPixel.Y + y);
            }
        }

        public void OnMouseDownOnPictureBox1(int x, int y)
        {
            FirstState = pictureBox1.Image;
            Selection = true;
            MouseDown.FillRectangle(myBrush, x, y, 1, 1);
            points.Clear();
            points.Add(new Point(x, y));
        }

        public void OnMouseUpOnPictureBox1()
        {
            Brush br = new System.Drawing.SolidBrush(SecondPicBrush);
            Selection = false;
            Point[] pointsArray = new Point[points.Count];
            Bitmap MagicSel = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            for (int i = 0; i < points.Count; i++)
            {
                pointsArray[i] = new Point(points.ElementAt<Point>(i).X / ((pictureBox1.Width + pictureBox2.Width) / pictureBox2.Width),
                    points.ElementAt<Point>(i).Y / ((pictureBox1.Height + pictureBox2.Height) / pictureBox2.Height));
            }


            if (!string.IsNullOrEmpty(txtB.Text)
                && !string.IsNullOrEmpty(txtR.Text)
                && !string.IsNullOrEmpty(txtG.Text)
                && Convert.ToInt32(txtR.Text) >= 0
                && Convert.ToInt32(txtG.Text) >= 0
                && Convert.ToInt32(txtB.Text) >= 0
                && Convert.ToInt32(txtR.Text) <= 255
                && Convert.ToInt32(txtG.Text) <= 255
                && Convert.ToInt32(txtB.Text) <= 255
                )
            {


                foreach (Point p in points)
                {
                    map.SetPixel(p.X, p.Y, Color.FromArgb(Convert.ToInt32(txtR.Text)
                    , Convert.ToInt32(txtG.Text)
                    , Convert.ToInt32(txtB.Text)));
                }

                pictureBox1.Image = map;
                panel1.BackColor = Color.FromArgb(Convert.ToInt32(txtR.Text)
              , Convert.ToInt32(txtG.Text)
              , Convert.ToInt32(txtB.Text));


            }
            else
            {
                pictureBox1.Image = FirstState;
            }


            if (points.Count > 1)
            {
                Graphics gtemp = pictureBox2.CreateGraphics();
                gtemp.DrawLines(new Pen(br, SecondPicBrushWidth), pointsArray);
            }
        }

        public void ResetPictureBox2()
        {
            pictureBox2.Image = null;
        }

        public void OpenColorDialog()
        {
            ColorDialog CD = new ColorDialog();
            CD.ShowDialog();
            Color newC = CD.Color;
            SecondPicBrush = newC;
        }

        public void OnMouseDownOnPictureBox2(int x, int y)
        {
            SelectionSecond = true;
            MouseDownSecond.FillEllipse(myBrush, x, y, 1, 1);
          
        }

        public void OnMouseUpOnPictureBox2()
        {
            SelectionSecond = false;
            
        }

        public void OnMouseMoveOnPictureBox2(int x,int y)
        {
            Brush br = new System.Drawing.SolidBrush(SecondPicBrush);
            if (SelectionSecond)
            {
                MouseDownSecond.FillEllipse(br, x, y, SecondPicBrushWidth, SecondPicBrushWidth);
            }
        }

        public void OpenFormBrushSize()
        {
            FormSize fs = new FormSize();
            fs.Size = 1;
            fs.StartPosition = FormStartPosition.CenterScreen;
            fs.ShowDialog();
            SecondPicBrushWidth = fs.Size;
        }

        public void OpenChangeColorDialog()
        {
            ColorDialog CD = new ColorDialog();
            CD.ShowDialog();
            if (CD.Color != null)
            {
                Color newC = CD.Color;
                pictureBox2.BackColor = newC;
            }
        }

        #endregion

        #region MACHADO_Events

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMove_AddToList(e.X,e.Y);
            OnMouseMove_Draw(e.X, e.Y);           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            OnCheckBoxChanged_ChangeMove();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            OnPictureBox1Click(e.X,e.Y);            
        }        

        private void button1_Click(object sender, EventArgs e)
        {
            LoadImage();            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OnButton2Click_ChangePixelColor();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnListBox1Click_ChangePanel1();          
        }

        private void btnR_Click(object sender, EventArgs e)
        {
            MovePictureBoxPosition(1,0);            
        }

        private void btnL_Click(object sender, EventArgs e)
        {
            MovePictureBoxPosition(-1, 0);               
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            MovePictureBoxPosition(0, -1);   
        }

        private void btnD_Click(object sender, EventArgs e)
        {
            MovePictureBoxPosition(0, 1);   
        }        

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDownOnPictureBox1(e.X,e.Y);
        }
        
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUpOnPictureBox1();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            ResetPictureBox2();
        }        

        private void button4_Click(object sender, EventArgs e)
        {
            OpenColorDialog();
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {           
            OnMouseDownOnPictureBox2(e.X,e.Y);
        }        

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMoveOnPictureBox2(e.X,e.Y);
        }       

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUpOnPictureBox2(); 
        }         

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFormBrushSize();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Bitmap newBm = new Bitmap(pictureBox2.Width,pictureBox2.Height);
            pictureBox2.Image = newBm;
        }
        
        private void button7_Click_1(object sender, EventArgs e)
        {
            OpenChangeColorDialog();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            SaveImage();
        }

        #endregion

        #region MACHADO_ApplyFilters

        private void button8_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Origin;
            pictureBox1.Image = ImageFilters.DivideCrop(new Bitmap(pictureBox1.Image));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Origin;
            pictureBox1.Image = ImageFilters.ApplyFilter(new Bitmap(pictureBox1.Image), 1, 10, 1, 1);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Origin;
            pictureBox1.Image = ImageFilters.ApplyFilter(new Bitmap(pictureBox1.Image), 1, 1, 10, 1);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Origin;
            pictureBox1.Image = ImageFilters.ApplyFilter(new Bitmap(pictureBox1.Image), 1, 1, 1, 25);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = ImageFilters.ApplyFilter(new Bitmap(pictureBox1.Image), 1, 1, 10, 15);
        }

        private void button13_Click(object sender, EventArgs e)
        {

            pictureBox1.Image = Origin;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Origin;
            pictureBox1.Image = ImageFilters.BlackWhite(new Bitmap(pictureBox1.Image));
        }

        private void button15_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Origin;
            pictureBox1.Image = ImageFilters.ApplyFilterSwap(new Bitmap(pictureBox1.Image));
        }

        private void button17_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Origin;
            System.Drawing.Image te = ImageFilters.ApplyFilterSwapDivide(new Bitmap(pictureBox1.Image), 1, 1, 2, 1);
            pictureBox1.Image = ImageFilters.ApplyFilterSwap(new Bitmap(te));
        }

        private void button18_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Origin;
            Color c = Color.Green;
            pictureBox1.Image = ImageFilters.ApplyFilterMega(new Bitmap(pictureBox1.Image), 230, 110, c);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Origin;
            Color c = Color.Blue;
            pictureBox1.Image = ImageFilters.ApplyFilterMega(new Bitmap(pictureBox1.Image), 230, 110, c);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Origin;
            Color c = Color.Orange;
            pictureBox1.Image = ImageFilters.ApplyFilterMega(new Bitmap(pictureBox1.Image), 230, 110, c);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Color c = Color.Pink;
            pictureBox1.Image = ImageFilters.ApplyFilterMega(new Bitmap(pictureBox1.Image), 230, 110, c);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Origin;
            pictureBox1.Image = ImageFilters.ApplyFilterMega(new Bitmap(pictureBox1.Image), 230, 110, SecondPicBrush);
        }

        private void button19_Click_1(object sender, EventArgs e)
        {
            pictureBox1.Image = Origin;
            pictureBox1.Image = ImageFilters.RainbowFilter(new Bitmap(pictureBox1.Image));
        }

        #endregion

        

        


        
    }
}
