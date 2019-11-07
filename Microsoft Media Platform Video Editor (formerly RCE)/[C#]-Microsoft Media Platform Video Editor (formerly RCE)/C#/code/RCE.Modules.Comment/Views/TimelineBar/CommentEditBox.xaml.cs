// <copyright file="CommentEditBox.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CommentEditBox.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Comment
{
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Provides the implementation for CommentEditBox view.
    /// </summary>
    public partial class CommentEditBox : ICommentEditBox
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentEditBox"/> class.
        /// </summary>
        public CommentEditBox()
        {
            InitializeComponent();
        }

        public ICommentEditBoxPresentationModel Model
        {
            get { return this.DataContext as CommentEditBoxPresentationModel; }
            set { this.DataContext = value; }
        }

        public void Close()
        {
            this.EditBoxPopup.IsOpen = false;
        }

        public void Show()
        {
            this.EditBoxPopup.IsOpen = true;
            this.CommentBox.Focus();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.CommentBox.Focus();
        }
    }
}