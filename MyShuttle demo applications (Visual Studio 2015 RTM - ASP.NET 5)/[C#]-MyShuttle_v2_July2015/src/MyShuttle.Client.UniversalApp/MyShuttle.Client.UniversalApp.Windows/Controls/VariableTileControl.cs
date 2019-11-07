using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MyShuttle.Client.UniversalApp.Controls
{
    public class VariableTileControl : GridView
    {
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            var itemIndex = Items.IndexOf(item);
            if (itemIndex == 0)
            {
                element.SetValue(VariableSizedWrapGrid.ColumnSpanProperty, 10);
                element.SetValue(VariableSizedWrapGrid.RowSpanProperty, 44);

                if (Window.Current.Bounds.Height >= 1080)
                {
                    element.SetValue(VariableSizedWrapGrid.ColumnSpanProperty, 17);
                    element.SetValue(VariableSizedWrapGrid.RowSpanProperty, 66);
                }
            }
            else
            {
                element.SetValue(VariableSizedWrapGrid.ColumnSpanProperty, 22);
                element.SetValue(VariableSizedWrapGrid.RowSpanProperty, 11);
            }

            base.PrepareContainerForItemOverride(element, item);
        }

        public void Update()
        {
            if (!(this.ItemsPanelRoot is VariableSizedWrapGrid))
                throw new ArgumentException("ItemsPanel is not VariableSizedWrapGrid");

            foreach (var container in this.ItemsPanelRoot.Children.Cast<GridViewItem>())
            {
                dynamic data = container.Content;
                VariableSizedWrapGrid.SetRowSpan(container, data.RowSpan);
                VariableSizedWrapGrid.SetColumnSpan(container, data.ColSpan);
            }

            this.ItemsPanelRoot.InvalidateMeasure();
        }
    }
}
