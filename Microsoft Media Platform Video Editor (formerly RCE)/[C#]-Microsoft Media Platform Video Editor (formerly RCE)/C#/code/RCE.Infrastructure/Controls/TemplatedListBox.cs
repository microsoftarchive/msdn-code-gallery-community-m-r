// <copyright file="TemplatedListBox.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TemplatedListBox.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Controls
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// <see cref="ListBox"/> control which supports rendering diffrent <see cref="DataTemplate"/> for
    /// diffrent items.
    /// </summary>
    public class TemplatedListBox : ListBox
    {
        /// <summary>
        /// <see cref="DependencyProperty"/> for selecting template for the given item.
        /// </summary>
        public static readonly DependencyProperty ItemTemplateSelectorProperty = DependencyProperty.Register("ItemTemplateSelector", typeof(DataTemplateSelector), typeof(TemplatedListBox), new PropertyMetadata(new PropertyChangedCallback(OnItemTemplateChanged)));

        /// <summary>
        /// Gets or sets the item template selector.
        /// </summary>
        /// <value>The item template selector.</value>
        public DataTemplateSelector ItemTemplateSelector
        {
            get { return (DataTemplateSelector)this.GetValue(ItemTemplateSelectorProperty); }
            set { this.SetValue(ItemTemplateSelectorProperty, value); }
        }

        /// <summary>
        /// Prepares the specified element to display the specified item.
        /// </summary>
        /// <param name="element">Element used to display the specified item.</param>
        /// <param name="item">The specified item.</param>
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            ListBoxItem listBoxItem = element as ListBoxItem;

            if (listBoxItem != null)
            {
                listBoxItem.ContentTemplate = this.ItemTemplateSelector.SelectTemplate(item, this);
            }
        }

        /// <summary>
        /// Called when [item template changed].
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject"/>.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnItemTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
    }
}