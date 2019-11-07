using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace MyShuttle.Client.Desktop.Behaviors
{
    public static class DynamicColumnsBehavior
    {
        public static readonly DependencyProperty MaxWidthProperty = DependencyProperty.RegisterAttached("MaxWidth", typeof(double), typeof(DynamicColumnsBehavior), new PropertyMetadata(0d, OnMaxWidthChanged));

        public static readonly DependencyProperty MinWidthProperty = DependencyProperty.RegisterAttached("MinWidth", typeof(double), typeof(DynamicColumnsBehavior), new PropertyMetadata(0d));

        public static double GetMaxWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(MaxWidthProperty);
        }

        public static void SetMaxWidth(DependencyObject obj, double value)
        {
            obj.SetValue(MaxWidthProperty, value);
        }

        public static double GetMinWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(MinWidthProperty);
        }

        public static void SetMinWidth(DependencyObject obj, double value)
        {
            obj.SetValue(MinWidthProperty, value);
        }

        private static void OnMaxWidthChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var uniformGrid = sender as UniformGrid;
            if (uniformGrid == null)
            {
                return;
            }

            var newMaxWidth = (double)e.NewValue;
            uniformGrid.SizeChanged -= UniformGridSizeChanged;
            if (newMaxWidth > 0)
            {
                uniformGrid.SizeChanged += UniformGridSizeChanged;
            }
        }

        private static void UniformGridSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var uniformGrid = (UniformGrid)sender;
            var dynamicMaxWith = GetMaxWidth(uniformGrid);
            var dynamicMinWith = GetMinWidth(uniformGrid);

            var initialColumns = uniformGrid.Columns;
            var size = e.NewSize;

            var finalColumns = (int)Math.Ceiling(size.Width / dynamicMaxWith);
            if ((size.Width / finalColumns) < dynamicMinWith)
            {
                finalColumns = (int)Math.Floor(size.Width / dynamicMinWith);
            }

            uniformGrid.Columns = finalColumns;
        }

    }
}
