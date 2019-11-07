using System;
using MyEvents.Api.Client;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MyEvents.Client.Organizer.Converters
{
    public class SelectedItemHighlightedConverter : DependencyObject, IValueConverter
    {

        public object CurrentSelectedItem
        {
            get { return (object)GetValue(CurrentSelectedItemProperty); }
            set { SetValue(CurrentSelectedItemProperty, value); }
        }

        public static readonly DependencyProperty CurrentSelectedItemProperty =
            DependencyProperty.Register("CurrentSelectedItem", typeof(object), typeof(SelectedItemHighlightedConverter), new PropertyMetadata(0, new PropertyChangedCallback(TheProperty_Changed)));

        
        
        private static void TheProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
 	        
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Session selected = value as Session;


            if (selected == CurrentSelectedItem)
                return App.Current.Resources["EventItemBackgroundBrush"] as LinearGradientBrush;

            return App.Current.Resources["SessionInfoBrush"] as SolidColorBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
