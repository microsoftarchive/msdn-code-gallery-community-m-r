// <copyright file="ICommentView.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ICommentView.cs                     
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
    using Infrastructure.Models;
    using Models;

    /// <summary>
    /// Interface that defines a Comments view.
    /// </summary>
    public interface ICommentView
    {
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The <see cref="ICommentViewPresentationModel"/>.</value>
        ICommentViewPresentationModel Model { get; set; }

        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="message">The message.</param>
        void ShowErrorMessage(string message);

        /// <summary>
        /// Clears the ink comment strokes.
        /// </summary>
        void ClearInkComment();

        /// <summary>
        /// Sets the editing mode.
        /// </summary>
        /// <param name="mode">The edit mode for the <see cref="InkComment"/>.</param>
        void SetInkEditingMode(InkEditingMode mode);
    }
}
