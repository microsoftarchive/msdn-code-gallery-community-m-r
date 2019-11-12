namespace MyShuttle.Dashboard.Client.Behaviors
{
    using System.Collections;
    using System.Collections.Generic;
    using Windows.UI.Xaml;
    using Models;

    using WinRTXamlToolkit.Controls.DataVisualization.Charting;
    public static class SeriesDefinitionBehavior
    {
        public static readonly DependencyProperty ItemsSourceBindingProperty;

        static SeriesDefinitionBehavior()
        {
            ItemsSourceBindingProperty = DependencyProperty.RegisterAttached("ItemsSourceBinding",
            typeof(IEnumerable), typeof(SeriesDefinitionBehavior), new PropertyMetadata(null, ItemsSourceBindingCallback));
        }

        public static IEnumerable GetItemsSourceBinding(UIElement element)
        {
            return (IEnumerable<MilesChartData>)element.GetValue(ItemsSourceBindingProperty);
        }

        public static void SetItemsSourceBinding(UIElement element, IEnumerable value)
        {
            element.SetValue(ItemsSourceBindingProperty, value);
        }

        private static void ItemsSourceBindingCallback(object obj, DependencyPropertyChangedEventArgs e)
        {
            var element = obj as SeriesDefinition;

            if (element != null) element.ItemsSource = (IEnumerable) e.NewValue;
        }
    }

}
