using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;

namespace ImageFunctions.Forms
{
    public partial class FrmModificationType : DockContent
    {

        public delegate void SusanSelectedHandler();
        public event SusanSelectedHandler SusanSelected;
        public delegate void MoravecSelectedHandler();
        public event MoravecSelectedHandler MoravecSelected;
        public delegate void HarrisSelectedHandler();
        public event HarrisSelectedHandler HarrisSelected;
        public delegate void FASTSelectedHandler();
        public event FASTSelectedHandler FASTSelected;

        private enum Modifications : int
        {
            None,
            CornerDetection,
        };

        private enum Methods
        {
            None,
            Susan,
            Moravec,
            Harris,
            FAST
        };


        public void EnableModifications()
        {
            lbModification.Enabled = true;
        }

        public void DisableModifications()
        {
            lbModification.Enabled = false;
        }

        public FrmModificationType()
        {
            InitializeComponent();
            lbModification.Items.Add("Corner Detection");
        }

        protected override string GetPersistString()
        {
            return this.Text;

        }

        private void lbModification_SelectedItemChanged(object sender, EventArgs e)
        {
            lbMethods.Enabled = true;
            switch (lbModification.SelectedIndex)
            {
                case (int)Modifications.None:
                    break;
                case (int)Modifications.CornerDetection:
                    PopulateMethods(Modifications.CornerDetection);
                    break;
                default:
                    break;
            }
        }

        //// Populate the Modification Method list box.
        private void PopulateMethods(Modifications modifications)
        {
            switch (modifications)
            {
                case Modifications.None:
                    lbMethods.Enabled = false;
                    break;
                case Modifications.CornerDetection:
                    lbMethods.Items.Add("Susan Corner Detection"); // index 1
                    lbMethods.Items.Add("Moravec Corner Detection"); // index 2
                    lbMethods.Items.Add("Harris Corner Detection");
                    lbMethods.Items.Add("FAST Corner Detection");
                    break;
                default:
                    lbMethods.Enabled = false;
                    break;
            }
        }

        private void lbMethods_SelectedItemChanged(object sender, EventArgs e)
        {
            switch (lbMethods.SelectedIndex)
            {
                case (int)Methods.None:
                    break;
                case (int)Methods.Susan:
                    SusanSelected();
                    break;
                case (int)Methods.Moravec:
                    MoravecSelected();
                    break;
                case (int)Methods.Harris:
                    HarrisSelected();
                    break;
                case (int)Methods.FAST:
                    FASTSelected();
                    break;
                default:
                    break;
            }
        }
    }
}
