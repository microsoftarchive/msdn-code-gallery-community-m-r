using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;

namespace EmployeeModule
{
    [ModuleExport(typeof(EmployeeModule))]
    public class EmployeeModule:IModule
    {
        [Import]
        public IRegionManager Region { get; set; }

        public void Initialize()
        {
            Region.RequestNavigate("EmployeeRegion", "EmployeeView");
        }
    }
}
