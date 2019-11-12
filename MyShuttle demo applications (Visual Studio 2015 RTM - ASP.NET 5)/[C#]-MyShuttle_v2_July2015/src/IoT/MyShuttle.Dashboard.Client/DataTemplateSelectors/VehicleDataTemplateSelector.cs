namespace MyShuttle.Dashboard.Client.DataTemplateSelectors
{
    using Models;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Helpers;

    public class VehicleDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate UnitsTemplate { get; set; }
        public DataTemplate AbsoluteTemplate { get; set; }
        public DataTemplate SmallAbsoluteTemplate { get; set; }
        public DataTemplate SmallUnitsTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var dataItem = item as VehicleSummaryData;

            var isSmallRes = WindowHelper.IsSmallResolution();

            if (isSmallRes)
            {
                return dataItem?.Index < 2 ? SmallUnitsTemplate : SmallAbsoluteTemplate;
            }

            var uiElement = container as UIElement;

            VariableSizedWrapGrid.SetRowSpan(uiElement, 2);

            return dataItem?.Index < 2 ? UnitsTemplate : AbsoluteTemplate;
        }
    }
}
