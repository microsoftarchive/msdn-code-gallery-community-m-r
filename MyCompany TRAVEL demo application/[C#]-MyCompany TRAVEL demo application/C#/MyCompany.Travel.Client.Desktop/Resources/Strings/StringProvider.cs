namespace MyCompany.Travel.Client.Desktop.Resources.Strings
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;
    using System.Resources;
    using System.Reflection;

    /// <summary>
    /// String provider acts as a resolver to allow data binding to resx files.
    /// </summary>
    public class StringProvider
    {
        private static ObjectDataProvider provider;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public StringProvider()
        {
        }

        /// <summary>
        /// Object data provider from app.xaml
        /// </summary>
        public static ObjectDataProvider Provider
        {
            get
            {
                if (provider == null)
                    provider = (ObjectDataProvider)App.Current.FindResource("Resources");

                return provider;
            }
        }

        /// <summary>
        /// Returns an instance of the resx file.
        /// </summary>
        /// <returns></returns>
        public StringResources GetResourceInstance()
        {
            return new StringResources();
        }

        /// <summary>
        /// Apply change to current culture and refresh the string provider.
        /// </summary>
        /// <param name="culture"></param>
        public static void ChangeCulture(CultureInfo culture)
        {
            Properties.Resources.Culture = culture;
            Provider.Refresh();
        }

        /// <summary>
        /// Get a property string from it name.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string GetString(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return string.Empty;

            try
            {
                ResourceManager man = new ResourceManager("MyCompany.Travel.Client.Desktop.Resources.Strings.StringResources", Assembly.GetExecutingAssembly());
                return man.GetString(propertyName);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
