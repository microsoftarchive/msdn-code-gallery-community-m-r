using MyShuttle.Client.Core.Infrastructure.Abstractions.Repositories;
using Windows.Storage;

namespace MyShuttle.Client.UniversalApp.Infrastructure
{
    public class ApplicationDataRepository : IApplicationDataRepository
    {
        public string GetStringFromApplicationData(string key, string defaultValue)
        {
            return ApplicationData.Current.LocalSettings.Values.ContainsKey(key) ? ApplicationData.Current.LocalSettings.Values[key].ToString() : defaultValue;
        }

        public bool? GetOptionalBooleanFromApplicationData(string key, bool? defaultValue)
        {
            return ApplicationData.Current.LocalSettings.Values.ContainsKey(key) ? bool.Parse(ApplicationData.Current.LocalSettings.Values[key].ToString()) : defaultValue;
        }

        public int? GetOptionalIntegerFromApplicationData(string key, int? defaultValue)
        {
            return ApplicationData.Current.LocalSettings.Values.ContainsKey(key) ? int.Parse(ApplicationData.Current.LocalSettings.Values[key].ToString()) : defaultValue;
        }

        public void SetStringToApplicationData(string key, string value)
        {
            ApplicationData.Current.LocalSettings.Values[key] = value;
        }
    }
}
