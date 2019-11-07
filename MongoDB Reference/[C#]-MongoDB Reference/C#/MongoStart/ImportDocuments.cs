using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.techphernalia.windows.forms.techieUI;

namespace MongoStart
{
    [Title("Importing Documents")]
    [Prefix("MongoImport")]
    public class ImportDocuments : TreeDataList
    {
        #region 1. MongoImport
        [Title("MongoImport Introduction")]
        [Category("1. MongoImport")]
        [Description("MongoImport is used mainly to import content from JSON, CSV, or TSV files. It should not be used for full instance production backups as these do not have datatype information.\n\nClick Run for more information.")]
        public void MongoImport000Intro()
        {
            StringBuilder bldr = new StringBuilder();
            bldr.AppendLine("Import is used mainly to import content from JSON, CSV, or TSV data. It should not be used for full instance production backups as these do not have datatype information.");
            bldr.AppendLine("Provide required information as [--db techphernalia] [--collection zips] [--type JSON|TSV|CSV]");
            bldr.AppendLine("Provide optional information as [--host 192.168.56.104] [--port 2585] [--username durgesh] [--password durgesh] [--authenticationDatabase durgesh]");
            bldr.AppendLine("There are many more information which can be provided while importing.");
            Console.WriteLine(bldr.ToString());
        }
        [Title("Importing JSON file")]
        [Category("1. MongoImport")]
        [Description("Here we are importing zips.json which contains 29467 records containing details of individual US zip code.\n\nSo start your mongod process and fire given command in your console. Click run to get your environment specific query.")]
        [Code("Console", "mongoimport --db database --collection collection --type json --file file_location")]
        public void MongoImport010ImportJSON()
        {
            Console.WriteLine("mongoimport --host " + Program.MongoData.Host + " --port " + Program.MongoData.Port + " --db " + Program.MongoData.Database + " --collection zips --type json --file " + AppDomain.CurrentDomain.BaseDirectory + "\\resources\\zips.json" + Environment.NewLine);
            Console.WriteLine("Fire above command in a new command prompt and it will import zips to said database.");

            try
            {
                Console.WriteLine("Let me check if you have already done it...");
                Double zipsCount = new MongoDB.Driver.MongoClient(new MongoDB.Driver.MongoClientSettings { Server = new MongoDB.Driver.MongoServerAddress(Program.MongoData.Host, Program.MongoData.Port) }).GetServer().GetDatabase(Program.MongoData.Database).GetCollection("zips").Count();
                if (zipsCount > 20000)
                    Console.WriteLine("Great DB is saying " + zipsCount + " documents.");
                else
                    Console.WriteLine("Result not satisfactory, please import and come back again to recheck status.");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                Console.WriteLine("Check if MongoDB is running at " + Program.MongoData.Host + ":" + Program.MongoData.Port);
            }
            Console.WriteLine();
        }
        [Title("Importing CSV file")]
        [Category("1. MongoImport")]
        [Description("Here we are importing zipstemp.csv which contains 5 records containing details of some states and no zip :( .\n\nSo start your mongod process and fire given command in your console. Click run to get your environment specific query.")]
        [Code("Console", "mongoimport --db database --collection collection --type csv --headerline --file file_location")]
        public void MongoImport020ImportCSV()
        {
            Console.WriteLine("mongoimport --host " + Program.MongoData.Host + " --port " + Program.MongoData.Port + " --db " + Program.MongoData.Database + " --collection zips --type csv  --headerline --file " + AppDomain.CurrentDomain.BaseDirectory + "\\resources\\zipstemp.csv" + Environment.NewLine);
            Console.WriteLine("Fire above command in a new command prompt and it will import zips to said database.");
        }
        [Title("Importing TSV file")]
        [Category("1. MongoImport")]
        [Description("Here we are importing zipstemp.tsv which contains 5 records containing details of some states and no zip :( .\n\nSo start your mongod process and fire given command in your console. Click run to get your environment specific query.")]
        [Code("Console", "mongoimport --db database --collection collection --type tsv --file --headerline file_location")]
        public void MongoImport030ImportTSV()
        {
            Console.WriteLine("mongoimport --host " + Program.MongoData.Host + " --port " + Program.MongoData.Port + " --db " + Program.MongoData.Database + " --collection zips --type tsv  --headerline --file " + AppDomain.CurrentDomain.BaseDirectory + "\\resources\\zipstemp.tsv" + Environment.NewLine);
            Console.WriteLine("Fire above command in a new command prompt and it will import zips to said database.");
        }
        #endregion
    }
}
