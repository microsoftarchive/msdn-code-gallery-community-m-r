// <copyright file="CommentViewer.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CommentViewer.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Player
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Ink;
    using System.Windows.Media;
    using RCE.Infrastructure.Models;

    /// <summary>
    /// To display the comment.
    /// </summary>
    public partial class CommentViewer : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentViewer"/> class.
        /// </summary>
        public CommentViewer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Shows the comment.
        /// </summary>
        /// <param name="comments">The comments being shown.</param>
        public void ShowComments(List<Comment> comments)
        {
            if (comments != null && comments.Count > 0)
            {
                this.Visibility = Visibility.Visible;
                this.TextualComments.Children.Clear();
                this.InkComment.Strokes.Clear();
                this.ShowAllComments(comments);
            }
            else
            {
                this.InkComment.Visibility = Visibility.Collapsed;
                this.CommentTextRectangle.Visibility = Visibility.Collapsed;
                this.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Create a textblock.
        /// </summary>
        /// <param name="text">The text being shown.</param>
        /// <returns>The textblock.</returns>
        private static TextBlock CreateTextBlock(string text)
        {
            TextBlock comment = new TextBlock
                                    {
                                        FontFamily = new FontFamily("Trebuchet MS"),
                                        TextWrapping = TextWrapping.Wrap,
                                        FontWeight = FontWeights.Normal,
                                        RenderTransformOrigin = new Point(0.5, 0.5),
                                        HorizontalAlignment = HorizontalAlignment.Left,
                                        VerticalAlignment = VerticalAlignment.Bottom,
                                        FontSize = 10,
                                        Foreground = new SolidColorBrush(Colors.White),
                                        Text = text,
                                        Margin = new Thickness(2, 1, 2, 2),
                                        Width = 320
                                    };
            return comment;
        }

        /// <summary>
        /// This method displays all the comments for the given shot or asset.
        /// </summary>
        /// <param name="comments">Comments collection associated with the current playing asset.</param>
        private void ShowAllComments(IEnumerable<Comment> comments)
        {
            foreach (Comment comment in comments)
            {
                switch (comment.CommentType)
                {
                    case CommentType.Ink:
                        this.InkComment.Visibility = Visibility.Visible;

                        InkComment inkComment = (InkComment)comment;
                        
                        foreach (Stroke stroke in inkComment.InkCommentStrokes)
                        {
                            this.InkComment.Strokes.Add(stroke);
                        }

                        this.AddTextBlock(inkComment.Text);

                        break;
                    default:

                        this.AddTextBlock(comment.Text);
                        
                        break;
                }
            }
        }

        /// <summary>
        /// Adds a textblock to the textual comments placeholder. Validates if there is space for another comment or not.
        /// </summary>
        /// <param name="text">The text to be added.</param>
        private void AddTextBlock(string text)
        {
            if (this.TextualComments.Children.Count < 4)
            {
                TextBlock textBlock = CreateTextBlock(text);

                this.TextualComments.Children.Add(textBlock);
            }
            else if (this.TextualComments.Children.Count == 4)
            {
                TextBlock textBlock = CreateTextBlock("...");

                this.TextualComments.Children.Add(textBlock);
            }

            this.CommentTextRectangle.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Handles the OnLayoutUpdated event of the CommentViewer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CommentViewer_OnLayoutUpdated(object sender, EventArgs e)
        {
            double modifier = 1 / this.InkComment.Height;
            this.InkCommentScaleTransform.ScaleY = this.Height * modifier;
            this.InkCommentScaleTransform.ScaleX = this.Height * modifier;
        }
    }
}
