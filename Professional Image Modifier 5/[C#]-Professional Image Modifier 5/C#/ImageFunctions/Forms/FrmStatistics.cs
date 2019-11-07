using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ImageFunctions.Forms
{
	public partial class FrmStatistics : DockContent
	{
		public delegate void StatisticsLogHandler(string Message);
		public event StatisticsLogHandler StatisticsLog;

		private Stopwatch swStatistics = new Stopwatch();
		private string thisImage;
		public FrmStatistics()
		{
			InitializeComponent();

		}


		protected override string GetPersistString()
		{
			return this.Text;

		}


		public void DoStatistics(string CurrentImage)
		{
			thisImage = CurrentImage;
			swStatistics.Reset();
			swStatistics.Start();
			StatisticsLog("Building Statistics");
			Classes.ImageStatistics imgStats = new Classes.ImageStatistics(CurrentImage);
			imgStats.StatsComplete += imgStats_StatsComplete;
			imgStats.StatsError += imgStats_StatsError;
			imgStats.GetStatistics();
			swStatistics.Stop();
			StatisticsLog("Statistics completed in " + swStatistics.Elapsed);
		}

		void imgStats_StatsError(string Message)
		{
			StatisticsLog("Error Collecting Statistics: " + Message);
		}

		// Compile the statistics into the Datagrid view that we have generated
		void imgStats_StatsComplete(System.Collections.ArrayList Statistics)
		{
			Array tmp = Statistics.ToArray();

			ResetStatistics();

			for (int idx = 0; idx < tmp.Length; idx++)
			{
				DataGridViewRow dgvr = new DataGridViewRow();
				dgvr.CreateCells(dgvStatistics);
				string description = tmp.GetValue(idx).ToString();

				string value = tmp.GetValue(idx + 1).ToString();
				dgvr.Cells[0].Value = description;
				dgvr.Cells[1].Value = value;
				AddStatisticsRow(dgvr);
				idx++;
			}
		}

		private void AddStatisticsRow(DataGridViewRow dgvr)
		{

			dgvStatistics.Rows.Add(dgvr);

		}


		private void ResetStatistics()
		{

			dgvStatistics.Rows.Clear();

		}

		private void rebuildToolStripMenuItem_Click(object sender, EventArgs e)
		{

			DoStatistics(thisImage);

		}
	}
}
