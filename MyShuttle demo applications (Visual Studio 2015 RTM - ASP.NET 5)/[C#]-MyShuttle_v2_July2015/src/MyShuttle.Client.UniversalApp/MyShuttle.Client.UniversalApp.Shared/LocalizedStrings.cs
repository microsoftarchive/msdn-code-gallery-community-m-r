using Windows.ApplicationModel.Resources;

namespace MyShuttle.Client.UniversalApp
{
    public class LocalizedStrings
    {
        private static ResourceLoader _localizedResources = new ResourceLoader();

        public ResourceLoader LocalizedResources { get { return _localizedResources; } }

        private string GetVal(string key)
        {
            return LocalizedResources.GetString(key);
        }

        public string this[string key]
        {
            get
            {
                return GetVal(key);
            }
        }
    }
}
