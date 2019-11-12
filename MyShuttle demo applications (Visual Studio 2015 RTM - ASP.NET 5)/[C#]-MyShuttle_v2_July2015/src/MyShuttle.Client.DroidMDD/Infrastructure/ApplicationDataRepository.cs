using MyShuttle.Client.Core.Infrastructure.Abstractions.Repositories;
using PerpetualEngine.Storage;

namespace MyShuttle.Client.Droid.Infrastructure
{
    public class ApplicationDataRepository : IApplicationDataRepository
    {
        private const string StorageGroupKey = "Default";

        private static readonly SimpleStorage StorageGroup = SimpleStorage.EditGroup(StorageGroupKey);

        public string GetStringFromApplicationData(string key, string defaultValue)
        {
            var returnString = StorageGroup.HasKey(key) ?
                StorageGroup.Get<string>(key) :
                defaultValue;

            return returnString;
        }

        public bool? GetOptionalBooleanFromApplicationData(string key, bool? defaultValue)
        {
            var returnBool = StorageGroup.HasKey(key) ? 
                StorageGroup.Get<bool?>(key) : 
                defaultValue;

            return returnBool;
        }

        public int? GetOptionalIntegerFromApplicationData(string key, int? defaultValue)
        {
            var returnInt = StorageGroup.HasKey(key) ? 
                StorageGroup.Get<int?>(key) : 
                defaultValue;
            
            return returnInt;
        }

        public void SetStringToApplicationData(string key, string value)
        {
            StorageGroup.Put<string>(key, value);
        }
    }
}