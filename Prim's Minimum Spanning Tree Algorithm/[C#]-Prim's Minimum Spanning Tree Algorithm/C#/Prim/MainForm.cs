using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Prim
{
    public partial class MainForm : Form
    {
        private const int PAINTED = 0, UNPAINTED = 1;
        private int index, n, x, y, x1, x2, y1, y2;
        private int[] u, v;
        private Color[] color;
        private List<Node> graph;
        private PriorityQueue queue;

        public MainForm()
        {
            InitializeComponent();
            CreateGraph();
            index = -1;
            panel1.Paint += new PaintEventHandler(panel1_Paint);
        }

        private void CreateGraph()
        {
            graph = new List<Node>();
            Node a = new Node(0, 0);
            Node b = new Node(1, Node.INFINITY);
            Node c = new Node(2, Node.INFINITY);
            Node d = new Node(3, Node.INFINITY);
            Node e = new Node(4, Node.INFINITY);
            Node f = new Node(5, Node.INFINITY);
            Node g = new Node(6, Node.INFINITY);
            Node h = new Node(7, Node.INFINITY);
            Node i = new Node(8, Node.INFINITY);

            List<Node> adjA = a.Adjacency;
            List<Node> adjB = b.Adjacency;
            List<Node> adjC = c.Adjacency;
            List<Node> adjD = d.Adjacency;
            List<Node> adjE = e.Adjacency;
            List<Node> adjF = f.Adjacency;
            List<Node> adjG = g.Adjacency;
            List<Node> adjH = h.Adjacency;
            List<Node> adjI = i.Adjacency;

            List<int> weightA = a.Weight;
            List<int> weightB = b.Weight;
            List<int> weightC = c.Weight;
            List<int> weightD = d.Weight;
            List<int> weightE = e.Weight;
            List<int> weightF = f.Weight;
            List<int> weightG = g.Weight;
            List<int> weightH = h.Weight;
            List<int> weightI = i.Weight;

            adjA.Add(b);
            adjA.Add(h);
            weightA.Add(4);
            weightA.Add(8);
            adjB.Add(a);
            adjB.Add(c);
            adjB.Add(h);
            weightB.Add(4);
            weightB.Add(8);
            weightB.Add(11);
            adjC.Add(b);
            adjC.Add(d);
            adjC.Add(f);
            adjC.Add(i);
            weightC.Add(8);
            weightC.Add(7);
            weightC.Add(4);
            weightC.Add(2);
            adjD.Add(c);
            adjD.Add(e);
            adjD.Add(f);
            weightD.Add(7);
            weightD.Add(9);
            weightD.Add(14);
            adjE.Add(d);
            adjE.Add(f);
            weightE.Add(9);
            weightE.Add(10);
            adjF.Add(c);
            adjF.Add(d);
            adjF.Add(e);
            adjF.Add(g);
            weightF.Add(4);
            weightF.Add(14);
            weightF.Add(10);
            weightF.Add(2);
            adjG.Add(f);
            adjG.Add(h);
            adjG.Add(i);
            weightG.Add(2);
            weightG.Add(1);
            weightG.Add(6);
            adjH.Add(a);
            adjH.Add(b);
            adjH.Add(g);
            adjH.Add(i);
            weightH.Add(8);
            weightH.Add(11);
            weightH.Add(1);
            weightH.Add(7);
            adjI.Add(c);
            adjI.Add(g);
            adjI.Add(h);
            weightI.Add(2);
            weightI.Add(6);
            weightI.Add(7);
            graph.Add(a);
            graph.Add(b);
            graph.Add(c);
            graph.Add(d);
            graph.Add(e);
            graph.Add(f);
            graph.Add(g);
            graph.Add(h);
            graph.Add(i);
            n = graph.Count;
            color = new Color[n];

            for (int m = 0; m < n; m++)
                color[m] = Color.Blue;

            u = new int[n];
            v = new int[n];

            MST();
        }

        void MST()
        {
            bool[] output = new bool[n];
            int[] pi = new int[n];
            List<Node> copy = new List<Node>();

            for (int i = 0; i < graph.Count; i++)
                copy.Add(graph[i]);

            queue = new PriorityQueue(copy);
            queue.buildHeap();

            for (int i = 0; i < queue.NodeList.Count; i++)
            {
                Node node = queue.extractMin();

                output[node.Id] = true;
                for (int j = 0; j < node.Adjacency.Count; j++)
                {
                    Node next = node.Adjacency[j];
                    int weight = node.Weight[j];

                    if (!output[next.Id] && weight < next.Key)
                    {
                        pi[next.Id] = node.Id;
                        node.Adjacency[j].Key = weight;
                    }
                }
            }

            pi[0] = -1;

            for (int i = 0; i < n; i++)
            {
                u[i] = i;
                v[i] = pi[i];
            }

            // reorder the edges in the minimum spanning tree
            
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    Node nodeI = graph[u[i]];
                    Node nodeJ = graph[u[j]];

                    if (v[i] >= v[j] && nodeI.Key > nodeJ.Key)
                    {
                        int t = u[i];

                        u[i] = u[j];
                        u[j] = t;
                        t = v[i];
                        v[i] = v[j];
                        v[j] = t;
                    }
                }
            }
        }

        private void calculateXY(int id)
        {
            int Width = panel1.Width;
            int Height = panel1.Height;

            x = Width / 2 + (int)(Width * Math.Cos(2 * id * Math.PI / n) / 4.0);
            y = Height / 2 + (int)(Width * Math.Sin(2 * id * Math.PI / n) / 4.0);
        }

        private void draw(Graphics g)
        {
            if (index != -1)
            {
                int Width = panel1.Width;
                int Height = panel1.Height;
                Font font = new Font("Courier New", 12f, FontStyle.Bold);
                List<Node> nodeList = queue.NodeList;
                Pen pen = new Pen(Color.Black);
                SolidBrush textBrush = new SolidBrush(Color.White);

                for (int i = 0; i <= index; i++)
                {
                    if (v[i] != -1)
                    {
                        calculateXY(u[i]);
                        x1 = x + (Width / 2) / n / 2;
                        y1 = y + (Width / 2) / n / 2;
                        calculateXY(v[i]);
                        x2 = x + (Width / 2) / n / 2;
                        y2 = y + (Width / 2) / n / 2;
                        g.DrawLine(pen, x1, y1, x2, y2);
                    }

                    SolidBrush brush = new SolidBrush(color[u[index]]);

                    char[] c = new char[1];

                    c[0] = (char)('a' + u[i]);

                    string str = new string(c);

                    calculateXY(u[i]);
                    g.FillEllipse(brush, x, y, (Width / 2) / n, (Width / 2) / n);
                    g.DrawString(str, font,
                        textBrush, (float)(x + (Width / 2) / n / 2) - 12f,
                        (float)(y + (Width / 2) / n / 2) - 12f);

                    if (v[i] != -1)
                    {
                        c = new char[1];
                        c[0] = (char)('a' + v[i]);
                        str = new string(c);
                        calculateXY(v[i]);
                        g.FillEllipse(brush, x, y, (Width / 2) / n, (Width / 2) / n);
                        g.DrawString(str, font,
                            textBrush, (float)(x + (Width / 2) / n / 2) - 12f,
                            (float)(y + (Width / 2) / n / 2) - 12f);
                    }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs pea)
        {
            draw(pea.Graphics);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            index++;

            if (index < n)
                panel1.Invalidate();

            else
                index = -1;
        }
    }
}