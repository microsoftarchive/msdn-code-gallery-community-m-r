// <copyright file="DataTemplateSelector.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DataTemplateSelector.cs                     
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
    using System.Windows;

    /// <summary>
    /// Base class for <see cref="AssetDataTemplateSelector"/>.
    /// </summary>
    public class DataTemplateSelector
    {
        /// <summary>
        /// Selects the appropiate template for the given item.
        /// </summary>
        /// <param name="item">The item for which <see cref="DataTemplate"/> is to be selected.</param>
        /// <param name="container">The container.</param>
        /// <returns>The <see cref="DataTemplate"/> for the given item.</returns>
        public virtual DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            throw new NotImplementedException();
        }
    }
}