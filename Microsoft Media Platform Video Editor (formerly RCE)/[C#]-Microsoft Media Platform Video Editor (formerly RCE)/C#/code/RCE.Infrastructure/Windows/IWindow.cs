// <copyright file="IWindow.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IWindow.cs                     
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
    using System.Windows.Controls;

    public interface IWindow
    {
        event EventHandler Closed;

        event EventHandler Closing;

        object Content { get; set; }
        
        Style Style { get; set; }

        double Left { get; set; }

        double Top { get; set; }

        bool IsOpen { get; }
        
        bool IsModal { get; }

        bool HasFocus { get; }

        object Title { get; set; }

        ResizeDirection ResizeDirection { get; set; }

        Size Size { get; set; }

        void Show(Panel root);

        void ShowDialog(Panel root);

        void Close();

        void SetWindowPosition(double heigth, double width);
    }
}
