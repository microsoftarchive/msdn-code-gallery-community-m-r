namespace MyCompany.Visitors.Client.WP
{
    using MyCompany.Visitors.Client.WP.Resources;

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