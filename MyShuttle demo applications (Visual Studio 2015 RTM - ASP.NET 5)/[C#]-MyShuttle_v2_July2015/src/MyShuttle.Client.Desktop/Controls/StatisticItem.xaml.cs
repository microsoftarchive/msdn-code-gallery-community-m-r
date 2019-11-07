using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyShuttle.Client.Desktop.Controls
{
    public partial class StatisticItem : UserControl
    {
        public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register("HeaderText", typeof(string), typeof(StatisticItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty SubheaderTextProperty = DependencyProperty.Register("SubheaderText", typeof(string), typeof(StatisticItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(StatisticItem), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty PercentageProperty = DependencyProperty.Register("Percentage", typeof(double), typeof(StatisticItem), new FrameworkPropertyMetadata(null));
        
        public StatisticItem()
        {
            InitializeComponent();
        }

        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        public string SubheaderText
        {
            get { return (string)GetValue(SubheaderTextProperty); }
            set { SetValue(SubheaderTextProperty, value); }
        }

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public double Percentage
        {
            get { return (double)GetValue(PercentageProperty); }
            set { SetValue(PercentageProperty, value); }
        }
    }
}
