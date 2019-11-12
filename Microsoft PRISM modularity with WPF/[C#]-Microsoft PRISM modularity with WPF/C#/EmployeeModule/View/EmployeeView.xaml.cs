using EmployeeModule.ViewModel;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace EmployeeModule.View
{

    [Export("EmployeeView")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RegionMemberLifetime(KeepAlive = false)]
    public partial class EmployeeView : UserControl
    {
        [ImportingConstructor]
        public EmployeeView(EmployeeViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
