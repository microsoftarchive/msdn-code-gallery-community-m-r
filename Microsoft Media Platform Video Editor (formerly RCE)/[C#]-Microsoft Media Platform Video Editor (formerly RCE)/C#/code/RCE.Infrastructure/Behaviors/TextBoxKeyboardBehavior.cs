// <copyright file="TextBoxKeyboardBehavior.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TextBoxKeyboardBehavior.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Behaviors
{
    using System.Windows.Controls;
    using System.Windows.Input;

    public abstract class TextBoxKeyboardBehavior : KeyboardBehavior
    {
        protected override void OnAttached()
        {
            this.AssociatedObject.KeyUp += this.RootVisual_KeyUp;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.KeyUp -= this.RootVisual_KeyUp;
        }

        protected override object GetCommandParameter()
        {
            TextBox textBox = this.AssociatedObject as TextBox;

            return textBox != null ? textBox.Text : string.Empty;
        }

        private void RootVisual_KeyUp(object sender, KeyEventArgs e)
        {
            this.ExecuteAction(e);
        }
    }
}