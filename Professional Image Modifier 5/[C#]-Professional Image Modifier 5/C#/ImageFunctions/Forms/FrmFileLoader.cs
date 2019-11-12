using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageFunctions.Controls;
using WeifenLuo.WinFormsUI.Docking;

namespace ImageFunctions.Forms
{
    public partial class FrmFileLoader : DockContent
    {
        public delegate void ThumbnailSelectedHandler(string PathToMedia);
        public event ThumbnailSelectedHandler ThumbnailSelected;


        public FrmFileLoader()
        {
            InitializeComponent();
        }

        protected override string GetPersistString()
        {
            return this.Text;
        }

        internal void Add(ThumbnailControl thumbnail)
        {
            thumbnail.ThumbnailSelected += thumbnail_ThumbnailSelected;
            ThumbnailLayoutPanel.Controls.Add(thumbnail);

        }

        void thumbnail_ThumbnailSelected(string PathToMedia)
        {
            ThumbnailSelected(PathToMedia);
        }



    }
}
