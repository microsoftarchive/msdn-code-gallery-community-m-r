using MyShuttle.Dashboard.Client.Helpers;

namespace MyShuttle.Dashboard.Client.DataTemplateSelectors
{
    using Models;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public class GeneralStatisticsDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MediumTemplate { get; set; }
        public DataTemplate SmallTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var dataItem = item as VehicleSummaryData;
            var uiElement = container as UIElement;

            var isSmallRes = WindowHelper.IsSmallResolution();
            
            if (dataItem?.Index < 2 && !isSmallRes)
            {
                VariableSizedWrapGrid.SetRowSpan(uiElement, 2);
                return MediumTemplate;
            }

            return SmallTemplate;
        }
    }
}
