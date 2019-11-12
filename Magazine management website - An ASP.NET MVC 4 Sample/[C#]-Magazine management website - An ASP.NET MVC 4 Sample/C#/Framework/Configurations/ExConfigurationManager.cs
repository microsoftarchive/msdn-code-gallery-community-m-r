using System.Collections.Specialized;
using System.Configuration;

namespace CIK.News.Framework.Configurations
{
    public class ExConfigurationManager : IExConfigurationManager 
    {
        public object GetSection(string sectionName)
        {
            return ConfigurationManager.GetSection(sectionName);
        }

        public ConnectionStringSettingsCollection GetConnectionStrings()
        {
            return ConfigurationManager.ConnectionStrings;
        }

        public NameValueCollection GetAppSettings()
        {
            return ConfigurationManager.AppSettings;
        }

        public string GetAppConfigBy(string appConfigName)
        {
            return GetAppSettings()[appConfigName];
        } 
    }
}