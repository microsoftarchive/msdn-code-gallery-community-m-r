using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;

namespace Repository
{
    [ModuleExport(typeof(RepositoryModule))]
    public class RepositoryModule:IModule
    {
        public void Initialize()
        {
        }
    }
}
