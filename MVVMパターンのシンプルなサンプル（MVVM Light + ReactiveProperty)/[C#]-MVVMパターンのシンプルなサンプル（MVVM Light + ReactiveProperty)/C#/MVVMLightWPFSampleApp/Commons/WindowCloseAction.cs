using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace MVVMLightWPFSampleApp.Commons
{
    public class WindowCloseAction : TriggerAction<Window>
    {
        protected override void Invoke(object parameter)
        {
            this.AssociatedObject.Close();
        }
    }
}
