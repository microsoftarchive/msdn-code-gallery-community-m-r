using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelConnectionLibraryCS
{
    public class Utils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pFileName">Excel file to work with</param>
        /// <param name="pHeader">Yes to include header row, No to exclude header row</param>
        /// <returns></returns>
        public string ConnectionString(string pFileName, string pHeader = "No", int pImex = 1)
        {

            OleDbConnectionStringBuilder Builder = new OleDbConnectionStringBuilder();

            if (Path.GetExtension(pFileName).ToUpper() == ".XLS")
            {
                Builder.Provider = "Microsoft.Jet.OLEDB.4.0";
                Builder.Add("Extended Properties", $"Excel 8.0;IMEX={pImex};HDR={pHeader};");
            }
            else
            {
                Builder.Provider = "Microsoft.ACE.OLEDB.12.0";
                Builder.Add("Extended Properties", $"Excel 12.0;IMEX={pImex};HDR={pHeader};");
            }

            Builder.DataSource = pFileName;

            return Builder.ConnectionString;

        }
    }
}
