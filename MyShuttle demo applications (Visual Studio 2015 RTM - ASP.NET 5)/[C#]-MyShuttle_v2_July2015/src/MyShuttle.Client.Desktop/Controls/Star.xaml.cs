using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MyShuttle.Client.Desktop.Controls
{
    public partial class Star : UserControl
    {
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register("State", typeof(StarState), typeof(Star), new FrameworkPropertyMetadata(StarState.Off, new PropertyChangedCallback(OnStateChanged)));
        
        public static readonly DependencyProperty StarOffImageProperty = DependencyProperty.Register("StarOffImage", typeof(ImageSource), typeof(Star), new FrameworkPropertyMetadata(null));
        
        public static readonly DependencyProperty StarOnImageProperty = DependencyProperty.Register("StarOnImage", typeof(ImageSource), typeof(Star), new FrameworkPropertyMetadata(null));
        
        public Star()
        {
            InitializeComponent();
        }
        
        public StarState State
        {
            get { return (StarState)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        public bool IsOn
        {
            get { return (this.State == StarState.On); }
        }

        public ImageSource StarOffImage
        {
            get { return (ImageSource)GetValue(StarOffImageProperty); }
            set { SetValue(StarOffImageProperty, value); }
        }

        public ImageSource StarOnImage
        {
            get { return (ImageSource)GetValue(StarOnImageProperty); }
            set { SetValue(StarOnImageProperty, value); }
        }

        private static void OnStateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Star star = obj as Star;

            if (star != null)
            {
                StarState newState = (StarState)e.NewValue;
                if (newState == StarState.Off)
                {
                    star.OffStar.Visibility = Visibility.Visible;
                    star.OnStar.Visibility = Visibility.Collapsed;
                }
                else
                {
                    star.OffStar.Visibility = Visibility.Collapsed;
                    star.OnStar.Visibility = Visibility.Visible;
                }
            }
        }
    }

    public enum StarState
    {
        Off = 0,
        On = 1
    }
}
