using System;
using System.Data.OleDb;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using Spire.Xls;

namespace Spire.Xls.Sample
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnRun;
		private System.Windows.Forms.Button btnAbout;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGrid dataGrid1;
		/// <summary>
		/// Required designer variable.
		/// </summary
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnRun = new System.Windows.Forms.Button();
			this.btnAbout = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// btnRun
			// 
			this.btnRun.Location = new System.Drawing.Point(360, 288);
			this.btnRun.Name = "btnRun";
			this.btnRun.Size = new System.Drawing.Size(72, 23);
			this.btnRun.TabIndex = 2;
			this.btnRun.Text = "Run";
			this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
			// 
			// btnAbout
			// 
			this.btnAbout.Location = new System.Drawing.Point(448, 288);
			this.btnAbout.Name = "btnAbout";
			this.btnAbout.TabIndex = 3;
			this.btnAbout.Text = "Close";
			this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.label1.Location = new System.Drawing.Point(16, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(528, 32);
			this.label1.TabIndex = 4;
			this.label1.Text = "The sample demonstrates how to read data  from  an excel workbook and export data" +
				" to datatable.";
			// 
			// dataGrid1
			// 
			this.dataGrid1.DataMember = "";
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(16, 56);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.ReadOnly = true;
			this.dataGrid1.Size = new System.Drawing.Size(512, 216);
			this.dataGrid1.TabIndex = 5;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(544, 325);
			this.Controls.Add(this.dataGrid1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnAbout);
			this.Controls.Add(this.btnRun);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Spire.XLS sample";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void btnRun_Click(object sender, System.EventArgs e)
		{
			Workbook workbook = new Workbook();
			
			workbook.LoadFromFile(@"..\..\..\DataTableSample.xls");
			//Initailize worksheet
			Worksheet sheet = workbook.Worksheets[0];

			this.dataGrid1.DataSource =  sheet.ExportDataTable();
		}


		private void btnAbout_Click(object sender, System.EventArgs e)
		{
			Close();
		}


	}
}
