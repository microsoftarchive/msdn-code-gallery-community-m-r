using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace com.techphernalia.windows.forms.techieUI
{
    public partial class frmMain : Form
    {
        public string PrimaryLanguage { get; set; }
        private frmOutput Out = new frmOutput();
        private TreeData _current;
        public frmMain(string title,List<TreeDataList> items,string primaryLanguage)
        {
            InitializeComponent();
            this.PrimaryLanguage = primaryLanguage;
            this.Text = title + " : By Durgesh Chaudhary for techPhernalia.com";

            /* Create Primary Node */
            TreeNode rootNode = new TreeNode { Text = title, Tag = null , ImageKey = ImageKey.TOP , SelectedImageKey = ImageKey.TOP };
            treeResources.Nodes.Add(rootNode);

            /* Loop for each Primary Node */
            foreach (TreeDataList item in items)
            {
                int sum = 0;
                TreeNode node = new TreeNode { Text = item.Title , Tag = null , ImageKey = ImageKey.TOPIC , SelectedImageKey = ImageKey.TOPIC };
                TreeNode CategoryNode = null;
                string category = string.Empty;

                /* Get all elements order by category and method name in asc */
                var xxx = from x in item orderby x.Category ascending , x.Method.Name ascending select x;
                foreach (TreeData data in xxx)
                {
                    sum++;
                    /* Set category if not correct */
                    if (!data.Category.ToLower().Equals(category) || string.IsNullOrEmpty(category))
                    {
                        category = data.Category.ToLower();
                        TreeNode[] t = node.Nodes.Find(category, false);
                        if (t.Length > 0)
                            CategoryNode = t[0];
                        else
                        {
                            /* Category not already added, Add one */
                            CategoryNode = node.Nodes.Add(category, data.Category, ImageKey.CLOSE, ImageKey.CLOSE);
                        }
                    }

                    /* Add final node to category */
                    TreeNode dataNode = new TreeNode { Text = data.Title, Tag = data, ImageKey = ImageKey.CLOSE, SelectedImageKey = ImageKey.CLOSE };
                    CategoryNode.Nodes.Add(dataNode);

                }
                /* Expand Category node */
                node.Text += " (" + sum + ")";
                node.Expand();
                treeResources.Nodes.Add(node);
            }
        }
        private void treeResources_AfterSelect(object sender, TreeViewEventArgs e)
        {
            txtDescription.Text = "";
            txtCode.Text = "";
            btnRun.Enabled = false;
            cmbLanguage.Enabled = false;

            /* Init */
            _current = ((TreeData)e.Node.Tag);
            if (_current != null)
            {
                /* Description */
                txtDescription.Text = _current.Description;

                /* Run Button */
                if (_current.Method != null)
                    btnRun.Enabled = true;
                
                /* Code TextBox and Drop Down */
                if (_current.Code != null)
                {
                    if (_current.Code.Count > 0)
                    {
                        cmbLanguage.Enabled = true;
                        cmbLanguage.DataSource = _current.Code;
                        var x = from c in _current.Code where c.Lang.ToLower().Equals(PrimaryLanguage.ToLower()) select c;
                        if (x.Count() > 0)
                            cmbLanguage.SelectedItem = x.First();
                        else
                            cmbLanguage.SelectedIndex = 0;
                        UpdateCode();
                    }
                }
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (_current != null)
            {
                Out.T.Text = "";
                StreamWriter writer = _current.ParentList.ConsoleWriter;
                TextWriter oldConsoleOut = Console.Out;
                Console.SetOut(writer);
                MemoryStream stream = (MemoryStream)writer.BaseStream;
                stream.SetLength(0);
                _current.Execute();
                writer.Flush();
                Console.SetOut(oldConsoleOut);
                Out.T.Text += writer.Encoding.GetString(stream.ToArray());

                Out.ShowDialog();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
        }
        private void UpdateCode()
        {
            txtCode.Text = ((CodeBlock)cmbLanguage.SelectedItem).Code;
        }
        private void treeResources_AfterExpand(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Level)
            {
                case 1:
                case 2:
                    e.Node.ImageKey = ImageKey.OPEN;
                    e.Node.SelectedImageKey = ImageKey.OPEN;
                    break;
            }
        }

        private void treeResources_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Level)
            {
                case 1:
                case 2:
                    e.Node.ImageKey = ImageKey.CLOSE;
                    e.Node.SelectedImageKey = ImageKey.CLOSE;
                    break;
            }
        }

        private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCode();
        }
    }
    public static class ImageKey
    {
        public static string OPEN = "Open";
        public static string CLOSE = "Closed";
        public static string TOPIC = "Topic";
        public static string TOP = "Top";
    }
}
