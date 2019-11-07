using MyCompany.Travel.Client.WP.Resources;

namespace MyCompany.Travel.Client.WP
{
    /// <summary>
    /// Provides access to string resources.
    /// </summary>
    public class LocalizedStrings
    {
        private static AppResources _localizedResources = new AppResources();

        /// <summary>
        /// Localized Resources
        /// </summary>
        public AppResources LocalizedResources { get { return _localizedResources; } }
    }
}