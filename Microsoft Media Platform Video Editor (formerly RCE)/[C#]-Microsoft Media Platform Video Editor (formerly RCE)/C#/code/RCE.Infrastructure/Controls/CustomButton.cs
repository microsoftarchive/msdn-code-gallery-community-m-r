// <copyright file="CustomButton.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CustomButton.cs                     
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
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Custom button control which triggers mouse left button up/down events.
    /// </summary>
    public class CustomButton : Button
    {
        /// <summary>
        /// Provides class handling for the <see cref="E:System.Windows.UIElement.MouseLeftButtonDown"/> event that occurs when the left mouse button is pressed while the mouse pointer is over this control.
        /// </summary>
        /// <param name="e">The event data.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="e"/> is null.
        /// </exception>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            e.Handled = false;
        }

        /// <summary>
        /// Provides class handling for the <see cref="E:System.Windows.UIElement.MouseLeftButtonUp"/> event that occurs when the left mouse button is released while the mouse pointer is over this control.
        /// </summary>
        /// <param name="e">The event data.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="e"/> is null.
        /// </exception>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            e.Handled = false;
        }
    }
}
