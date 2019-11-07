using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LanguageExtensions
{
    public static class GeneralExtensions
    {
        public static void ExpandColumns(this DataGridView sender)
        {
            foreach (DataGridViewColumn col in sender.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }
        public static bool IsNull(this object sender)
        {
            return sender == null || sender == DBNull.Value || Convert.IsDBNull(sender) == true;
        }
        public static bool IsNullOrWhiteSpace(this string sender)
        {
            return string.IsNullOrWhiteSpace(sender);
        }
        public static bool Between<T>(this T actual, T lower, T upper) where T : IComparable<T>
        {
            return actual.CompareTo(lower) >= 0 && actual.CompareTo(upper) < 0;
        }
        /// <summary>
        /// Get last column value in this table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <param name="ColumnName">Name of column to get value for</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static T FieldLastValue<T>(this System.Data.DataTable dt, string ColumnName)
        {
            return dt.Rows[dt.Rows.Count - 1].Field<T>(dt.Columns[ColumnName]);
        }
        /// <summary>
        /// Get last column value in this table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <param name="ColumnIndex">Ordinal index of column to get value for.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static T FieldLastValue<T>(this System.Data.DataTable dt, int ColumnIndex)
        {
            return dt.Rows[dt.Rows.Count - 1].Field<T>(dt.Columns[ColumnIndex]);
        }
                /// <summary>
        /// Creates a wrapper for the given event handler which unsubscribes from the event source immediately prior to calling the given event handler.
        /// </summary>
        /// <param name="handler">Handler that will be wrapped.</param>
        /// <param name="remove">Action to remove the wrapped handler. (wrapper =&gt; source.MyEvent -= wrapper);</param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// source.TestEvent += new EventHandler(source_TestEvent)
        ///     .HandleOnce(wrapper => source.TestEvent -= wrapper);
        /// </code>
        /// </example>
        public static EventHandler HandleOnce(this EventHandler handler, Action<EventHandler> remove)
        {
            EventHandler wrapper = null;
            wrapper = delegate (object sender, EventArgs e)
            {
                remove(wrapper);
                handler(sender, e);
            };
            return wrapper;
        }
    }
}
