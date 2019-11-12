using System;

namespace MyShuttle.Client.Desktop.Resources
{
    public class LocalizedResources
    {
        private Lazy<Resources> resources = new Lazy<Resources>(()=> new Resources());

        public Resources Resources
        {
            get
            {
                return resources.Value;
            }
        }
    }
}
