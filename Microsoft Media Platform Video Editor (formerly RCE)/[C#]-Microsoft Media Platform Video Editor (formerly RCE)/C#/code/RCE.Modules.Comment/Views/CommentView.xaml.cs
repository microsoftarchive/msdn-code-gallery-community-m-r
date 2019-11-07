// <copyright file="CommentView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CommentView.xaml.cs                     
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
    using System;
    using System.Windows;
    using System.Windows.Browser;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using Infrastructure;
    using Infrastructure.Models;
    using Models;

    /// <summary>
    /// View for the comment module.
    /// </summary>
    public partial class CommentView : UserControl, ICommentView
    {
        /// <summary>
        /// The field used to maintain the current stroke for an ink comment.
        /// </summary>
        private Stroke currentStroke;

        /// <summary>
        /// The field used to store the points to erase.
        /// </summary>
        private StylusPointCollection pointsToErase;

        /// <summary>
        /// The field used to maintain the current color for an ink comment.
        /// </summary>
        private Color currentColor;

        /// <summary>
        /// The field used to maintain the current <seealso cref="InkEditingMode"/>.
        /// </summary>
        private InkEditingMode editingMode;

        /// <summary>
        /// Ticks for the last click of the mouse.
        /// </summary>
        private long lastClickTicks;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentView"/> class.
        /// </summary>
        public CommentView()
        {
            this.currentColor = Colors.Black;
            InitializeComponent();

            this.LayoutUpdated += this.CommentView_LayoutUpdated;
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public ICommentViewPresentationModel Model
        {
            get
            {
                return this.DataContext as ICommentViewPresentationModel;
            }

            set
            {
                this.DataContext = value;
            }
        }

        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ShowErrorMessage(string message)
        {
            HtmlPage.Window.Dispatcher.BeginInvoke(() => MessageBox.Show(message, RCE.Modules.Comment.Resources.Resources.CommentsModuleCaption, MessageBoxButton.OK));
        }

        /// <summary>
        /// Clears the ink comment strokes.
        /// </summary>
        public void ClearInkComment()
        {
            this.InkPresenterCommentText.Strokes.Clear();
        }

        /// <summary>
        /// Sets the editing mode.
        /// </summary>
        /// <param name="mode">The editing mode for the <see cref="InkComment"/>.</param>
        public void SetInkEditingMode(InkEditingMode mode)
        {
            this.editingMode = mode;
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!this.Model.EditMode)
            {
                if ((DateTime.Now.Ticks - this.lastClickTicks) < UtilityHelper.MouseDoubleClickDurationValue)
                {
                    // play the current comment.
                    this.Model.PlayComment();
                    this.lastClickTicks = 0;
                }

                this.lastClickTicks = DateTime.Now.Ticks;
            }
        }

        /// <summary>
        /// Handles the Loaded event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Binding editCommand = new Binding("EditCommand") { Source = this.DataContext };
            ((BindingHelper)this.Resources["EditCommand"]).SetBinding(BindingHelper.ValueProperty, editCommand);

            Binding deleteCommand = new Binding("DeleteCommand") { Source = this.DataContext };
            ((BindingHelper)this.Resources["DeleteCommand"]).SetBinding(BindingHelper.ValueProperty, deleteCommand);

            Binding playCommentCommand = new Binding("PlayCommentCommand") { Source = this.DataContext };
            ((BindingHelper)this.Resources["PlayCommentCommand"]).SetBinding(BindingHelper.ValueProperty, playCommentCommand);
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the InkPresenterCommentText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void InkPresenterCommentText_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.InkPresenterCommentText.CaptureMouse();

            if (this.editingMode == InkEditingMode.Ink)
            {
                this.currentStroke = new Stroke
                                         {
                                             DrawingAttributes = { Color = this.currentColor }
                                         };
                this.currentStroke.StylusPoints.Add(e.StylusDevice.GetStylusPoints(this.InkPresenterCommentText));
                this.InkPresenterCommentText.Strokes.Add(this.currentStroke);
            }

            if (this.editingMode == InkEditingMode.Erase)
            {
                this.pointsToErase = e.StylusDevice.GetStylusPoints(this.InkPresenterCommentText);
            }
        }

        /// <summary>
        /// Handles the MouseMove event of the InkPresenterCommentText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void InkPresenterCommentText_MouseMove(object sender, MouseEventArgs e)
        {
            Point point = e.GetPosition(this.InkPresenterCommentText);

            if (point.X > InkPresenterCommentText.ActualWidth || point.Y > InkPresenterCommentText.ActualHeight)
            {
                return;
            }

            if (this.editingMode == InkEditingMode.Ink && this.currentStroke != null)
            {
                this.currentStroke.DrawingAttributes.Color = this.currentColor;
                this.currentStroke.StylusPoints.Add(e.StylusDevice.GetStylusPoints(this.InkPresenterCommentText));
            }

            if (this.editingMode == InkEditingMode.Erase && this.pointsToErase != null)
            {
                this.pointsToErase.Add(e.StylusDevice.GetStylusPoints(this.InkPresenterCommentText));

                StrokeCollection hitStrokes = this.InkPresenterCommentText.Strokes.HitTest(this.pointsToErase);

                if (hitStrokes.Count > 0)
                {
                    foreach (Stroke hitStroke in hitStrokes)
                    {
                        this.InkPresenterCommentText.Strokes.Remove(hitStroke);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the MouseLeftButtonUp event of the InkPresenterCommentText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void InkPresenterCommentText_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.InkPresenterCommentText.ReleaseMouseCapture();
            this.currentStroke = null;
            this.pointsToErase = null;
            this.Model.InkCommentStrokes = this.InkPresenterCommentText.Strokes;
        }

        /// <summary>
        /// Handles the MouseLeftButtonUp event of the Brush control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void Brush_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string originalSource = ((Border)sender).Name;

            this.InkRadioButton.IsChecked = true;

            switch (originalSource)
            {
                case "RedBrush":
                    this.currentColor = Colors.Red;
                    break;
                case "BlueBrush":
                    this.currentColor = Colors.Blue;
                    break;
                case "YellowBrush":
                    this.currentColor = Colors.Yellow;
                    break;
                case "GreenBrush":
                    this.currentColor = Colors.Green;
                    break;
                case "BrownBrush":
                    this.currentColor = Colors.Brown;
                    break;
                default:
                    this.currentColor = Colors.Black;
                    break;
            }
        }

        /// <summary>
        /// Handles the Selected event of the Ink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Ink_Selected(object sender, RoutedEventArgs e)
        {
            if (this.InkPresenterCommentText != null)
            {
                this.editingMode = InkEditingMode.Ink;
                this.InkPresenterCommentText.Cursor = Cursors.Stylus;
            }
        }

        /// <summary>
        /// Handles the Selected event of the Eraser control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Eraser_Selected(object sender, RoutedEventArgs e)
        {
            if (this.InkPresenterCommentText != null)
            {
                this.editingMode = InkEditingMode.Erase;
                this.InkPresenterCommentText.Cursor = Cursors.Eraser;
            }
        }

        /// <summary>
        /// Handles the LayoutUpdated event of the CommentView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CommentView_LayoutUpdated(object sender, EventArgs e)
        {
            ScaleTransform scale = InkPresenterCommentText.RenderTransform as ScaleTransform;

            if (scale != null)
            {
                double modifier = 1 / this.InkPresenterCommentText.Height;
                scale.ScaleY = this.InkPresenterGrid.ActualHeight * modifier;
                scale.ScaleX = this.InkPresenterGrid.ActualWidth * modifier;
            }
        }

        /// <summary>
        /// Handles the KeyUp event of the WatermarkedTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void WatermarkedCommentTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.Model.Text = this.WatermarkedCommentTextBox.Text;
                this.Model.SaveCommand.Execute(this.Model.CurrentComment.CommentId);
            }
        }

        /// <summary>
        /// Handles the KeyUp event of the WatermarkedTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void WatermarkedInkCommentTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.Model.Text = this.WatermarkedInkCommentTextBox.Text;
                this.Model.SaveCommand.Execute(this.Model.CurrentComment.CommentId);
            }
        }
    }
}
