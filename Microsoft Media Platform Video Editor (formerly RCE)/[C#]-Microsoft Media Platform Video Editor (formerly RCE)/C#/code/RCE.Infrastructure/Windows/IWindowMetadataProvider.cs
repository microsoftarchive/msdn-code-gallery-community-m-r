// <copyright file="IWindowMetadataProvider.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IWindowInfoProvider.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Windows
{
    using System;
    using System.Windows;

    public interface IWindowMetadataProvider
    {
        event EventHandler<DataEventArgs<object>> TitleUpdated;

        event EventHandler<DataEventArgs<object>> ResetPositionRaised;

        VerticalWindowPosition VerticalPosition { get; }

        HorizontalWindowPosition HorizontalPosition { get; }

        object Title { get; }

        ResizeDirection ResizeDirection { get; }

        Size Size { get; }
    }
}
