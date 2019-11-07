using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MyShuttle.Client.Desktop.Controls
{
    public partial class Rating : UserControl
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(Rating), new FrameworkPropertyMetadata(0, new PropertyChangedCallback(OnValueChanged), new CoerceValueCallback(CoerceValueValue)));
        
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(int), typeof(Rating), new FrameworkPropertyMetadata(5));
        
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(int), typeof(Rating), new FrameworkPropertyMetadata(0));

        public static readonly DependencyProperty StarOffImageProperty = DependencyProperty.Register("StarOffImage", typeof(ImageSource), typeof(Rating), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(StarOffImageChnaged)));
        
        public static readonly DependencyProperty StarOnImageProperty = DependencyProperty.Register("StarOnImage", typeof(ImageSource), typeof(Rating), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(StarOnImageChnaged)));
        
        public Rating()
        {
            InitializeComponent();
        }

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
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

        private static object CoerceValueValue(DependencyObject obj, object value)
        {
            Rating rating = (Rating)obj;

            int current = (int)value;

            if (current < rating.Minimum)
            {
                current = rating.Minimum;
            }

            if (current > rating.Maximum)
            {
                current = rating.Maximum;
            }

            return current;
        }

        private static void OnValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            Rating rating = (Rating)obj;
            if (rating.stackPanelStars.Children.Count > 0)
            {
                int value = 1;

                foreach (var item in rating.stackPanelStars.Children)
                {
                    var star = (Star)item;
                    star.State = value <= rating.Value ? StarState.On : StarState.Off;
                    value++;
                }
            }
        }

        private void ControlLoaded(object sender, RoutedEventArgs e)
        {
            if (stackPanelStars.Children.Count == 0)
            {
                InitializeStars();
            }
        }

        private void InitializeStars()
        {
            for (int value = 1; value <= this.Maximum; value++)
            {
                Star star = new Star();
                star.State = value <= this.Value ? StarState.On : StarState.Off;
                star.StarOffImage = this.StarOffImage;
                star.StarOnImage = this.StarOnImage;

                this.stackPanelStars.Children.Add(star);
            }
        }

        private void UpdateStars()
        {
            if (stackPanelStars.Children.Count > 0)
            {
                int value = 1;

                foreach (var item in stackPanelStars.Children)
                {
                    var star = (Star)item;
                    star.State = value <= Value ? StarState.On : StarState.Off;
                    value++;
                }
            }
        }

        private static void StarOnImageChnaged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            Rating rating = (Rating)obj;
            foreach (var item in rating.stackPanelStars.Children)
            {
                var star = (Star)item;
                star.StarOnImage = rating.StarOnImage;
            }
        }

        private static void StarOffImageChnaged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            Rating rating = (Rating)obj;
            foreach (var item in rating.stackPanelStars.Children)
            {
                var star = (Star)item;
                star.StarOffImage = rating.StarOffImage;
            }
        }
    }
}
