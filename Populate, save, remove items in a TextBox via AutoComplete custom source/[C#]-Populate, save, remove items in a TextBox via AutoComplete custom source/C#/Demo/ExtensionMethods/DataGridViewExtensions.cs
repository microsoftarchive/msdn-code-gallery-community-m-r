using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// DataAdapter http://msdn.microsoft.com/en-us/library/377a8x4t(v=vs.110).aspx?cs-save-lang=1&cs-lang=vb#code-snippet-1

internal static class DataGridViewExtensions
{
    public static string[] ExportRows(this DataGridView sender)
    {
        return 
            (
                from row in sender.Rows.Cast<DataGridViewRow>()
                where !((DataGridViewRow)row).IsNewRow
                let RowItem = string.Join(",", Array.ConvertAll(((DataGridViewRow)row).Cells.Cast<DataGridViewCell>().ToArray(), 
                (DataGridViewCell c) => ((c.Value == null) ? "" : c.Value.ToString())))
                select RowItem
             ).ToArray();
    }

    /// <summary>
    /// Resize all columns to see longest item in each column
    /// </summary>
    /// <param name="sender"></param>
    public static void ExpandColumns(this DataGridView sender)
    {
        foreach (DataGridViewColumn col in sender.Columns)
        {
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
    }

}

