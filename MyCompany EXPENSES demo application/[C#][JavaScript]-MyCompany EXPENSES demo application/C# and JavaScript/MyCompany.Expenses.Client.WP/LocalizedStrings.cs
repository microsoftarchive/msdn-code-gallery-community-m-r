
namespace MyCompany.Expenses.Client.WP
{
    using MyCompany.Expenses.Client.WP.Resources;

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