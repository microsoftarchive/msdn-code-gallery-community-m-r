// <copyright file="VisualTreeHelperExtensions.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: VisualTreeHelperExtensions.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>
    
namespace RCE.Infrastructure
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Extension methods for the <see cref="VisualTreeHelper"/> class.
    /// </summary>
    public static class VisualTreeHelperExtensions
    {
        /// <summary>
        /// Gets all the child controls in the given control.
        /// </summary>
        /// <typeparam name="T">The type of the controls that is to be searched in the given control.</typeparam>
        /// <param name="obj">The control instance.</param>
        /// <returns>The list of child controls.</returns>
        public static IList<T> GetChildControls<T>(this DependencyObject obj) where T : DependencyObject
        {
            var childs = new List<T>();
            DependencyObject child;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);

                if (child != null && (child.GetType() == typeof(T) || child.GetType().IsSubclassOf(typeof(T))))
                {
                    childs.Add(child as T);
                }
                else if (child != null)
                {
                    IList<T> subChilds = child.GetChildControls<T>();
                    if (subChilds.Count > 0)
                    {
                        childs.AddRange(subChilds);
                    }
                }
            }

            return childs;
        }

        public static T GetParentControl<T>(this DependencyObject obj) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(obj);

            if (parent == null)
            {
                return null;
            }

            return parent as T ?? GetParentControl<T>(parent);
        }
    }
}
