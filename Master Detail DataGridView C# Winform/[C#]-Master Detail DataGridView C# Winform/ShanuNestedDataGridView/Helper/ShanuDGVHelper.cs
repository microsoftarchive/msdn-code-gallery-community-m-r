using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

/// <summary>
/// Author      : Shanu
/// Create date : 2014-11-11
/// Description : ShanuDGVHelper
/// Latest
/// Modifier    : Shanu
/// Modify date : 2014-11-11
/// </summary>
namespace ShanuNestedDataGridView.Helper
{
    class ShanuDGVHelper
    {
        #region Variables
        public DataGridView MasterDGVs = new DataGridView();
        public DataGridView DetailDGVs = new DataGridView();
        List<int> listcolumnIndex;
        static String ImageName = "toggle.png";
        String FilterColumnName = "";
        DataTable DetailgridDT;
        int gridColumnIndex = 0;
       

        DateTimePicker shanuDateTimePicker = new DateTimePicker();
        DataGridView shanuNestedDGV = new DataGridView();
        String EventFucntions;
        # endregion

        //Set all the telerik Grid layout
        #region Layout

        public static void Layouts(DataGridView ShanuDGV, Color BackgroundColor, Color RowsBackColor, Color AlternatebackColor, Boolean AutoGenerateColumns, Color HeaderColor, Boolean HeaderVisual, Boolean RowHeadersVisible, Boolean AllowUserToAddRows, Color HeaderForeColor, int headerHeight)
        {
            //Grid Back ground Color
            ShanuDGV.BackgroundColor = BackgroundColor;

            //Grid Back Color
            ShanuDGV.RowsDefaultCellStyle.BackColor = RowsBackColor;

            //GridColumnStylesCollection Alternate Rows Backcolr
            ShanuDGV.AlternatingRowsDefaultCellStyle.BackColor = AlternatebackColor;

            // Auto generated here set to tru or false.
            ShanuDGV.AutoGenerateColumns = AutoGenerateColumns;
            //  ShanuDGV.DefaultCellStyle.Font = new Font("Verdana", 10.25f, FontStyle.Regular);
            // ShanuDGV.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 11, FontStyle.Regular);

            //Column Header back Color
            ShanuDGV.ColumnHeadersDefaultCellStyle.BackColor = HeaderColor;
            ShanuDGV.ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColor;
            ShanuDGV.ColumnHeadersHeight = headerHeight;
            //header Visisble
            ShanuDGV.EnableHeadersVisualStyles = HeaderVisual;

            // Enable the row header
            ShanuDGV.RowHeadersVisible = RowHeadersVisible;

            // to Hide the Last Empty row here we use false.
            ShanuDGV.AllowUserToAddRows = AllowUserToAddRows;
        }
        #endregion

        //Add your grid to your selected Control and set height,width,position of your grid.
        #region Variables
        public static void Generategrid(DataGridView ShanuDGV, Control cntrlName, int width, int height, int xval, int yval)
        {
            ShanuDGV.Location = new Point(xval, yval);
            ShanuDGV.Size = new Size(width, height);
            //ShanuDGV.Dock = docktyope.
            cntrlName.Controls.Add(ShanuDGV);
        }
        #endregion

        //Template Column In this column we can add Textbox,Lable,Check Box,Dropdown box and etc
        #region Templatecolumn
        public static void Templatecolumn(DataGridView ShanuDGV, ShanuControlTypes ShanuControlTypes, String cntrlnames, String Headertext, String ToolTipText, Boolean Visible, int width, DataGridViewTriState Resizable, DataGridViewContentAlignment cellAlignment, DataGridViewContentAlignment headerAlignment, Color CellTemplateBackColor, DataTable dtsource, String DisplayMember, String ValueMember, Color CellTemplateforeColor)
        {
            switch (ShanuControlTypes)
            {
                case ShanuControlTypes.CheckBox:
                    DataGridViewCheckBoxColumn dgvChk = new DataGridViewCheckBoxColumn();
                    dgvChk.ValueType = typeof(bool);
                    dgvChk.Name = cntrlnames;

                    dgvChk.HeaderText = Headertext;
                    dgvChk.ToolTipText = ToolTipText;
                    dgvChk.Visible = Visible;
                    dgvChk.Width = width;
                    dgvChk.SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvChk.Resizable = Resizable;
                    dgvChk.DefaultCellStyle.Alignment = cellAlignment;
                    dgvChk.HeaderCell.Style.Alignment = headerAlignment;
                    if (CellTemplateBackColor.Name.ToString() != "Transparent")
                    {
                        dgvChk.CellTemplate.Style.BackColor = CellTemplateBackColor;
                    }
                    dgvChk.DefaultCellStyle.ForeColor = CellTemplateforeColor;
                    ShanuDGV.Columns.Add(dgvChk);
                    break;
                case ShanuControlTypes.BoundColumn:
                    DataGridViewColumn dgvbound = new DataGridViewTextBoxColumn();
                    dgvbound.DataPropertyName = cntrlnames;
                    dgvbound.Name = cntrlnames;
                    dgvbound.HeaderText = Headertext;
                    dgvbound.ToolTipText = ToolTipText;
                    dgvbound.Visible = Visible;
                    dgvbound.Width = width;
                    dgvbound.SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvbound.Resizable = Resizable;
                    dgvbound.DefaultCellStyle.Alignment = cellAlignment;
                    dgvbound.HeaderCell.Style.Alignment = headerAlignment;
                    dgvbound.ReadOnly = true;
                    if (CellTemplateBackColor.Name.ToString() != "Transparent")
                    {
                        dgvbound.CellTemplate.Style.BackColor = CellTemplateBackColor;
                    }
                    dgvbound.DefaultCellStyle.ForeColor = CellTemplateforeColor;
                    ShanuDGV.Columns.Add(dgvbound);
                    break;
                case ShanuControlTypes.TextBox:
                    DataGridViewTextBoxColumn dgvText = new DataGridViewTextBoxColumn();
                    dgvText.ValueType = typeof(decimal);
                    dgvText.DataPropertyName = cntrlnames;
                    dgvText.Name = cntrlnames;
                    dgvText.HeaderText = Headertext;
                    dgvText.ToolTipText = ToolTipText;
                    dgvText.Visible = Visible;
                    dgvText.Width = width;
                    dgvText.SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvText.Resizable = Resizable;
                    dgvText.DefaultCellStyle.Alignment = cellAlignment;
                    dgvText.HeaderCell.Style.Alignment = headerAlignment;
                    if (CellTemplateBackColor.Name.ToString() != "Transparent")
                    {
                        dgvText.CellTemplate.Style.BackColor = CellTemplateBackColor;
                    }
                    dgvText.DefaultCellStyle.ForeColor = CellTemplateforeColor;
                    ShanuDGV.Columns.Add(dgvText);
                    break;
                case ShanuControlTypes.ComboBox:
                    DataGridViewComboBoxColumn dgvcombo = new DataGridViewComboBoxColumn();
                    dgvcombo.ValueType = typeof(decimal);
                    dgvcombo.Name = cntrlnames;
                    dgvcombo.DataSource = dtsource;
                    dgvcombo.DisplayMember = DisplayMember;
                    dgvcombo.ValueMember = ValueMember;
                    dgvcombo.Visible = Visible;
                    dgvcombo.Width = width;
                    dgvcombo.SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvcombo.Resizable = Resizable;
                    dgvcombo.DefaultCellStyle.Alignment = cellAlignment;
                    dgvcombo.HeaderCell.Style.Alignment = headerAlignment;
                    if (CellTemplateBackColor.Name.ToString() != "Transparent")
                    {
                        dgvcombo.CellTemplate.Style.BackColor = CellTemplateBackColor;

                    }
                    dgvcombo.DefaultCellStyle.ForeColor = CellTemplateforeColor;
                    ShanuDGV.Columns.Add(dgvcombo);
                    break;

                case ShanuControlTypes.Button:
                    DataGridViewButtonColumn dgvButtons = new DataGridViewButtonColumn();
                    dgvButtons.Name = cntrlnames;
                    dgvButtons.FlatStyle = FlatStyle.Popup;
                    dgvButtons.DataPropertyName = cntrlnames;
                    dgvButtons.Visible = Visible;
                    dgvButtons.Width = width;
                    dgvButtons.SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvButtons.Resizable = Resizable;
                    dgvButtons.DefaultCellStyle.Alignment = cellAlignment;
                    dgvButtons.HeaderCell.Style.Alignment = headerAlignment;
                    if (CellTemplateBackColor.Name.ToString() != "Transparent")
                    {
                        dgvButtons.CellTemplate.Style.BackColor = CellTemplateBackColor;
                    }
                    dgvButtons.DefaultCellStyle.ForeColor = CellTemplateforeColor;
                    ShanuDGV.Columns.Add(dgvButtons);
                    break;
                case ShanuControlTypes.ImageColumn:
                    DataGridViewImageColumn dgvnestedBtn = new DataGridViewImageColumn();
                    dgvnestedBtn.Name = cntrlnames;
                    ImageName = "expand.png";
                  
                    dgvnestedBtn.Image = Image.FromFile(ImageName);//global::ShanuDGVHelper_Demo.Properties.Resources.toggle;
                    // dgvnestedBtn.DataPropertyName = cntrlnames;
                    dgvnestedBtn.Visible = Visible;
                    dgvnestedBtn.Width = width;
                    dgvnestedBtn.SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvnestedBtn.Resizable = Resizable;
                    dgvnestedBtn.DefaultCellStyle.Alignment = cellAlignment;
                    dgvnestedBtn.HeaderCell.Style.Alignment = headerAlignment;
                    ShanuDGV.Columns.Add(dgvnestedBtn);
                    break;
            }
        }

        #endregion


        // Image Colukmn Click evnet
        #region Image Colukmn Click Event
        public void DGVMasterGridClickEvents(DataGridView ShanuMasterDGV, DataGridView ShanuDetailDGV, int columnIndexs, ShanuEventTypes eventtype, ShanuControlTypes types,DataTable DetailTable,String FilterColumn)
        {
            MasterDGVs = ShanuMasterDGV;
            DetailDGVs = ShanuDetailDGV;
            gridColumnIndex = columnIndexs;
            DetailgridDT = DetailTable;
            FilterColumnName = FilterColumn;
           
            MasterDGVs.CellContentClick += new DataGridViewCellEventHandler(masterDGVs_CellContentClick_Event);


        }
        private void masterDGVs_CellContentClick_Event(object sender, DataGridViewCellEventArgs e)
        {
           
            DataGridViewImageColumn cols = (DataGridViewImageColumn)MasterDGVs.Columns[0];
         
           // cols.Image = Image.FromFile(ImageName);
            MasterDGVs.Rows[e.RowIndex].Cells[0].Value = Image.FromFile("expand.png");

             if (e.ColumnIndex == gridColumnIndex)
             {
                
            
                 if (ImageName == "expand.png")
                 {
                     DetailDGVs.Visible = true;
                     ImageName = "toggle.png";
                     // cols.Image = Image.FromFile(ImageName);
                     MasterDGVs.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Image.FromFile(ImageName);


                     String Filterexpression = MasterDGVs.Rows[e.RowIndex].Cells[FilterColumnName].Value.ToString();

                     DataView detailView = new DataView(DetailgridDT);
                     detailView.RowFilter = FilterColumnName + " = '" + Filterexpression + "'";
                     if (detailView.Count <= 0)
                     {
                         MessageBox.Show("No Details Found");
                     }

                     MasterDGVs.Controls.Add(DetailDGVs);


                     Rectangle dgvRectangle = MasterDGVs.GetCellDisplayRectangle(1, e.RowIndex, true);
                     DetailDGVs.Size = new Size(MasterDGVs.Width - 200, 200);
                     DetailDGVs.Location = new Point(dgvRectangle.X, dgvRectangle.Y + 20);


                    
                     DetailDGVs.DataSource = detailView;


                 }
                 else
                 {
                     ImageName = "expand.png";
                     //  cols.Image = Image.FromFile(ImageName);
                     MasterDGVs.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Image.FromFile(ImageName);
                     DetailDGVs.Visible = false;
                 }                 
             }
             else
             {
                 DetailDGVs.Visible = false;
                 
             }
        }
        #endregion

        public void DGVDetailGridClickEvents(DataGridView ShanuDetailDGV)
        {
          
            DetailDGVs = ShanuDetailDGV;

            DetailDGVs.CellContentClick += new DataGridViewCellEventHandler(detailDGVs_CellContentClick_Event);


        }
          private void detailDGVs_CellContentClick_Event(object sender, DataGridViewCellEventArgs e)
          {
              MessageBox.Show("Detail grid Clicked : You clicked on " + DetailDGVs.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
          }

    }
}
//Enam decalaration for DataGridView Column Type ex like Textbox Column ,Button Column
public enum ShanuControlTypes { BoundColumn, TextBox, ComboBox, CheckBox, DateTimepicker, Button, NumericTextBox, ColorDialog, ImageColumn }
public enum ShanuEventTypes { CellClick, cellContentClick, EditingControlShowing }
