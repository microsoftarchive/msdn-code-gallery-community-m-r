// <copyright file="TimelineKeyboardBehavior.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TimelineKeyboardBehavior.cs                    
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline.Behaviors
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Behaviors;

    public class TimelineKeyboardBehavior : KeyboardBehavior
    {
        protected override void OnAttached()
        {
            Application.Current.RootVisual.KeyDown += this.RootVisual_KeyDown;
        }

        protected override void OnDetaching()
        {
            Application.Current.RootVisual.KeyDown -= this.RootVisual_KeyDown;
        }

        protected override bool CanHandleKey(KeyEventArgs e)
        {
            return !(e.OriginalSource is TextBox);
        }

        protected override KeyboardActionContext GetKeyboardActionContext()
        {
            return KeyboardActionContext.Timeline;
        }

        private void RootVisual_KeyDown(object sender, KeyEventArgs e)
        {
            this.ExecuteAction(e);
        }
    }
}
