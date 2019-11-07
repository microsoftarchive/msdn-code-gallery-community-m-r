using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace MVVMLightWPFSampleApp.Commons
{
    public class ShowWindowAction : TriggerAction<Window>
    {


        public bool IsModal
        {
            get { return (bool)GetValue(IsModalProperty); }
            set { SetValue(IsModalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsModal.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsModalProperty =
            DependencyProperty.Register("IsModal", typeof(bool), typeof(ShowWindowAction), new PropertyMetadata(false));




        public Type WindowType
        {
            get { return (Type)GetValue(WindowTypeProperty); }
            set { SetValue(WindowTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WindowType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WindowTypeProperty =
            DependencyProperty.Register("WindowType", typeof(Type), typeof(ShowWindowAction), new PropertyMetadata(null));



        protected override void Invoke(object parameter)
        {
            if (this.WindowType == null) { return; }

            var window = Activator.CreateInstance(this.WindowType) as Window;
            window.Owner = this.AssociatedObject;
            if (this.IsModal)
            {
                window.ShowDialog();
            }
            else
            {
                window.Show();
            }
        }
    }
}
