

namespace MyShuttle.WebJob
{
    using Microsoft.Azure.WebJobs;
    using System.Configuration;

    public class QueueNameResolver : INameResolver
    {
        public string Resolve(string name)
        {
            return ConfigurationManager.AppSettings[name].ToString();
        }
    }
}
