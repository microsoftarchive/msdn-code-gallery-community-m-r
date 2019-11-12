using Windows.Graphics.Display;
using MyShuttle.Dashboard.Client.Helpers;

namespace MyShuttle.Dashboard.Client.DataTemplateSelectors
{
    using Models;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public class TopDriverDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SmallTemplate { get; set; }
        public DataTemplate LongTemplate { get; set; }
        public DataTemplate MediumTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var dataItem = item as Driver;
            var uiElement = container as UIElement;

            var isSmallRes = WindowHelper.IsSmallResolution();

            if (dataItem?.Index == 0)
            {
                VariableSizedWrapGrid.SetRowSpan(uiElement, isSmallRes ? 2 :  3);
                return isSmallRes ? MediumTemplate : LongTemplate;
            }

            if (dataItem?.Index < 4)
            {
                VariableSizedWrapGrid.SetRowSpan(uiElement, isSmallRes ? 1 : 2);
                return isSmallRes ? SmallTemplate : MediumTemplate;
            }

            return SmallTemplate;
        }

        
    }
}
