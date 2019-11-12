using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace MvvmMasterDetailApp.Commons
{
    public class ShowWindowAction : TriggerAction<Window>
    {
        public bool ShowModal
        {
            get { return (bool)GetValue(ShowModalProperty); }
            set { SetValue(ShowModalProperty, value); }
        }

        public static readonly DependencyProperty ShowModalProperty =
            DependencyProperty.Register("ShowModal", typeof(bool), typeof(ShowWindowAction), new PropertyMetadata(false));


        public Type WindowType
        {
            get { return (Type)GetValue(WindowTypeProperty); }
            set { SetValue(WindowTypeProperty, value); }
        }

        public static readonly DependencyProperty WindowTypeProperty =
            DependencyProperty.Register("WindowType", typeof(Type), typeof(ShowWindowAction), new PropertyMetadata(null));


        protected override void Invoke(object parameter)
        {
            var window = (Window)Activator.CreateInstance(this.WindowType);
            if (this.ShowModal)
            {
                window.Owner = this.AssociatedObject;
                window.ShowDialog();
            }
            else
            {
                window.Show();
            }
        }
    }
}
