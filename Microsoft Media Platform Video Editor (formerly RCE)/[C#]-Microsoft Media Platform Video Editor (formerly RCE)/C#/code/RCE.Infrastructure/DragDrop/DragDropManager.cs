// <copyright file="DragDropManager.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DragDropManager.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.DragDrop
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Input;
    using Behaviors;

    public sealed class DragDropManager
    {
        public static readonly DependencyProperty CanBeDraggedProperty = DependencyProperty.RegisterAttached(
           "CanBeDragged",
           typeof(bool),
           typeof(DragDropManager),
           new PropertyMetadata(true));

        public static readonly DependencyProperty IsDragSourceProperty = DependencyProperty.RegisterAttached(
           "IsDragSource",
           typeof(bool),
           typeof(DragDropManager),
           new PropertyMetadata(false, IsDragSourceChanged));

        public static readonly DependencyProperty AllowDropProperty = DependencyProperty.RegisterAttached(
            "AllowDrop",
            typeof(bool),
            typeof(DragDropManager),
            new PropertyMetadata(false, AllowDropChanged));

        public static readonly DependencyProperty DragTemplateProperty = DependencyProperty.RegisterAttached(
           "DragTemplate",
           typeof(DataTemplate),
           typeof(DragDropManager),
           new PropertyMetadata(null));

        public static readonly DependencyProperty DragDataProperty = DependencyProperty.RegisterAttached(
          "DragData",
          typeof(object),
          typeof(DragDropManager),
          new PropertyMetadata(null));

        public static readonly DependencyProperty CustomDataProperty = DependencyProperty.RegisterAttached(
          "CustomData",
          typeof(object),
          typeof(DragDropManager),
          new PropertyMetadata(null));

        public static readonly DependencyProperty DropDataProperty = DependencyProperty.RegisterAttached(
          "DropData",
          typeof(object),
          typeof(DragDropManager),
          new PropertyMetadata(null));

        public static readonly DependencyProperty DropInfoProperty = DependencyProperty.RegisterAttached(
            "DropInfo",
            typeof(IDropInfo),
            typeof(DragDropManager),
            new PropertyMetadata(default(IDropInfo)));

        /// <summary>
        /// Command to execute on click event.
        /// </summary>
        public static readonly DependencyProperty DropCommandProperty = DependencyProperty.RegisterAttached(
            "DropCommand",
            typeof(ICommand),
            typeof(DragDropManager),
            null);

        private static readonly DependencyProperty DragDropBehaviorProperty = DependencyProperty.RegisterAttached(
            "DragDropBehavior",
            typeof(DragDropBehavior),
            typeof(DragDropManager),
            null);

        private static List<FrameworkElement> dropZones = new List<FrameworkElement>();

        /// <summary>
        /// Prevents a default instance of the <see cref="DragDropManager"/> class from being created.
        /// </summary>
        private DragDropManager()
        {
        }

        public static List<FrameworkElement> DropZones
        {
            get { return dropZones; }
        }

        public static bool GetCanBeDragged(DependencyObject dependencyObject)
        {
            return (bool)dependencyObject.GetValue(CanBeDraggedProperty);
        }

        public static void SetCanBeDragged(DependencyObject dependencyObject, bool value)
        {
            dependencyObject.SetValue(CanBeDraggedProperty, value);
        }

        public static bool GetIsDragSource(DependencyObject dependencyObject)
        {
            return (bool)dependencyObject.GetValue(IsDragSourceProperty);
        }

        public static void SetIsDragSource(DependencyObject dependencyObject, bool value)
        {
            dependencyObject.SetValue(IsDragSourceProperty, value);
        }

        public static bool GetAllowDrop(DependencyObject dependencyObject)
        {
            return (bool)dependencyObject.GetValue(AllowDropProperty);
        }

        public static void SetAllowDrop(DependencyObject dependencyObject, bool value)
        {
            dependencyObject.SetValue(AllowDropProperty, value);
        }

        public static void SetDragTemplate(DependencyObject dependencyObject, DataTemplate value)
        {
            dependencyObject.SetValue(DragTemplateProperty, value);
        }

        public static DataTemplate GetDragTemplate(DependencyObject dependencyObject)
        {
            return (DataTemplate)dependencyObject.GetValue(DragTemplateProperty);
        }

        public static object GetDragData(DependencyObject dependencyObject)
        {
            return dependencyObject.GetValue(DragDataProperty);
        }

        public static object GetCustomData(DependencyObject dependencyObject)
        {
            return dependencyObject.GetValue(CustomDataProperty);
        }

        public static void SetDragData(DependencyObject dependencyObject, object value)
        {
            dependencyObject.SetValue(DragDataProperty, value);
        }

        public static void SetCustomData(DependencyObject dependencyObject, object value)
        {
            dependencyObject.SetValue(CustomDataProperty, value);
        }

        public static object GetDropData(DependencyObject dependencyObject)
        {
            return dependencyObject.GetValue(DropDataProperty);
        }

        public static void SetDropData(DependencyObject dependencyObject, object value)
        {
            dependencyObject.SetValue(DropDataProperty, value);
        }

        public static IDropInfo GetDropInfo(DependencyObject element)
        {
            return (IDropInfo)element.GetValue(DropInfoProperty);
        }

        public static void SetDropInfo(DependencyObject element, IDropInfo value)
        {
            element.SetValue(DropInfoProperty, value);
        }

        /// <summary>
        /// Retrieves the <see cref="ICommand"/> attached to the <see cref="DependencyObject"/>.
        /// </summary>
        /// <param name="dependencyObject">ButtonBase containing the Command dependency property.</param>
        /// <returns>The value of the command attached.</returns>
        public static ICommand GetDropCommand(DependencyObject dependencyObject)
        {
            return dependencyObject.GetValue(DropCommandProperty) as ICommand;
        }

        /// <summary>
        /// Sets the <see cref="ICommand"/> to execute on the click event.
        /// </summary>
        /// <param name="dependencyObject">The object where the value will be set.</param>
        /// <param name="command">Command to attach.</param>
        public static void SetDropCommand(DependencyObject dependencyObject, ICommand command)
        {
            dependencyObject.SetValue(DropCommandProperty, command);
        }

        private static void IsDragSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            UIElement dragSource = dependencyObject as UIElement;

            if (dragSource != null)
            {
                if (e.NewValue.Equals(true))
                {
                    GetOrCreateBehavior(dragSource);
                }
                else
                {
                    DestroyBehavior(dragSource);
                }
            }
        }

        private static void AllowDropChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement dropZone = dependencyObject as FrameworkElement;

            if (dropZone != null)
            {
                RegisterDropZone(dropZone);
            }
        }

        private static void RegisterDropZone(FrameworkElement dropZone)
        {
            if (!dropZones.Contains(dropZone))
            {
                dropZones.Add(dropZone);
            }
        }

        /// <summary>
        /// Gets or creates a <see cref="DragDropBehavior"/> for the element.
        /// </summary>
        /// <param name="element">The element to attach the behavior.</param>
        /// <returns>The attached behavior.</returns>
        private static DragDropBehavior GetOrCreateBehavior(UIElement element)
        {
            DragDropBehavior behavior = element.GetValue(DragDropBehaviorProperty) as DragDropBehavior;
            
            if (behavior == null)
            {
                behavior = new DragDropBehavior(element);
                element.SetValue(DragDropBehaviorProperty, behavior);
            }

            return behavior;
        }

        /// <summary>
        /// Deattachs the behavior from the element.
        /// </summary>
        /// <param name="element">The element to deattach the behavior.</param>
        private static void DestroyBehavior(UIElement element)
        {
            DragDropBehavior behavior = element.GetValue(DragDropBehaviorProperty) as DragDropBehavior;

            if (behavior != null)
            {
                element.SetValue(DragDropBehaviorProperty, null);
                behavior.Dispose();
            }
        }
    }
}
