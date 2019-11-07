using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace PrismEdu
{
    [ModuleExport(typeof(SampleModule))]
    public class SampleModule : IModule
    {
        // PrismのコンポーネントはMEFから設定してもらう
        [Import]
        public IRegionManager RegionManager { get; set; }

        public void Initialize()
        {
            // MainのRegionにSampleViewを表示する
            this.RegionManager.RequestNavigate(
                "Main", "SampleView");
        }
    }
}
