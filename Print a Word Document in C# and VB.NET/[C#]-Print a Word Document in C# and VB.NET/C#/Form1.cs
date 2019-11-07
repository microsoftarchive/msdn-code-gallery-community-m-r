using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

namespace Print
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Create word document
            Document document = new Document();

            Section section = document.AddSection();
            section.PageSetup.PageSize = PageSize.A4;
            section.PageSetup.Margins.Top = 72f;
            section.PageSetup.Margins.Bottom = 72f;
            section.PageSetup.Margins.Left = 89.85f;
            section.PageSetup.Margins.Right = 89.85f;

            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
            paragraph.AppendPicture(Print.Properties.Resources.Word);

            String p1
                = "Microsoft Word is a word processor designed by Microsoft. "
                + "It was first released in 1983 under the name Multi-Tool Word for Xenix systems. "
                + "Subsequent versions were later written for several other platforms including "
                + "IBM PCs running DOS (1983), the Apple Macintosh (1984), the AT&T Unix PC (1985), "
                + "Atari ST (1986), SCO UNIX, OS/2, and Microsoft Windows (1989). ";
            String p2
                = "Microsoft Office Word instead of merely Microsoft Word. "
                + "The 2010 version appears to be branded as Microsoft Word, "
                + "once again. The current versions are Microsoft Word 2010 for Windows and 2008 for Mac.";
            section.AddParagraph().AppendText(p1).CharacterFormat.FontSize = 14;
            section.AddParagraph().AppendText(p2).CharacterFormat.FontSize = 14;

            //Print doc file.
            PrintDialog dialog = new PrintDialog();
            dialog.AllowCurrentPage = true;
            dialog.AllowSomePages = true;
            dialog.UseEXDialog = true;
            try
            {
                document.PrintDialog = dialog;
                dialog.Document = document.PrintDocument;
                dialog.Document.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
