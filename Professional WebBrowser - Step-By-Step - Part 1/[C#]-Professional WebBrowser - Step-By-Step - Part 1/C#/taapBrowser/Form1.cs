using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using taapBrowser.Forms;

namespace taapBrowser
{
    public partial class Form1 : Form
    {
        FrmBrowser browser;

        public Form1()
        {
            InitializeComponent();
            browser = new FrmBrowser();
            browser.Show(dockPanel);

        }
    }
}
