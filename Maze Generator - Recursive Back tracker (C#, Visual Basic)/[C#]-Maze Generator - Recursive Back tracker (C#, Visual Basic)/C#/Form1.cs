using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Collections;
using System.Diagnostics;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            doLayout();
        }
        public void doLayout()
        {
            Panel2.Top = 100;
            Panel2.Left = 0;
            Panel2.Height = this.ClientRectangle.Height - Panel2.Top;
            Panel2.Width = this.ClientRectangle.Width;
            Panel2.BorderStyle = BorderStyle.FixedSingle;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            Maze maze = new Maze(Convert.ToInt32(nudRows.Value), Convert.ToInt32(nudCols.Value), Convert.ToInt32(nudWidth.Value), Convert.ToInt32(nudHeight.Value));
            maze.MazeComplete += (Image m) =>{
                Panel1.BackgroundImage = m;
                Panel1.BackgroundImageLayout = ImageLayout.None;
                Panel1.Width = m.Width;
                Panel1.Height = m.Height;
                //maze.PrintMaze()
            };
            maze.Generate();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            doLayout();
        }
    }
}

public class Maze : Control
{
    int Rows;
    int Columns;
    int cellWidth;
    int cellHeight;
    Dictionary<string, Cell> cells = new Dictionary<string, Cell>();
    Stack<Cell> stack = new Stack<Cell>();

    public Image MazeImage;
    public event MazeCompleteEventHandler MazeComplete;
    public delegate void MazeCompleteEventHandler(Image Maze);
    private event CallCompleteEventHandler CallComplete;
    private delegate void CallCompleteEventHandler(Image Maze);
    public new Rectangle Bounds
    {
        get
        {
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            return rect;
        }
    }
    private System.Drawing.Printing.PrintDocument withEventsField_printDoc = new System.Drawing.Printing.PrintDocument();
    public System.Drawing.Printing.PrintDocument printDoc
    {
        get { return withEventsField_printDoc; }
        set
        {
            if (withEventsField_printDoc != null)
            {
                withEventsField_printDoc.PrintPage -= PrintImage;
            }
            withEventsField_printDoc = value;
            if (withEventsField_printDoc != null)
            {
                withEventsField_printDoc.PrintPage += PrintImage;
            }
        }
    }
    private void PrintImage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
        List<string> nonprinters = new string[] { "Send To OneNote 2013", "PDFCreator", "PDF Architect 4", "Microsoft XPS Document Writer", "Microsoft Print to PDF", "Fax", "-" }.ToList();
        string printerName = "none";
        foreach (string a in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
        {
            if (nonprinters.IndexOf(a) > -1)
                continue;
            printerName = a;
        }
        if (printerName == "none")
            return;
        printDoc.PrinterSettings.PrinterName = printerName;
        int imageLeft = Convert.ToInt32(e.PageBounds.Width / 2) - Convert.ToInt32(MazeImage.Width / 2);
        int imageTop = Convert.ToInt32(e.PageBounds.Height / 2) - Convert.ToInt32(MazeImage.Height / 2);
        e.Graphics.DrawImage(MazeImage, imageLeft, imageTop);
    }
    public void PrintMaze()
    {
        printDoc.Print();
    }
    public void Generate()
    {
        int c = 0;
        int r = 0;
        for (int y = 0; y <= Height; y += cellHeight)
        {
            for (int x = 0; x <= Width; x += cellWidth)
            {
                Cell cell = new Cell(new Point(x, y), new Size(cellWidth, cellHeight),ref cells, r, c, (Rows - 1), (Columns - 1));
                c += 1;
            }
            c = 0;
            r += 1;
        }
        System.Threading.Thread thread = new System.Threading.Thread(Dig);
        thread.Start();
    }
    private void Dig()
    {
        int r = 0;
        int c = 0;
        string key = "c" + 5 + "r" + 5;
        Cell startCell = cells[key];
        stack.Clear();
        startCell.Visited = true;
        while ((startCell != null))
        {
            startCell = startCell.Dig(ref stack);
            if (startCell != null)
            {
                startCell.Visited = true;
                startCell.Pen = Pens.Black;
            }
        }
        stack.Clear();
        Bitmap Maze = new Bitmap(Width, Height);
        using (Graphics g = Graphics.FromImage(Maze))
        {
            g.Clear(Color.White);
            if (cells.Count > 0)
            {
                for (r = 0; r <= this.Rows - 1; r++)
                {
                    for (c = 0; c <= this.Columns - 1; c++)
                    {
                        Cell cell = cells["c" + c + "r" + r];
                        cell.draw(g);
                    }
                }
            }
        }
        this.MazeImage = Maze;

        if (CallComplete != null)
        {
            CallComplete(Maze);
        }
    }
    public delegate void dComplete(Image maze);
    private void Call_Complete(Image maze)
    {
        if (this.InvokeRequired)
        {
            this.Invoke(new dComplete(Call_Complete), maze);
        }
        else
        {
            if (MazeComplete != null)
            {
                MazeComplete(maze);
            }
        }
    }
    public Maze(int rows, int columns, int cellWidth, int cellHeight)
    {
        CallComplete += Call_Complete;
        this.Rows = rows;
        this.Columns = columns;
        this.cellWidth = cellWidth;
        this.cellHeight = cellHeight;
        this.Width = (this.Columns * this.cellWidth) + 1;
        this.Height = (this.Rows * this.cellHeight) + 1;
        this.CreateHandle();
    }
}
public class Cell
{
    public bool NorthWall = true;
    public bool SouthWall = true;
    public bool WestWall = true;
    public bool EastWall = true;
    public string id;
    public Pen Pen = Pens.Black;
    public Rectangle Bounds;
    public Dictionary<string, Cell> Cells;
    public int Column;
    public int Row;
    public string NeighborNorthID;
    public string NeighborSouthID;
    public string NeighborEastID;
    public string NeighborWestID;
    public bool Visited = false;
    public Stack<Cell> Stack;
    public void draw(Graphics g)
    {
        if (NorthWall) g.DrawLine(Pen, new Point(Bounds.Left, Bounds.Top), new Point(Bounds.Right, Bounds.Top));
        if (SouthWall)g.DrawLine(Pen, new Point(Bounds.Left, Bounds.Bottom), new Point(Bounds.Right, Bounds.Bottom));
        if (WestWall)g.DrawLine(Pen, new Point(Bounds.Left, Bounds.Top), new Point(Bounds.Left, Bounds.Bottom));
        if (EastWall)g.DrawLine(Pen, new Point(Bounds.Right, Bounds.Top), new Point(Bounds.Right, Bounds.Bottom));
    }
    public Cell(Point location, Size size, ref Dictionary<string, Cell> cellList, int r, int c, int maxR, int maxC)
    {
        this.Bounds = new Rectangle(location, size);
        this.Column = c;
        this.Row = r;
        this.id = "c" + c + "r" + r;
        int rowNort = r - 1;
        int rowSout = r + 1;
        int colEast = c + 1;
        int colWest = c - 1;
        NeighborNorthID = "c" + c + "r" + rowNort;
        NeighborSouthID = "c" + c + "r" + rowSout;
        NeighborEastID = "c" + colEast + "r" + r;
        NeighborWestID = "c" + colWest + "r" + r;
        if (rowNort < 0) NeighborNorthID = "none";
        if (rowSout > maxR) NeighborSouthID = "none";
        if (colEast > maxC) NeighborEastID = "none";
        if (colWest < 0) NeighborWestID = "none";
        this.Cells = cellList;
        this.Cells.Add(this.id, this);
    }
    public Cell getNeighbor()
    {
        List<Cell> c = new List<Cell>();
        if (!(NeighborNorthID == "none") && Cells[NeighborNorthID].Visited == false)c.Add(Cells[NeighborNorthID]);
        if (!(NeighborSouthID == "none") && Cells[NeighborSouthID].Visited == false)c.Add(Cells[NeighborSouthID]);
        if (!(NeighborEastID == "none") && Cells[NeighborEastID].Visited == false)c.Add(Cells[NeighborEastID]);
        if (!(NeighborWestID == "none") && Cells[NeighborWestID].Visited == false)c.Add(Cells[NeighborWestID]);
        int max = c.Count;
        Cell currentCell = null;
        if (c.Count > 0)
        {
            Microsoft.VisualBasic.VBMath.Randomize();
            int index = Convert.ToInt32(Conversion.Int(c.Count * VBMath.Rnd()));
            currentCell = c[index];
        }
        return currentCell;
    }
    public Cell Dig(ref Stack<Cell> stack)
    {
        this.Stack = stack;
        Cell nextCell = getNeighbor();
        if ((nextCell != null))
        {
            stack.Push(nextCell);
            if (nextCell.id == this.NeighborNorthID)
            {
                this.NorthWall = false;
                nextCell.SouthWall = false;
            }
            else if (nextCell.id == this.NeighborSouthID)
            {
                this.SouthWall = false;
                nextCell.NorthWall = false;
            }
            else if (nextCell.id == this.NeighborEastID)
            {
                this.EastWall = false;
                nextCell.WestWall = false;
            }
            else if (nextCell.id == this.NeighborWestID)
            {
                this.WestWall = false;
                nextCell.EastWall = false;
            }
        }
        else if (!(stack.Count == 0))
        {
            nextCell = stack.Pop();
        }
        else
        {
            return null;
        }
        return nextCell;
    }
}