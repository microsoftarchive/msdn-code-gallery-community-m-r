namespace MyShuttle.Dashboard.Client.Helpers
{
    using System.Collections.Generic;
    using System.Reflection;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;

    public static class ControlTreeHelper
    {
        /// <summary>
        ///     Find a UserControl in the parent controls
        /// </summary>
        /// <param name="value">Child control</param>
        /// <returns>Reference to the UserControl</returns>
        public static FrameworkElement FindParentUserControl(FrameworkElement value)
        {
            FrameworkElement result = null;
            if (value != null)
            {
                while (value.Parent != null && !(value.Parent is UserControl))
                {
                    if (value.Parent is FrameworkElement)
                    {
                        value = (FrameworkElement)value.Parent;
                    }
                    else
                    {
                        break;
                    }
                }

                if (value is UserControl)
                {
                    result = value;
                }
            }

            return result;
        }

        /// <summary>
        ///     Find a control of type <see cref="{T}" /> straight in parenting line
        /// </summary>
        /// <typeparam name="T">Type of the control to find</typeparam>
        /// <param name="leaf">Child control to start the searching</param>
        /// <returns>A reference to the control if it's exists</returns>
        public static T FindUniqueParentControl<T>(DependencyObject leaf)
        {
            T res = default(T);
            if (leaf != null)
            {
                DependencyObject tmp = VisualTreeHelper.GetParent(leaf);
                while (tmp != null && !typeof(T).GetTypeInfo().IsAssignableFrom(tmp.GetType().GetTypeInfo()))
                {
                    tmp = VisualTreeHelper.GetParent(tmp);
                }

                if (tmp != null)
                {
                    if (typeof(T).GetTypeInfo().IsAssignableFrom(tmp.GetType().GetTypeInfo()))
                    {
                        res = (T)((object)tmp);
                    }
                }
            }

            return res;
        }

        /// <summary>
        ///     Return a list with the straight parent line
        /// </summary>
        /// <param name="element">Child element to start the search</param>
        /// <returns>The list with all the parent elements</returns>
        public static List<DependencyObject> GetAllParentControls(DependencyObject element)
        {
            var list = new List<DependencyObject>();
            list.Add(element);

            while (VisualTreeHelper.GetParent(element) != null)
            {
                list.Add(VisualTreeHelper.GetParent(element));
                element = VisualTreeHelper.GetParent(element);
            }

            return list;
        }

        /// <summary>
        ///     Find a visual child of a element of type <see cref="{T}" />
        /// </summary>
        /// <typeparam name="T">Type of the children to search for</typeparam>
        /// <param name="obj">Element to start the searching</param>
        /// <returns>A reference to the child element</returns>
        public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }
                var childOfChild = FindVisualChild<T>(child);
                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }

            return null;
        }

        /// <summary>
        ///     Gets all the control of a type <see cref="{T}" />
        /// </summary>
        /// <typeparam name="T">Type of element to search for</typeparam>
        /// <param name="root">Start element</param>
        /// <returns>A List with all the elements to search for</returns>
        public static List<T> GetAllControlsByType<T>(DependencyObject root)
        {
            var list = new List<T>();
            var tmp = new List<DependencyObject>();
            ExploreTree(tmp, root);
            for (int x = 0; x < tmp.Count; x++)
            {
                if (tmp[x].GetType() == typeof(T) || tmp[x].GetType().GetTypeInfo().IsSubclassOf(typeof(T)))
                {
                    list.Add((T)((object)tmp[x]));
                }
            }

            return list;
        }

        /// <summary>
        ///     Gets a list with all the child elements of the visual tree
        /// </summary>
        /// <param name="obj">Root node to start the search</param>
        /// <returns>A list with all the controls of the visual tree</returns>
        public static List<DependencyObject> GetVisualTree(DependencyObject obj)
        {
            var tmp = new List<DependencyObject>();
            ExploreTree(tmp, obj);
            return tmp;
        }

        /// <summary>
        ///     Find a element based on their name
        /// </summary>
        /// <typeparam name="T">Type of the control to search</typeparam>
        /// <param name="root">Root control to start the search</param>
        /// <param name="name">Name of the control</param>
        /// <returns>A reference to the control</returns>
        public static T FindNameInVisualTree<T>(DependencyObject root, string name)
        {
            T res = default(T);
            List<DependencyObject> tree = null;
            tree = GetVisualTree(root);
            for (int x = 0; x < tree.Count; x++)
            {
                if (tree[x] is FrameworkElement)
                {
                    if (((FrameworkElement)tree[x]).Name == name)
                    {
                        res = (T)((object)tree[x]);
                        break;
                    }
                }
            }

            return res;
        }

        /// <summary>
        ///     Internal method where the search is doing
        /// </summary>
        /// <param name="list">A list with all the elements</param>
        /// <param name="userControl">Root control</param>
        private static void InternalGetAllControls(List<FrameworkElement> list, FrameworkElement userControl)
        {
            if (list != null && userControl != null)
            {
                if (userControl is FrameworkElement)
                {
                    list.Add(userControl);
                }

                if (userControl is ContentControl)
                {
                    var actual = (ContentControl)userControl;
                    if (actual.Content is FrameworkElement)
                    {
                        list.Add((FrameworkElement)actual.Content);
                    }

                    if (actual.Content is Panel)
                    {
                        userControl = (FrameworkElement)actual.Content;
                    }
                }

                if (userControl is Panel)
                {
                    var panel = (Panel)userControl;
                    for (int x = 0; x < panel.Children.Count; x++)
                    {
                        if (panel.Children[x] is FrameworkElement)
                        {
                            list.Add((FrameworkElement)panel.Children[x]);
                        }
                        else
                        {
                            InternalGetAllControls(list, (FrameworkElement)panel.Children[x]);
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Explore the visual tree
        /// </summary>
        /// <param name="list">List with all the elements</param>
        /// <param name="obj">Current tree node</param>
        private static void ExploreTree(List<DependencyObject> list, DependencyObject obj)
        {
            if (list != null && obj != null)
            {
                int childrens = VisualTreeHelper.GetChildrenCount(obj);
                for (int x = 0; x < childrens; x++)
                {
                    DependencyObject tmp = VisualTreeHelper.GetChild(obj, x);
                    list.Add(tmp);
                    ExploreTree(list, tmp);
                }
            }
        }

        /// <summary>
        ///     Internal method for search on the visual tree
        ///     <remark>
        ///         This is where the search is doing
        ///     </remark>
        /// </summary>
        /// <param name="list">List with all the elements</param>
        /// <param name="current">Current tree node</param>
        private static void InternalGetAllUserControl(List<UserControl> list, FrameworkElement current)
        {
            if (list != null && current != null)
            {
                if (current is ContentControl)
                {
                    var actual = (ContentControl)current;
                    if (actual.Content is UserControl)
                    {
                        list.Add((UserControl)actual.Content);
                    }

                    if (actual.Content is Panel)
                    {
                        current = (FrameworkElement)actual.Content;
                    }
                }

                if (current is Panel)
                {
                    var panel = (Panel)current;
                    for (int x = 0; x < panel.Children.Count; x++)
                    {
                        if (panel.Children[x] is UserControl)
                        {
                            list.Add((UserControl)panel.Children[x]);
                        }
                        else
                        {
                            InternalGetAllUserControl(list, (FrameworkElement)panel.Children[x]);
                        }
                    }
                }
            }
        }
    }
}
