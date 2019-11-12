using System;
using MyEvents.Api.Client;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MyEvents.Client.Organizer.Converters
{
    public class SelectedItemTextColorConverter : DependencyObject, IValueConverter
    {

        public object CurrentSelectedItem 
        {
            get { return (object)GetValue(CurrentSelectedItemProperty); }
            set { SetValue(CurrentSelectedItemProperty, value); } 
        }
        
        public static readonly DependencyProperty CurrentSelectedItemProperty =
            DependencyProperty.Register("CurrentSelectedItem", typeof(object), typeof(SelectedItemTextColorConverter), new PropertyMetadata(0, new PropertyChangedCallback(TheProperty_changed)));

        private static void TheProperty_changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
 	        
        }
        
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Session selected = value as Session;

            if (selected == CurrentSelectedItem)
                return new SolidColorBrush(Colors.Gray);

            return  new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
           //Not implemented
            return null;
        }
    }
}
