// <copyright file="ICommentViewPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ICommentViewPresentationModel.cs                     
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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Ink;
    using Infrastructure;
    using Infrastructure.Models;
    using Microsoft.Practices.Composite.Presentation.Commands;

    using RCE.Infrastructure.Services;

    /// <summary>
    /// Interface for comment view presentation model.
    /// </summary>
    public interface ICommentViewPresentationModel : IHeaderInfoProvider<string>, INotifyPropertyChanged, IKeyboardAware
    {
        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <value>The <see cref="ICommentView"/>.</value>
        ICommentView View { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>The comments.</value>
        ObservableCollection<Comment> Comments { get; set; }

        /// <summary>
        /// Gets or sets the comments types.
        /// </summary>
        /// <value>The comments types.</value>
        IList<string> CommentsTypes { get; set; }

        /// <summary>
        /// Gets or sets the currently selected comment type.
        /// </summary>
        /// <value>The type of the selected comment.</value>
        string SelectedCommentType { get; set; }

        /// <summary>
        /// Gets or sets the current comment.
        /// </summary>
        /// <value>The current comment.</value>
        Comment CurrentComment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the current mode is edit.
        /// </summary>
        /// <value>A <seealso cref="bool"/> that represents current mode is edit. true, indicates current mode is edit. false, that is not.</value>
        bool EditMode { get; set; }

        /// <summary>
        /// Gets the command which searches the given text.
        /// </summary>
        /// <value>The <see cref="DelegateCommand{T}"/>.</value>
        DelegateCommand<string> SearchCommand { get; }

        /// <summary>
        /// Gets the command that deletes the selected comment.
        /// </summary>
        /// <value>The <see cref="DelegateCommand{T}"/>.</value>
        DelegateCommand<object> DeleteCommand { get; }

        /// <summary>
        /// Gets the command that save the comment.
        /// </summary>
        /// <value>The <see cref="DelegateCommand{T}"/>.</value>
        DelegateCommand<Guid> SaveCommand { get; }

        /// <summary>
        /// Gets the command that save the comment.
        /// </summary>
        /// <value>The <see cref="DelegateCommand{T}"/>.</value>
        DelegateCommand<string> CancelCommand { get; }

        /// <summary>
        /// Gets the command that edit the current selected comment.
        /// </summary>
        /// <value>The <see cref="DelegateCommand{T}"/>.</value>
        DelegateCommand<object> EditCommand { get; }

        /// <summary>
        /// Gets the command that plays the current selected command.
        /// </summary>
        /// <value>The <see cref="DelegateCommand{T}"/>.</value>
        DelegateCommand<object> PlayCommentCommand { get; }

        /// <summary>
        /// Gets or sets the text of the current comment.
        /// </summary>
        /// <value>An <seealso cref="string"/> that represents the text of the current comment.</value>
        string Text { get; set; }

        /// <summary>
        /// Gets the header icon (on status).
        /// </summary>
        /// <value>An <seealso cref="string" /> that represents the header icon on resource.</value>
        string HeaderIconOn { get; }

        /// <summary>
        /// Gets the header icon (off status).
        /// </summary>
        /// <value>An <seealso cref="string" /> that represents the header icon off resource.</value>
        string HeaderIconOff { get; }

        /// <summary>
        /// Gets or sets the ink comment strokes.
        /// </summary>
        /// <value>The ink comment strokes.</value>
        StrokeCollection InkCommentStrokes { get; set; }

        /// <summary>
        /// Gets or sets the frame image.
        /// </summary>
        /// <value>The frame image.</value>
        string FrameImage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the global comments should be showed in the comments view.
        /// </summary>
        /// <value>A <seealso cref="bool"/> that represents if the global comments should be show. true, indicates that the global comments is showed. false, that is not showed.</value>
        bool ShowGlobalComments { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the timeline comments should be showed in the comments view.
        /// </summary>
        /// <value>A <seealso cref="bool"/> that represents if the timeline comments should be show. true, indicates that the global comments is showed. false, that is not showed.</value>
        bool ShowTimelineComments { get; set; }

        /// <summary>
        /// Plays the comment.
        /// </summary>
        void PlayComment();
    }
}
