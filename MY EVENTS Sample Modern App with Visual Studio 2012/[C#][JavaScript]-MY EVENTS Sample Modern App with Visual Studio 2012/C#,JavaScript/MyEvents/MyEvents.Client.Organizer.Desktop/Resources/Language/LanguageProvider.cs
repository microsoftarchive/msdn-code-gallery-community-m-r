using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MyEvents.Client.Organizer.Desktop.Resources.Language
{
    /// <summary>
    /// Language provider class.
    /// </summary>
    public class LanguageProvider
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public LanguageProvider()
        {

        }
        /// <summary>
        /// The Resources ObjectDataProvider uses this method to get an instance of the WPFLocalize.Properties.Resources class
        /// </summary>
        /// <returns></returns>
        public LanguageResources GetResourceInstance()
        {
            return new LanguageResources();
        }

        private static ObjectDataProvider m_provider;
        /// <summary>
        /// ODP for resource change
        /// </summary>
        public static ObjectDataProvider ResourceProvider
        {
            get
            {
                if (m_provider == null)
                    m_provider = (ObjectDataProvider)App.Current.FindResource("Res");
                return m_provider;
            }
        }

        /// <summary>
        /// Change the current culture used in the application.
        /// If the desired culture is available all localized elements are updated.
        /// </summary>
        /// <param name="culture">Culture to change to</param>
        public static void ChangeCulture(CultureInfo culture)
        {
            Properties.Resources.Culture = culture;
            ResourceProvider.Refresh();
        }

        /// <summary>
        /// Gets the string value of the string resource.
        /// </summary>
        /// <param name="name">The resource name.</param>
        /// <returns></returns>
        public static string GetString(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                return string.Empty;
            }
            try
            {
                // Create a resource manager to retrieve resources.
                ResourceManager rm = new ResourceManager("MyEvents.Client.Organizer.Desktop.Resources.Language.LanguageResources", Assembly.GetExecutingAssembly());
                return rm.GetString(name);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
