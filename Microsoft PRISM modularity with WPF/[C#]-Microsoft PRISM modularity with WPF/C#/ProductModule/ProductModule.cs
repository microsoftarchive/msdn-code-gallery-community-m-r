using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;

namespace ProductModule
{
    [ModuleExport(typeof(ProductModule))]
    public class ProductModule:IModule
    {
        [Import]
        public IRegionManager Region { get; set; }

        public void Initialize()
        {
            Region.RequestNavigate("ProductRegion", "ProductView");
        }
    }
}
