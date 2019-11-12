using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LanguageExtensions
{
    public static class BindingSourceExtensions
    {
        public static DataTable DataTable(this BindingSource sender)
        {
            return (DataTable)sender.DataSource;
        }
        public static DataRow CurrentRow(this BindingSource sender)
        {
            return ((DataRowView)sender.Current).Row;
        }
        /// <summary>
        /// Get the current row of binding source as datarow    
        /// </summary>
        /// <param name="bindingSource"></param>
        /// <returns></returns>
        public static DataRow GetCurrentDataRow(this BindingSource bindingSource)
        {
            if (bindingSource.Current == null)
                return null;
            else
                return ((DataRowView)bindingSource.Current).Row;
        }
    }
}
