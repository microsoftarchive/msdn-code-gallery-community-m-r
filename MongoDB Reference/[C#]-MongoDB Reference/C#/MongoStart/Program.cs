using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using com.techphernalia.windows.forms.techieUI;

namespace MongoStart
{
    static class Program
    {
        public static String Title = "Mongo Learning";
        public static MongoData MongoData;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMongoSetup());

            List<TreeDataList> tutorials = new List<TreeDataList>() 
            {
                new About(),
                new BSONTutorial(),
                new MongoGeneral(),
                new FindingDocuments(),
                new InsertingDocuments(),
                new DeletingDocuments(),
                new UpdateDocuments(),
                new ExportDocuments(),
                new ImportDocuments(),
            };

            Application.Run(new frmMain("Learning Mongo the techie way...", tutorials, "C#"));
        }
    }
}