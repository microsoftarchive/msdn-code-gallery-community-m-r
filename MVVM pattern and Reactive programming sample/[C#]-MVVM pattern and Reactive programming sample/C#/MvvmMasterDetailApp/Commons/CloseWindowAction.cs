using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace MvvmMasterDetailApp.Commons
{
    public class CloseWindowAction : TriggerAction<Window>
    {
        protected override void Invoke(object parameter)
        {
            this.AssociatedObject.Close();
        }
    }
}
