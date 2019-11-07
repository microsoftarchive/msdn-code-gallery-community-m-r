using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MyCompany.Visitors.Client.UniversalApp.Controls
{
    public sealed partial class SnappedVisitList : UserControl
    {
        public object ListItemsSource
        {
            get { return (object)GetValue(ListItemsSourceProperty); }
            set { SetValue(ListItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ListItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListItemsSourceProperty =
            DependencyProperty.Register("ListItemsSource", typeof(object), typeof(SnappedVisitList), new PropertyMetadata(0));        

        public SnappedVisitList()
        {
            this.InitializeComponent();
        }
    }
}
