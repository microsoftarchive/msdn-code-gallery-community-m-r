// <copyright file="WrapPanel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: WrapPanel.cs                     
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
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Based on http://blogs.msdn.com/devdave/archive/2008/07/17/layout-transitions-an-animatable-wrappanel.aspx.
    /// It automatically handles the layout of the items and the no. of items in a row. 
    /// </summary>
    public class WrapPanel : Panel
    {
        /// <summary>
        /// <see cref="DependencyProperty"/> to store the position of the last element in the wrappanel and 
        /// total number of rows in the wrappanel.
        /// </summary>
        private static readonly DependencyProperty WrapPanelAttachedDataProperty =
            DependencyProperty.RegisterAttached("WrapPanelAttachedDataProperty", typeof(WrapPanelAttachedData), typeof(WrapPanel), null);

        /// <summary>
        /// List to maintain the heights of all the rows.
        /// </summary>
        private readonly List<double> rowHeights = new List<double>();

        /// <summary>
        /// To maintain the row no. for each item.
        /// </summary>
        private readonly Dictionary<int, int> indexRowMapping = new Dictionary<int, int>();

        /// <summary>
        /// Gets the offset from the top of the wrappanel for the given item.
        /// </summary>
        /// <param name="indexOfItem">The index of item.</param>
        /// <returns>The offset position from the top of the wrappanel.</returns>
        public double GetOffsetFromTop(int indexOfItem)
        {
            double offset = 0;
            if (!this.indexRowMapping.ContainsKey(indexOfItem))
            {
                return 0;
            }

            // Get the row no. for the given item.
            int rowNo = this.indexRowMapping[indexOfItem];

            // If row no. is 0 then offset would be 0.
            if (rowNo == 0)
            {
                return 0;
            }
            else
            {
                // Add the height of the rows which are before the row of the given element.
                for (int index = rowNo - 1; index >= 0; index--)
                {
                    offset += this.rowHeights[index];
                }
            }

            return offset;
        }

        /// <summary>
        /// Provides the behavior for the "measure" pass of Silverlight layout. Classes can override this method to define their own measure pass behavior.
        /// </summary>
        /// <param name="availableSize">The available size that this object can give to child objects. Infinity can be specified as a value to indicate that the object will size to whatever content is available.</param>
        /// <returns>
        /// The size that this object determines it needs during layout, based on its calculations of child object allotted sizes.
        /// </returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (FrameworkElement child in this.Children)
            {
                child.Measure(availableSize);
            }
            
            double rowHeight = 0;
            int row = 0;
            this.rowHeights.Clear();
            this.indexRowMapping.Clear();
            Size desiredSize = Size.Empty;
            int elementNo = 0;
            Point nextChildPosition = new Point(0, 0);

            foreach (FrameworkElement child in this.Children)
            {
                if (nextChildPosition.X + child.DesiredSize.Width > availableSize.Width)
                {
                    this.rowHeights.Add(rowHeight);
                    ++row;
                    nextChildPosition.X = 0;
                    nextChildPosition.Y += rowHeight;
                    rowHeight = 0;
                }

                // Insert the index of the item and the row no.
                this.indexRowMapping.Add(elementNo, row);
                elementNo++;
                
                WrapPanelAttachedData data = GetWrapPanelAttachedData(child);

                if (data.TargetPosition != nextChildPosition)
                {
                    data.TargetPosition = nextChildPosition;
                    data.Row = row;
                }

                desiredSize.Width = Math.Max(desiredSize.Width, nextChildPosition.X + child.DesiredSize.Width);
                desiredSize.Height = Math.Max(desiredSize.Height, nextChildPosition.Y + child.DesiredSize.Height);

                rowHeight = Math.Max(rowHeight, child.DesiredSize.Height);

                nextChildPosition.X += child.DesiredSize.Width;
            }

            this.rowHeights.Add(rowHeight);
      
            return desiredSize;
        }

        /// <summary>
        /// Provides the behavior for the "Arrange" pass of Silverlight layout. Classes can override this method to define their own arrange pass behavior.
        /// </summary>
        /// <param name="finalSize">The final area within the parent that this object should use to arrange itself and its children.</param>
        /// <returns>The actual size used.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (UIElement child in this.Children)
            {
                WrapPanelAttachedData data = GetWrapPanelAttachedData(child);
                child.Arrange(new Rect(data.TargetPosition.X, data.TargetPosition.Y, child.DesiredSize.Width, this.rowHeights[data.Row]));
            }

            return finalSize;
        }

        /// <summary>
        /// Gets the wrap panel attached data.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/>.</param>
        /// <returns>The <see cref="WrapPanelAttachedData"/>.</returns>
        private static WrapPanelAttachedData GetWrapPanelAttachedData(DependencyObject obj)
        {
            object value = obj.GetValue(WrapPanelAttachedDataProperty);

            if (value == null)
            {
                WrapPanelAttachedData data = new WrapPanelAttachedData();
                SetAnimatedWrapPanelAttachedData(obj, data);
                return data;
            }

            return (WrapPanelAttachedData)value;
        }

        /// <summary>
        /// Sets the <see cref="WrapPanelAttachedData"/>.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/>.</param>
        /// <param name="value">The <see cref="WrapPanelAttachedData"/>.</param>
        private static void SetAnimatedWrapPanelAttachedData(DependencyObject obj, WrapPanelAttachedData value)
        {
            obj.SetValue(WrapPanelAttachedDataProperty, value);
        }

        /// <summary>
        /// Used by <see cref="WrapPanel"/> to store the position of the 
        /// last element and the number of rows in the <see cref="WrapPanel"/>.
        /// </summary>
        private class WrapPanelAttachedData
        {
            /// <summary>
            /// Default value of the TargetPosition.
            /// </summary>
            public static readonly Point Unset = new Point(-1, -1);

            /// <summary>
            /// Initializes a new instance of the <see cref="WrapPanelAttachedData"/> class.
            /// </summary>
            public WrapPanelAttachedData()
            {
                this.TargetPosition = Unset;
            }

            /// <summary>
            /// Gets or sets the target position.
            /// </summary>
            /// <value>The target position.</value>
            public Point TargetPosition { get; set; }

            /// <summary>
            /// Gets or sets the row number.
            /// </summary>
            /// <value>The total number of rows.</value>
            public int Row { get; set; }
        }
    }
}