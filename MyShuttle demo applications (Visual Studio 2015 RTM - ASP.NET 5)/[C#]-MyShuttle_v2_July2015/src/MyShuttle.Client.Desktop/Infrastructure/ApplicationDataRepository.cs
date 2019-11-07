using MyShuttle.Client.Core.Infrastructure.Abstractions.Repositories;
using System.Collections.Specialized;
using System.Configuration;

namespace MyShuttle.Client.Desktop.Infrastructure
{
    public class ApplicationDataRepository : IApplicationDataRepository
    {
        private NameValueCollection appSettings;

        public ApplicationDataRepository()
        {
            appSettings = ConfigurationManager.AppSettings;
        }

        public string GetStringFromApplicationData(string key, string defaultValue)
        {
            var result = appSettings[key];
            if (result == null)
            {
                result = defaultValue;
            }
            return result;
        }

        public bool? GetOptionalBooleanFromApplicationData(string key, bool? defaultValue)
        {
            var resultStr = appSettings[key];
            bool result;
            if (!bool.TryParse(resultStr, out result) && defaultValue.HasValue)
            {
                result = defaultValue.Value;
            }
            return result;
        }

        public int? GetOptionalIntegerFromApplicationData(string key, int? defaultValue)
        {
            var resultStr = appSettings[key];
            int result;
            if (!int.TryParse(resultStr, out result) && defaultValue.HasValue)
            {
                result = defaultValue.Value;
            }
            return result;
        }

        public void SetStringToApplicationData(string key, string value)
        {
            appSettings.Set(key, value);
        }
    }
}
