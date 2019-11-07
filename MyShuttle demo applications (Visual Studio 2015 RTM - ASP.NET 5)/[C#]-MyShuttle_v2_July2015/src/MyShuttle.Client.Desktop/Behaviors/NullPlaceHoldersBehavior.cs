using MyShuttle.Client.Core.DocumentResponse;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace MyShuttle.Client.Desktop.Behaviors
{
    public static class NullPlaceHoldersBehavior
    {
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.RegisterAttached("ItemsSource", typeof(ObservableCollection<Vehicle>), typeof(NullPlaceHoldersBehavior), new PropertyMetadata(new ObservableCollection<Vehicle>(), OnItemsSourceChanged));

        public static ObservableCollection<Vehicle> GetItemsSource(DependencyObject obj)
        {
            return (ObservableCollection<Vehicle>)obj.GetValue(ItemsSourceProperty);
        }

        public static void SetItemsSource(DependencyObject obj, double value)
        {
            obj.SetValue(ItemsSourceProperty, value);
        }
        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var listView = d as ListView;
            if (listView == null)
            {
                return;
            }

            listView.SizeChanged -= ListViewSizeChanged;
            listView.SizeChanged += ListViewSizeChanged;
            listView.Loaded -= ListViewLoaded;
            listView.Loaded += ListViewLoaded;

            if (e.OldValue != null)
            {
                var collection = (INotifyCollectionChanged)e.OldValue;
                collection.CollectionChanged -= ItemsSourceCollectionChanged;
            }

            if (e.NewValue != null)
            {
                var collection = (INotifyCollectionChanged)e.NewValue;
                collection.CollectionChanged += (sender, args) => ItemsSourceCollectionChanged(listView, args);
            }

            ItemsSourceCollectionChanged(listView, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        private static void ListViewSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = (ListView)sender;
            listView.Dispatcher.InvokeAsync(() => CalculatePalceHolders((ListView)sender));
        }

        private static void ListViewLoaded(object sender, RoutedEventArgs e)
        {
            var listView = (ListView)sender;
            listView.Loaded -= ListViewLoaded;
            CalculatePalceHolders(listView);
            DisableKeyboardNavigation(listView);
        }

        private static void DisableKeyboardNavigation(ListView listView)
        {
            ScrollViewer scrollViewer = GetVisualChild<ScrollViewer>(listView);
            scrollViewer.KeyDown -= ScrollViewerKeyDown;
            scrollViewer.KeyDown += ScrollViewerKeyDown;
        }

        private static void ScrollViewerKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                case Key.Right:
                case Key.Up:
                case Key.Down:
                    e.Handled = true;
                    break;
                default:
                    break;
            }
        }

        private static void ItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var list = (ListView)sender;
            if (list.ItemsSource == null)
            {
                list.ItemsSource = new ObservableCollection<Vehicle>();
            }
            var collection = (ObservableCollection<Vehicle>)list.ItemsSource;
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                collection.Clear();
                var newItems = GetItemsSource(list);
                if (newItems != null)
                {
                    foreach (var item in newItems)
                    {
                        collection.Add((Vehicle)item);
                    }
                }
            }
            else
            {
                if (e.OldItems != null)
                {
                    foreach (var item in e.OldItems)
                    {
                        collection.Remove((Vehicle)item);
                    }
                }
                if (e.NewItems != null)
                {
                    foreach (var item in e.NewItems)
                    {
                        collection.Add((Vehicle)item);
                    }
                }


                foreach (var item in collection.Reverse().SkipWhile(x => x == null).Where(x => x == null).ToList())
                {
                    collection.Remove(item);
                }
            }

            CalculatePalceHolders((ListView)sender);
        }

        private static void CalculatePalceHolders(ListView list)
        {
            UniformGrid itemsPanel;
            ScrollViewer scrollViewer;
            if (!TryGetItemsPanelAndScrollViewer(list, out itemsPanel, out scrollViewer))
            {
                return;
            }

            var realCollection = GetItemsSource(list);
            var realCollectionCount = realCollection != null ? realCollection.Count : 0;
            var innerCollection = (ObservableCollection<Vehicle>)list.ItemsSource;
            
            var itemHeight = GetItemHeight(list, itemsPanel);
            var collectionCount = innerCollection.Count;
            var height = list.ActualHeight;
            var columns = itemsPanel.Columns;

            var initialRows = (int)Math.Ceiling(realCollectionCount / (double)columns);
            var visibleRows = list.ActualHeight / itemHeight;
            var minimunRows = (int)Math.Ceiling(height / itemHeight);
            var minimunItems = minimunRows * columns;

            while (collectionCount < minimunItems)
            {
                innerCollection.Add(null);
                collectionCount++;
            }

            while (collectionCount > realCollectionCount && collectionCount > minimunItems)
            {
                innerCollection.RemoveAt(innerCollection.Count - 1);
                collectionCount--;
            }

            var adjustItems = collectionCount % columns;
            for (int i = 0; i < adjustItems; i++)
            {
                innerCollection.Add(null);
                collectionCount++;
            }

            UpdateScrollViewerState(scrollViewer, initialRows, visibleRows);
        }

        private static bool TryGetItemsPanelAndScrollViewer(ListView listView, out UniformGrid itemsPanel, out ScrollViewer scrollViewer)
        {
            var elements = listView.Tag as Tuple<UniformGrid, ScrollViewer>;
            if (elements == null)
            {
                itemsPanel = GetVisualChild<UniformGrid>(listView);
                scrollViewer = GetVisualChild<ScrollViewer>(listView);
                if (itemsPanel == null || scrollViewer == null)
                {
                    return false;
                }
                elements = Tuple.Create(itemsPanel, scrollViewer);
                listView.Tag = elements;
            }
            itemsPanel = elements.Item1;
            scrollViewer = elements.Item2;
            return true;
        }

        private static double GetItemHeight(ListView listView, UniformGrid itemsPanel)
        {
            var innerCollection = (ObservableCollection<Vehicle>)listView.ItemsSource;

            if (innerCollection.Count == 0)
            {
                innerCollection.Add(null);
            }
            var listViewItem = (ListViewItem)itemsPanel.Children[0];
            if (listViewItem.ActualHeight == double.NaN || listViewItem.ActualHeight == 0)
            {
                listViewItem.UpdateLayout();
            }
            return listViewItem.ActualHeight;
        }

        private static void UpdateScrollViewerState(ScrollViewer scrollViewer, int initialRows, double visibleRows)
        {
            if (initialRows < visibleRows)
            {
                scrollViewer.PreviewMouseWheel -= ScrollViewerPreviewMouseWheel;
                scrollViewer.PreviewMouseWheel += ScrollViewerPreviewMouseWheel;
                scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                scrollViewer.ScrollToTop();
            }
            else
            {
                scrollViewer.PreviewMouseWheel -= ScrollViewerPreviewMouseWheel;
                scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
        }

        private static void ScrollViewerPreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            e.Handled = true;
        }

        private static T GetVisualChild<T>(DependencyObject parent) where T : Visual
        {
            T child = default(T);

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
    }
}
