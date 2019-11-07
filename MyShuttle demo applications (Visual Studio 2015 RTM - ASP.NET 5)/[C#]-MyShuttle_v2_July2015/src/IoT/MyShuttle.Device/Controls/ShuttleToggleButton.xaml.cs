using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for ShuttleToggleButton.xaml
    /// </summary>
    public partial class ShuttleToggleButton : ToggleButton
    {
        public ShuttleToggleButton()
        {
            InitializeComponent();
        }
            
        public static readonly DependencyProperty CheckedImageProperty = DependencyProperty.Register(
            "CheckedImage", typeof (ImageSource), typeof (ShuttleToggleButton), new PropertyMetadata(default(ImageSource)));

        public ImageSource CheckedImage
        {
            get { return (ImageSource) GetValue(CheckedImageProperty); }
            set { SetValue(CheckedImageProperty, value); }
        }

        public static readonly DependencyProperty UncheckedImageProperty = DependencyProperty.Register(
            "UncheckedImage", typeof (ImageSource), typeof (ShuttleToggleButton), new PropertyMetadata(default(ImageSource)));

        public ImageSource UncheckedImage
        {
            get { return (ImageSource) GetValue(UncheckedImageProperty); }
            set { SetValue(UncheckedImageProperty, value); }
        }

        public static readonly DependencyProperty CheckedTextColorProperty = DependencyProperty.Register(
            "CheckedTextColor", typeof(SolidColorBrush), typeof(ShuttleToggleButton), new PropertyMetadata(default(SolidColorBrush)));

        public SolidColorBrush CheckedTextColor
        {
            get { return (SolidColorBrush)GetValue(CheckedTextColorProperty); }
            set { SetValue(CheckedTextColorProperty, value); }
        }

        public static readonly DependencyProperty UncheckedTextColorProperty = DependencyProperty.Register(
            "UncheckedTextColor", typeof(SolidColorBrush), typeof(ShuttleToggleButton), new PropertyMetadata(default(SolidColorBrush)));

        public SolidColorBrush UncheckedTextColor
        {
            get { return (SolidColorBrush)GetValue(UncheckedTextColorProperty); }
            set { SetValue(UncheckedTextColorProperty, value); }
        }

        public static readonly DependencyProperty ButtonTextProperty = DependencyProperty.Register(
            "ButtonText", typeof (string), typeof (ShuttleToggleButton), new PropertyMetadata(default(string)));

        public string ButtonText
        {
            get { return (string) GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        public static readonly DependencyProperty TextVisibilityProperty = DependencyProperty.Register(
            "TextVisibility", typeof (Visibility), typeof (ShuttleToggleButton), new PropertyMetadata(default(Visibility)));

        public Visibility TextVisibility
        {
            get { return (Visibility) GetValue(TextVisibilityProperty); }
            set { SetValue(TextVisibilityProperty, value); }
        }

        public static readonly DependencyProperty ImageSizeProperty = DependencyProperty.Register(
            "ImageSize", typeof (int), typeof (ShuttleToggleButton), new PropertyMetadata(35));

        public int ImageSize
        {
            get { return (int) GetValue(ImageSizeProperty); }
            set { SetValue(ImageSizeProperty, value); }
        }
    
    }
}
