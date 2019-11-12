using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPrint
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            lblPrintingStatus.Visible = true;

            if (!string.IsNullOrEmpty(txtPrinterName.Text) && !string.IsNullOrEmpty(txtToPrint.Text))
            {
                ShowPrintingStatus(new PrinterHandler().DoPrintDocument(txtPrinterName.Text, txtTray.Text, txtToPrint.Text));                 
            }
            else
                ShowPrintingStatus(false); 
        }
        void ShowPrintingStatus(bool isPrintingComplete)
        {
            lblPrintingStatus.Visible = true;
            if (!isPrintingComplete)
            {
                lblPrintingStatus.ForeColor = System.Drawing.Color.Red;
                lblPrintingStatus.Text = "Error occured.";
            }
            else
            {
                lblPrintingStatus.ForeColor = System.Drawing.Color.Green;
                lblPrintingStatus.Text = "Printing complete.";
            }
        }
    }
}