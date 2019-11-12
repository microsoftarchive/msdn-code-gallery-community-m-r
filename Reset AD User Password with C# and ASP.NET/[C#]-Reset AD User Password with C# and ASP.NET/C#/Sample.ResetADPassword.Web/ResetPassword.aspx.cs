using System;
using System.DirectoryServices;
using System.Text;
using System.Web.Configuration;
using System.Web.UI;

namespace Sample.ResetADPassword.Web
{
    public partial class ResetPassword : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ResetUserPassword(object sender, EventArgs e)
        {
            var userDn = txtUsername.Text;

            var directoryEntry = GetDirectoryEntryByUserName(userDn);
            directoryEntry.Invoke("SetPassword", new object[] {"password"});
            directoryEntry.Properties["LockOutTime"].Value = 0; 

            directoryEntry.Close();
        }

        public static DirectoryEntry GetDirectoryEntryByUserName(string userName)
        {
            var de = GetDirectoryObject(GetDomain());
            var deSearch = new DirectorySearcher(de)
                                             {SearchRoot = de, Filter = "(&(objectCategory=user)(cn=" + userName + "))"};
            
            var results = deSearch.FindOne();
            return results != null ? results.GetDirectoryEntry() : null;
        }

        private static string GetDomain()
        {
            string adDomain = WebConfigurationManager.AppSettings["adDomainFull"];

            var domain = new StringBuilder();
            string[] dcs = adDomain.Split('.');
            for (var i = 0; i < dcs.GetUpperBound(0) + 1; i++)
            {
                domain.Append("DC=" + dcs[i]);
                if (i < dcs.GetUpperBound(0))
                {
                    domain.Append(",");
                }
            }
            return domain.ToString();
        }

        private static DirectoryEntry GetDirectoryObject(string domainReference)
        {
            string adminUser = WebConfigurationManager.AppSettings["adAdminUser"];
            string adminPassword = WebConfigurationManager.AppSettings["adAdminPassword"];
            string fullPath = "LDAP://" + domainReference;

            var directoryEntry = new DirectoryEntry(fullPath , adminUser, adminPassword, AuthenticationTypes.Secure);
            return directoryEntry;
        }
    }
}