using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace com.techphernalia.windows.forms.techieUI
{
    [Title("About")]
    [Prefix("Help")]
    public class About : TreeDataList
    {
        #region 1. License
        [Title("glyphicons_free")]
        [Category("1. License")]
        [Description("This application uses glyphicons_free(http://glyphicons.com/), under Creative Commons Attribution 3.0 Unported (CC BY 3.0)(http://creativecommons.org/licenses/by/3.0/legalcode). Please let us know if it violates the license in any way and we will be more than happy to resolve it.")]
        public void HelpL000glyphicons_free()
        {
            StreamReader rdr = new StreamReader("resources\\AttributionUnported.txt");
            Console.WriteLine(rdr.ReadToEnd());
        }
        [Title("MS-PL")]
        [Category("1. License")]
        [Description("This application is released under Microsoft Public License(http://opensource.org/licenses/ms-pl.html) , and has learned a lot from other applications released under Microsoft Public License.\n\nHave tried to use UI from : 'LINQ - Sample Queries'(http://code.msdn.microsoft.com/LINQ-Sample-Queries-13a42a54)")]
        public void HelpL000MSPL()
        {
            StreamReader rdr = new StreamReader("resources\\MSPL.txt");
            Console.WriteLine(rdr.ReadToEnd().Replace("\n",Environment.NewLine));
        }
        #endregion

        #region 2. Contact us
        [Title("Email")]
        [Category("2. Contact us")]
        [Description("You can mail your queries at me@techphernalia.com")]
        public void HelpC000Email()
        {
            Console.WriteLine("Mail your queries/feedback to me@techphernalia.com");
        }

        [Title("Website")]
        [Category("2. Contact us")]
        [Description("You can visit us at http://www.techphernalia.com/")]
        public void HelpC000Website()
        {
            Process.Start("http://www.techphernalia.com/");
            Console.WriteLine("Please visit our website at http://www.techphernalia.com/");
        }

        [Title("MSDN")]
        [Category("2. Contact us")]
        [Description("You can visit our MSDN profile at http://social.msdn.microsoft.com/profile/durgesh%20chaudhary")]
        public void HelpC000MSDN()
        {
            Process.Start("http://social.msdn.microsoft.com/profile/durgesh%20chaudhary");
            Console.WriteLine("Please visit our MSDN Profile at http://social.msdn.microsoft.com/profile/durgesh%20chaudhary");
        }

        [Title("GitHub")]
        [Category("2. Contact us")]
        [Description("You can visit our Github profile at https://github.com/techphernalia/")]
        public void HelpC000GH()
        {
            Process.Start("https://github.com/techphernalia/");
            Console.WriteLine("Please visit our MSDN Profile at https://github.com/techphernalia/");
        }

        [Title("Stack Exchange")]
        [Category("2. Contact us")]
        [Description("You can visit our Stack Overflow profile at http://stackexchange.com/users/3211800/durgesh-chaudhary?tab=accounts")]
        public void HelpC000SE()
        {
            Process.Start("http://stackexchange.com/users/3211800/durgesh-chaudhary?tab=accounts");
            Console.WriteLine("Please visit our Stack Overflow Profile at http://stackexchange.com/users/3211800/durgesh-chaudhary?tab=accounts");
        }
        #endregion

        #region 3. Help
        [Title("How to use")]
        [Category("3. Help")]
        [Description("Click Run to know more.\nThis application briefs you on BSON Document used in MongoDB. In coming time it will be updated with more MongoDB tutorials. Stay tuned with us.")]
        public void HelpH000Me()
        {
            StreamReader rdr = new StreamReader("resources\\Help.txt");
            Console.WriteLine(rdr.ReadToEnd());
        }
        #endregion
    }
}
