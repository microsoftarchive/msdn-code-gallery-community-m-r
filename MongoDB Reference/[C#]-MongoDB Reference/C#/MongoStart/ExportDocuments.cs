using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.techphernalia.windows.forms.techieUI;

namespace MongoStart
{
    [Title("Exporting Documents")]
    [Prefix("MongoExport")]
    public class ExportDocuments : TreeDataList
    {
        #region 1. MongoExport
        [Title("MongoExport Introduction")]
        [Category("1. MongoExport")]
        [Description("MongoExport is used mainly to Export content from collection in  JSON or CSV format. It should not be used for full instance production backups as these do not have datatype information.\n\nClick Run for more information.")]
        public void MongoExport000Intro()
        {
            StringBuilder bldr = new StringBuilder();
            bldr.AppendLine("Export is used mainly to Export content to JSON or CSV. It should not be used for full instance production backups as these do not have datatype information.");
            bldr.AppendLine("Provide required information as [--db techphernalia] [--collection zips] [--csv --fields field1,field2 | --fieldFile fieldfile] --out outputfile");
            bldr.AppendLine("Provide optional information as [--host 192.168.56.104] [--port 2585] [--username durgesh] [--password durgesh] [--authenticationDatabase durgesh]");
            bldr.AppendLine("There are many more information which can be provided while Exporting.");
            Console.WriteLine(bldr.ToString());
        }
        [Title("Exporting as JSON Documents")]
        [Category("1. MongoExport")]
        [Description("Here we are Exporting from zips collection which contains 29467 records, we will specify a query so that less records are exported rather than full collection.\n\nSo start your mongod process and fire given command in your console.")]
        [Code("Console", "mongoexport --db database --collection collection --query \"{\\\"city\\\":\\\"PLEASANT GROVE\\\"}\" --out zipstemp.json")]
        public void MongoExport010ExportJSON()
        {
            Console.WriteLine("mongoexport --host " + Program.MongoData.Host + " --port " + Program.MongoData.Port + " --db " + Program.MongoData.Database + " --collection zips --query \"{\\\"city\\\":\\\"PLEASANT GROVE\\\"}\" --out zipstemp.json");
            Console.WriteLine("Fire above command in a new command prompt and it will export some records to given file.");
        }
        [Title("Exporting as CSV Documents")]
        [Category("1. MongoExport")]
        [Description("Here we are Exporting from zips collection which contains 29467 records, we will specify a query so that less records are exported rather than full collection.\n\nSo start your mongod process and fire given command in your console.")]
        [Code("Console", "mongoexport --db database --collection collection --query \"{\\\"city\\\":\\\"PLEASANT GROVE\\\"}\" --csv --fields city,loc,pop,state,_id --out zipstemp.csv")]
        public void MongoExport020ExportCSV()
        {
            Console.WriteLine("mongoexport --host " + Program.MongoData.Host + " --port " + Program.MongoData.Port + " --db " + Program.MongoData.Database + " --collection zips --query \"{\\\"city\\\":\\\"PLEASANT GROVE\\\"}\" --csv --fields city,loc,pop,state,_id --out zipstemp.csv" + Environment.NewLine);
            Console.WriteLine("Fire above command in a new command prompt and it will export some records to given file.");
        }
        #endregion
    }
}
