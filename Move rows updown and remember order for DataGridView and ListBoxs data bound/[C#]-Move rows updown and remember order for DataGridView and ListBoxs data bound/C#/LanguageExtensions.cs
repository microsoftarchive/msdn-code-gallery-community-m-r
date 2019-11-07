using System;
using System.Windows.Forms;
using System.Data;

/// <summary>
/// Contains two methods for moving DataRows up/down. 
/// You could easily tweak the code to work for say a ListBox.
/// </summary>
/// <remarks></remarks>
public static class LanguageExtensions
{
    public static DataTable GetChecked(this DataTable sender, string ColumnName)
    {
        var d = from T in sender.AsEnumerable()  where T.Field<bool>(ColumnName) == true   select T;
        DataTable dt = sender.Clone();
        foreach (var row in d)
        {
            dt.Rows.Add(row.ItemArray);
        }
        dt.Columns[ColumnName].ColumnMapping = MappingType.Hidden;
        return dt;
    }

    /// <summary>
    /// Used to copy columns from another DataGridView to this DataGridView
    /// </summary>
    /// <param name="Self"></param>
    /// <param name="CloneFrom"></param>
    /// <remarks>
    /// Only does cloning if Self has no columns
    /// </remarks>
    public static void CloneColumns(this DataGridView Self, DataGridView CloneFrom)
    {
        if (Self.ColumnCount == 0)
        {
            foreach (DataGridViewColumn c in CloneFrom.Columns)
            {
                Self.Columns.Add((DataGridViewColumn)c.Clone());
            }
        }
    }

    public static void MoveRowUp(this DataGridView sender, BindingSource bs)
    {
        if (!(string.IsNullOrWhiteSpace(bs.Sort)))
        {
            bs.Sort = "";
        }
        int CurrentColumnIndex = sender.CurrentCell.ColumnIndex;
        Int32 NewIndex = Convert.ToInt32((bs.Position == 0) ? 0 : bs.Position - 1);
        var dt = (DataTable)bs.DataSource;
        DataRow RowToMove = ((DataRowView)bs.Current).Row;
        DataRow NewRow = dt.NewRow();

        NewRow.ItemArray = RowToMove.ItemArray;
        dt.Rows.RemoveAt(bs.Position);
        dt.Rows.InsertAt(NewRow, NewIndex);
        dt.AcceptChanges();
        bs.Position = NewIndex;
        sender.CurrentCell = sender[CurrentColumnIndex, NewIndex];
    }
    public static void MoveRowUp(this BindingSource sender)
    {
        if (!(string.IsNullOrWhiteSpace(sender.Sort)))
        {
            sender.Sort = "";
        }

        Int32 NewIndex = Convert.ToInt32((sender.Position == 0) ? 0 : sender.Position - 1);
        var dt = (DataTable)sender.DataSource;
        DataRow RowToMove = ((DataRowView)sender.Current).Row;
        DataRow NewRow = dt.NewRow();

        NewRow.ItemArray = RowToMove.ItemArray;
        dt.Rows.RemoveAt(sender.Position);
        dt.Rows.InsertAt(NewRow, NewIndex);
        dt.AcceptChanges();
        sender.Position = NewIndex;

    }
    public static void MoveRowDown(this DataGridView sender, BindingSource bs)
    {
        if (!(string.IsNullOrWhiteSpace(bs.Sort)))
        {
            bs.Sort = "";
        }
        int CurrentColumnIndex = sender.CurrentCell.ColumnIndex;
        Int32 UpperLimit = bs.Count - 1;
        Int32 NewIndex = Convert.ToInt32((bs.Position + 1 >= UpperLimit) ? UpperLimit : bs.Position + 1);
        var dt = (DataTable)bs.DataSource;
        DataRow RowToMove = ((DataRowView)bs.Current).Row;
        DataRow NewRow = dt.NewRow();

        NewRow.ItemArray = RowToMove.ItemArray;
        dt.Rows.RemoveAt(bs.Position);
        dt.Rows.InsertAt(NewRow, NewIndex);
        dt.AcceptChanges();
        bs.Position = NewIndex;
        sender.CurrentCell = sender[CurrentColumnIndex, NewIndex];
    }
    public static void MoveRowDown(this BindingSource sender)
    {
        if (!(string.IsNullOrWhiteSpace(sender.Sort)))
        {
            sender.Sort = "";
        }

        Int32 UpperLimit = sender.Count - 1;
        Int32 NewIndex = Convert.ToInt32((sender.Position + 1 >= UpperLimit) ? UpperLimit : sender.Position + 1);
        var dt = (DataTable)sender.DataSource;
        DataRow RowToMove = ((DataRowView)sender.Current).Row;
        DataRow NewRow = dt.NewRow();

        NewRow.ItemArray = RowToMove.ItemArray;
        dt.Rows.RemoveAt(sender.Position);
        dt.Rows.InsertAt(NewRow, NewIndex);
        dt.AcceptChanges();
        sender.Position = NewIndex;

    }

    public static void MoveRowUp(this ListBox Sender, BindingSource bs)
    {
        if (!(string.IsNullOrWhiteSpace(bs.Sort)))
        {
            bs.Sort = "";
        }

        string DisplayText = Sender.Text;
        Int32 SelectedIndex = bs.Position;
        string SelectedItem = Sender.SelectedItem.ToString();
        Int32 NewIndex = Convert.ToInt32((bs.Position == 0) ? 0 : bs.Position - 1);
        var dt = (DataTable)bs.DataSource;
        DataRow RowToMove = ((DataRowView)bs.Current).Row;
        DataRow NewRow = dt.NewRow();
        NewRow.ItemArray = RowToMove.ItemArray;
        dt.Rows.RemoveAt(SelectedIndex);
        dt.Rows.InsertAt(NewRow, NewIndex);

        dt.AcceptChanges();
        bs.Position = bs.Find(Sender.DisplayMember, DisplayText);

        for (int x = 0; x < dt.Rows.Count; x++)
        {
            dt.Rows[x][2] = x;
        }
    }
    public static void MoveRowDown(this ListBox Sender, BindingSource bs)
    {
        if (!(string.IsNullOrWhiteSpace(bs.Sort)))
        {
            bs.Sort = "";
        }

        string DisplayText = Sender.Text;
        Int32 SelectIndex = bs.Position;
        string SelectedItem = Sender.SelectedItem.ToString();
        Int32 UpperLimit = bs.Count - 1;
        Int32 NewIndex = Convert.ToInt32((bs.Position + 1 >= UpperLimit) ? UpperLimit : bs.Position + 1);
        var dt = (DataTable)bs.DataSource;
        DataRow RowToMove = ((DataRowView)bs.Current).Row;
        DataRow NewRow = dt.NewRow();
        NewRow.ItemArray = RowToMove.ItemArray;
        dt.Rows.RemoveAt(SelectIndex);
        dt.Rows.InsertAt(NewRow, NewIndex);

        dt.AcceptChanges();
        bs.Position = bs.Find(Sender.DisplayMember, DisplayText);

        for (int x = 0; x < dt.Rows.Count; x++)
        {
            dt.Rows[x][2] = x;
        }

    }
}