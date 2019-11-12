// <copyright file="ErrorView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ErrorView.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Dialogs
{
    using System.Windows;
    using System.Windows.Controls;
    using Infrastructure;

    /// <summary>
    /// A view to show error messages.
    /// </summary>
    public partial class ErrorView : ChildWindow, IErrorView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorView"/> class.
        /// </summary>
        public ErrorView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the error message to show.
        /// </summary>
        /// <value>The error message to show.</value>
        public string ErrorMessage
        {
            get { return this.MessageTextBlock.Text; }
            set { this.MessageTextBlock.Text = value; }
        }

        /// <summary>
        /// Shows the view.
        /// </summary>
        public void Show()
        {
            base.Show();
        }

        /// <summary>
        /// Handles the click event of the OKButton.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args instance containing the event data.</param>
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
