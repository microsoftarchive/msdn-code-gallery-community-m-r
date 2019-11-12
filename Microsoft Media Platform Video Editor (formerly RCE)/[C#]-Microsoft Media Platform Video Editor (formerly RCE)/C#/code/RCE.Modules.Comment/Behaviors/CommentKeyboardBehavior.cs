// <copyright file="CommentKeyboardBehavior.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MediaBinKeyboardBehavior.cs                    
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Comment.Behaviors
{
    using System.Windows;
    using System.Windows.Input;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Behaviors;

    public class CommentKeyboardBehavior : KeyboardBehavior
    {
        protected override void OnAttached()
        {
            Application.Current.RootVisual.KeyUp += this.RootVisual_KeyUp;
        }

        protected override void OnDetaching()
        {
            Application.Current.RootVisual.KeyUp -= this.RootVisual_KeyUp;
        }

        protected override KeyboardActionContext GetKeyboardActionContext()
        {
            return KeyboardActionContext.Comment;
        }

        private void RootVisual_KeyUp(object sender, KeyEventArgs e)
        {
            this.ExecuteAction(e);
        }
    }
}
