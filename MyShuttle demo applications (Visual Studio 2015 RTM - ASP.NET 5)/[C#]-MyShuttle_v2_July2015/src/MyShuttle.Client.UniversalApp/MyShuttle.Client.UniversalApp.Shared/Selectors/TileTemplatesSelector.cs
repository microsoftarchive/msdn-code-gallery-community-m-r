using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace MyShuttle.Client.UniversalApp.Selectors
{
    public class TileTemplatesSelector : DataTemplateSelector
    {
        //These are public properties that will be used in the Resources section of the XAML.
        public DataTemplate MainTileItemTemplate { get; set; }

        public DataTemplate DefaultTileItemTemplate { get; set; }
        
        public TileTemplatesSelector()
        {
            MainTileItemTemplate = null;
            DefaultTileItemTemplate = null;
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var dataTemplate = new DataTemplate();
            var parentListView = FindParent<ListViewBase>((FrameworkElement)container);
            var itemIndex = parentListView.Items.IndexOf(item);
            if (itemIndex == 0)
            {
                dataTemplate = MainTileItemTemplate;
            }
            else
            {
                dataTemplate = DefaultTileItemTemplate;
            }

            return dataTemplate;
        }

        public static T FindParent<T>(FrameworkElement element) where T : FrameworkElement
        {
            FrameworkElement parent = VisualTreeHelper.GetParent(element) as FrameworkElement;

            while (parent != null)
            {
                T correctlyTyped = parent as T;
                if (correctlyTyped != null)
                    return correctlyTyped;
                else
                    return FindParent<T>(parent);
            }

            return null;
        }
    }
}
