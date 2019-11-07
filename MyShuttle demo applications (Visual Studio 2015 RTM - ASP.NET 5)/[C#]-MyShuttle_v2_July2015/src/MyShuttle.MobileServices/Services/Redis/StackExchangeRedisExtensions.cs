

namespace MyShuttle.MobileServices.Services.Redis
{
    using Newtonsoft.Json;
    using StackExchange.Redis;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    public static class StackExchangeRedisExtensions
    {
        public static T Get<T>(this IDatabase cache, string key)
        {
            var value = cache.StringGet(key);

            if (value.HasValue)
                return JsonConvert.DeserializeObject<T>(value);

            return default(T);
        }

        public static object Get(this IDatabase cache, string key)
        {
            return JsonConvert.DeserializeObject<object>(cache.StringGet(key));
        }

        public static void Set(this IDatabase cache, string key, object value)
        {
            var serialized = JsonConvert.SerializeObject(value);
            cache.StringSet(key, serialized);
        }

    }
}
