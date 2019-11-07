using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace PrismEdu
{
    /// <summary>
    /// SampleView.xaml の相互作用ロジック
    /// </summary>
    [Export("SampleView", typeof(SampleView))]
    public partial class SampleView : UserControl
    {
        public SampleView()
        {
            InitializeComponent();
        }
    }
}
