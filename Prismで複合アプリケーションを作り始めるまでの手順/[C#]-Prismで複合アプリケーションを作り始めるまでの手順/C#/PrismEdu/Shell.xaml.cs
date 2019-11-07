using System.ComponentModel.Composition;
using System.Windows;

namespace PrismEdu
{
    [Export]
    public partial class Shell : Window
    {
        public Shell()
        {
            InitializeComponent();
        }
    }
}
