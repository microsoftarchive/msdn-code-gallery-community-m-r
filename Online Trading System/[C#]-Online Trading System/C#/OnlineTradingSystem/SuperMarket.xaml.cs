using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;




namespace OnlineTradingSystem
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial  class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
         
        private void image2_MouseEnter(object sender, MouseEventArgs e)
        {


            Image c = sender as Image;
         
            var top = Canvas.GetTop(c);
            var left = Canvas.GetLeft(c);

            Storyboard sb = new Storyboard();
            foreach (var ab in MoveTo(c, left, top, 250, 0))
                sb.Children.Add(ab);

            foreach (var ab in MoveTo(c, 0, 250, 0, 0))
            {
                ab.BeginTime = TimeSpan.FromSeconds(5);
                sb.Children.Add(ab);
            }

            (sender as Image).BeginStoryboard(sb);



 

        }
     
      


        public static   void MoveTo(  Image target, double newX, double newY)
        {
            var top = Canvas.GetTop(target);
            var left = Canvas.GetLeft(target);
            TranslateTransform trans = new TranslateTransform();
            target.RenderTransform = trans;
            DoubleAnimation anim1 = new DoubleAnimation(top, newY - top, TimeSpan.FromSeconds(2));
            DoubleAnimation anim2 = new DoubleAnimation(left, newX - left, TimeSpan.FromSeconds(2));
            trans.BeginAnimation(TranslateTransform.XProperty, anim1);
            trans.BeginAnimation(TranslateTransform.YProperty, anim2);
           
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();

            
        }

        private void label3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UserPassword win2 = new UserPassword(this);
            win2.Show();
        }

        private void Enterlabel_MouseDown(object sender, MouseButtonEventArgs e)
        {

            MainWindow win = new MainWindow();
            this.Close();
            win.Show();

        }

 
         

        

        static IEnumerable<DoubleAnimation> MoveTo(Image target, double origX, double origY, double newX, double newY)
        {

            TranslateTransform trans = new TranslateTransform();
            target.RenderTransform = trans;
            var a = new DoubleAnimation(origX, newY, TimeSpan.FromSeconds(2));
            Storyboard.SetTargetProperty(a, new PropertyPath("(Canvas.Top)"));
            var b = new DoubleAnimation(origY, newX, TimeSpan.FromSeconds(2));
            Storyboard.SetTargetProperty(b, new PropertyPath("(Canvas.Left)"));

            yield return a;
            yield return b;
        }

        private void textBlock1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Registration win = new Registration();
            win.Show();
        }
 

        
    }
}
