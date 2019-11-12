using System;
using System.Windows.Forms;

namespace DataGridViewExtensionsCS
{
    public static class Extensions
    {
        public static void ExpandColumns(this DataGridView pDataGridView)
        {
            foreach (DataGridViewColumn col in pDataGridView.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }
    }
}
