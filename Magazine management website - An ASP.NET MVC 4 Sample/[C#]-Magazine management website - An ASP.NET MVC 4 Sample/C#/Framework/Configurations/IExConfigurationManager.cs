using System.Collections.Specialized;
using System.Configuration;

namespace CIK.News.Framework.Configurations
{
    public interface IExConfigurationManager
    {
        object GetSection(string sectionName);

        ConnectionStringSettingsCollection GetConnectionStrings();

        NameValueCollection GetAppSettings();

        string GetAppConfigBy(string appConfigName); 
    }
}