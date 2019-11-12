using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ODataReadWriteSampleApp.Commons
{
    [TypeConstraint(typeof(ListViewBase))]
    public class ItemClickToCommandBehavior : DependencyObject, IBehavior
    {
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(ItemClickToCommandBehavior), new PropertyMetadata(null));


        public DependencyObject AssociatedObject { get; private set; }

        private ListViewBase ListView { get { return (ListViewBase)this.AssociatedObject; } }

        public void Attach(DependencyObject associatedObject)
        {
            this.AssociatedObject = associatedObject;
            this.ListView.ItemClick += this.ItemClick;
        }

        private void ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.Command.CanExecute(e.ClickedItem))
            {
                this.Command.Execute(e.ClickedItem);
            }
        }

        public void Detach()
        {
            this.ListView.ItemClick -= this.ItemClick;
        }
    }
}
