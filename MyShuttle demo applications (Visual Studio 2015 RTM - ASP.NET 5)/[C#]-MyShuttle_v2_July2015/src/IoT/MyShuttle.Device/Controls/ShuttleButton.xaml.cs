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

namespace MyShuttle.Device.Controls
{
    /// <summary>
    /// Interaction logic for ShuttleButton.xaml
    /// </summary>
    public partial class ShuttleButton : Button
    {
        public ShuttleButton()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty EnabledImageProperty = DependencyProperty.Register(
            "EnabledImage", typeof (ImageSource), typeof (ShuttleButton), new PropertyMetadata(default(ImageSource)));

        public ImageSource EnabledImage
        {
            get { return (ImageSource) GetValue(EnabledImageProperty); }
            set { SetValue(EnabledImageProperty, value); }
        }

        public static readonly DependencyProperty DisabledImageProperty = DependencyProperty.Register(
            "DisabledImage", typeof (ImageSource), typeof (ShuttleButton), new PropertyMetadata(default(ImageSource)));

        public ImageSource DisabledImage
        {
            get { return (ImageSource) GetValue(DisabledImageProperty); }
            set { SetValue(DisabledImageProperty, value); }
        }

        public static readonly DependencyProperty EnabledTextColorProperty = DependencyProperty.Register(
            "EnabledTextColor", typeof (SolidColorBrush), typeof (ShuttleButton), new PropertyMetadata(default(SolidColorBrush)));

        public SolidColorBrush EnabledTextColor
        {
            get { return (SolidColorBrush) GetValue(EnabledTextColorProperty); }
            set { SetValue(EnabledTextColorProperty, value); }
        }

        public static readonly DependencyProperty DisabledTextColorProperty = DependencyProperty.Register(
            "DisabledTextColor", typeof (SolidColorBrush), typeof (ShuttleButton), new PropertyMetadata(default(SolidColorBrush)));

        public SolidColorBrush DisabledTextColor
        {
            get { return (SolidColorBrush) GetValue(DisabledTextColorProperty); }
            set { SetValue(DisabledTextColorProperty, value); }
        }


        public static readonly DependencyProperty ButtonTextProperty = DependencyProperty.Register(
          "ButtonText", typeof(string), typeof(ShuttleButton), new PropertyMetadata(default(string)));

        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        public static readonly DependencyProperty TextVisibilityProperty = DependencyProperty.Register(
           "TextVisibility", typeof(Visibility), typeof(ShuttleButton), new PropertyMetadata(default(Visibility)));

        public Visibility TextVisibility
        {
            get { return (Visibility)GetValue(TextVisibilityProperty); }
            set { SetValue(TextVisibilityProperty, value); }
        }

        public static readonly DependencyProperty ImageSizeProperty = DependencyProperty.Register(
            "ImageSize", typeof(int), typeof(ShuttleButton), new PropertyMetadata(35));

        public int ImageSize
        {
            get { return (int)GetValue(ImageSizeProperty); }
            set { SetValue(ImageSizeProperty, value); }
        }

    }
}
