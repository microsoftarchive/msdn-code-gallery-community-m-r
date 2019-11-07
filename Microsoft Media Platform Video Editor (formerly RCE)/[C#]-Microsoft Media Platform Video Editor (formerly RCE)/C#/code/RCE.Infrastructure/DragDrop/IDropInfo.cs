// <copyright file="IDropInfo.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IDropInfo.cs                     
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
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the interface to define the allowed types that can be dragged to a drop zone.
    /// </summary>
    public interface IDropInfo
    {
        /// <summary>
        /// Gets the allowed types that can be dragged into a drop zone.
        /// </summary>
        /// <value>The list of allowed types.</value>
        IList<Type> AllowedDragTypes { get; }
    }
}
